using AspNetCore;
using AutoMapper;
using BusinessManagementSystem.BusinessLayer.Services;
using BusinessManagementSystem.Controllers;
using BusinessManagementSystem.Data;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Enums;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Repositories;
using BusinessManagementSystem.Services;
using BusinessManagementSystem.Utility;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;

namespace BusinessManagementSystem.BusinessLayer.Implementations
{
    public class UserImpl :IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        ResponseDto<User> _responseDto;
        ResponseDto<UserRoleDto> _responseUserRole;
        ResponseDto<UserDto> _responseUserDto;
        ResponseDto<UserDetailDto> _responseUserDetailDto;
        public UserImpl(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _responseDto = new ResponseDto<User>();
            _responseUserRole = new ResponseDto<UserRoleDto>();
            _responseUserDto = new ResponseDto<UserDto>();
            _responseUserDetailDto = new ResponseDto<UserDetailDto>();
            _mapper = mapper;
        }
        public ResponseDto<UserRoleDto> GetAllUser(string roleName)
        {
           _responseUserRole = _unitOfWork.Users.GetAllUser(roleName);
            return _responseUserRole;
        }
        public ResponseDto<User> GetUserById(int id)
        {
            _responseDto = _unitOfWork.Users.GetFirstOrDefault(p =>p.Id==id, includeProperties: "UserRoles.Role");
            return _responseDto;
        }
        public ResponseDto<UserDetailDto> GetUserDetailDtoById(int id)
        {
            try
            {
                RequestDto requestDto = new RequestDto();
                requestDto.UserId = id;
                _responseDto = _unitOfWork.Users.GetFirstOrDefault(p => p.Id == id, includeProperties: "Appointments");
                if(_responseDto.StatusCode== HttpStatusCode.OK) 
                {
                    var appointMents = _unitOfWork.Appointment.GetAll(p => p.UserId == id,
                        orderBy:p=>p.AppointmentDate,
                        orderByDescending:true,
                        includeProperties: "Payment").Datas;
                    var paymentHistories = _unitOfWork.Payment.GetPaymentHistory(requestDto).Datas;

                     UserDetailDto userDetailDto = _mapper.Map<UserDetailDto>(_responseDto.Data);
                    userDetailDto.Appointments = appointMents;
                    userDetailDto.PaymentHistories = paymentHistories;
                    _responseUserDetailDto.Data = userDetailDto;
                    _responseUserDetailDto.StatusCode = HttpStatusCode.OK ;
                }
                else
                {
                    _responseUserDetailDto.StatusCode = _responseDto.StatusCode;
                    _responseUserDetailDto.Message = _responseDto.Message;
                } 
            }
            catch (Exception ex)
            {
                _responseUserDetailDto.StatusCode = HttpStatusCode.InternalServerError;
                _responseUserDetailDto.Message = _responseDto.Message + "Exception:" + ex.Message;
            }
            return _responseUserDetailDto;
        }
        public ResponseDto<UserDto> GetUserByGuid(Guid guid)
        {
            UserDto userDto = new();
            _responseDto = _unitOfWork.Users.GetFirstOrDefault(p => p.Guid == guid, includeProperties: "UserRoles.Role");
            if (_responseDto.StatusCode == HttpStatusCode.OK)
            {
                userDto = _mapper.Map<UserDto>(_responseDto.Data);
                _responseUserDto.Data = userDto;
                _responseUserDto.StatusCode=_responseDto.StatusCode;
            }
            else
            {
                _responseUserDto.StatusCode = _responseDto.StatusCode;
                _responseUserDto.Message = _responseDto.Message;
            }
            return _responseUserDto;
        }
        public ResponseDto<User> GetAllActiveUsers()
        {
            _responseDto = _unitOfWork.Users.GetAll(p => p.Status == true);
            return _responseDto;
        }
        public ResponseDto<User> GetAllInactiveUsers()
        {
            _responseDto = _unitOfWork.Users.GetAll(p => p.Status == false);
            return _responseDto;
        }
        public ResponseDto<User> CreateUser(UserDto userDto)
        {
            try
            {
                var hashInfo = Helper.Helpers.GetHashPassword(userDto.Password);
                List<UserRole> urList = [new UserRole { RoleId = userDto.RoleId }];
                User u = new User();
                u = _mapper.Map<User>(userDto);
                u.HashPassword = hashInfo.Hash;
                u.Occupation = SD.Occupations.FirstOrDefault(x => x.Value == userDto.Occupation).Value;
                u.Status = true;
                u.Salt = hashInfo.Salt;
                u.FirstPasswordReset = false;
                u.UserRoles = urList;
                _responseDto = _unitOfWork.Users.Insert(u);
            }
            catch (Exception ex)
            {
                _responseDto.StatusCode = HttpStatusCode.InternalServerError;
                _responseDto.Message= ex.Message;
            }
            
            return _responseDto;
        }
        public ResponseDto<User> UpdateUser(UserDto userDto)
        {
            var item = _unitOfWork.Users.GetFirstOrDefault(p => p.Id == userDto.UserId, includeProperties: "UserRoles", tracked:true);
            if(item.StatusCode == HttpStatusCode.OK)
            {
                if (!string.IsNullOrEmpty(userDto.ProfilePictureLink))
                {
                    item.Data.ProfilePictureLink = userDto.ProfilePictureLink;
                }
                //item.Data.UserName=userDto.UserName;
                item.Data.Email = userDto.Email;
                item.Data.FullName = userDto.FullName;
                //item.Data.DateOfBirth=userDto.DateOfBirth;
                item.Data.Address = userDto.Address;
                item.Data.PhoneNumber = userDto.PhoneNumber;
                //item.Data.Gender=userDto.Gender;
                item.Data.Occupation = userDto.Occupation;
                item.Data.Status = userDto.Status;
                item.Data.FacebookLink = userDto.FacebookLink;
                item.Data.InstagramLink = userDto.InstagramLink;
                item.Data.TiktokLink = userDto.TiktokLink;
                item.Data.Skills = userDto.Skills;
                item.Data.Notes = userDto.Notes;
                item.Data.Percentage = (int)userDto.Percentage;
                item.Data.DefaultTips = userDto.DefaultTips;
                var userRole = item.Data.UserRoles.Where(p => p.UserId == userDto.UserId).SingleOrDefault();
                if (userRole != null)
                {
                    var response = _unitOfWork.UserRole.Delete(userRole);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        List<UserRole> urList = [new UserRole { RoleId = userDto.RoleId }];
                        item.Data.UserRoles = urList;
                    }
                }
                _responseDto = _unitOfWork.Users.Update(item.Data);
            }
            else
            {
                _responseDto.StatusCode = item.StatusCode;
                _responseDto.Message = item.Message;
            }
             return _responseDto;
        }
        public ResponseDto<User> DeleteUser(int id)
        {
            var result = _unitOfWork.Users.GetById(id);
            if (result.StatusCode==HttpStatusCode.OK)
            {
                _responseDto = _unitOfWork.Users.Delete(result.Data);
            }
            else
            {
                _responseDto.StatusCode = HttpStatusCode.NotFound;
                _responseDto.Message = "User Not Found";
            }
            return _responseDto;
        }
        public dynamic RoleList()
        {
            var roleLIst = _unitOfWork.Users.RoleList();
            return roleLIst;
        }
        public dynamic GetAllActiveTattooArtist()
        {
            var activeTattooArtist = _unitOfWork.Users.ArtistList();
            return activeTattooArtist;
        }
        public dynamic GetAllActiveTattooArtistWithoutAll()
        {
            var activeTattooArtist = _unitOfWork.Users.ArtistListWithoutAll();
            return activeTattooArtist;
        }
        public bool ValidateUserName(string username)
        {
           var item = _unitOfWork.Users.GetFirstOrDefault(p => p.UserName == username).Data;
            return item == null ? true : false;
        }
        public bool ValidateEmail(string email)
        {
            var item = _unitOfWork.Users.GetFirstOrDefault(p => p.Email == email).Data;
            return item == null ? true : false;
        }
        public bool ValidatePhoneNumber(string phoneNumber)
        {
            var item = _unitOfWork.Users.GetFirstOrDefault(p => p.PhoneNumber == phoneNumber).Data;
            return item == null ? true : false;
        }
        public ResponseDto<User> GetSuperadminUser()
        {
            _responseDto = _unitOfWork.Users.GetAllSuperAdmin();
            return _responseDto;
        }
    }
}

