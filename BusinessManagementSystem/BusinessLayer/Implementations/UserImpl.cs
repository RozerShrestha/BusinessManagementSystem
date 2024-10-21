using AspNetCore;
using AutoMapper;
using BusinessManagementSystem.BusinessLayer.Services;
using BusinessManagementSystem.Controllers;
using BusinessManagementSystem.Data;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Repositories;
using BusinessManagementSystem.Services;
using BusinessManagementSystem.Utility;
using Microsoft.EntityFrameworkCore;
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


        public UserImpl(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _responseDto = new ResponseDto<User>();
            _responseUserRole = new ResponseDto<UserRoleDto>();
            _responseUserDto = new ResponseDto<UserDto>();
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
        public ResponseDto<UserDto> GetUserByGuid(Guid guid)
        {
            try
            {
                UserDto userDto = new();
                _responseDto = _unitOfWork.Users.GetFirstOrDefault(p => p.Guid == guid, includeProperties: "UserRoles.Role");
                userDto = _mapper.Map<UserDto>(_responseDto.Data);
                _responseUserDto.Data = userDto;
                
            }
            catch (Exception ex)
            {
                _responseUserDto.StatusCode = HttpStatusCode.InternalServerError;
                _responseUserDto.Message=_responseDto.Message +"Exception:"+ ex.Message;
                
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
            var hashInfo = Helper.Helpers.GetHashPassword(userDto.Password);
            List<UserRole> urList = new List<UserRole>();
            urList.Add(new UserRole {RoleId = userDto.RoleId });
            User u = new User();
            u = _mapper.Map<User>(userDto);
            u.HashPassword = hashInfo.Hash;
            u.Status = true;
            u.Salt=hashInfo.Salt;
            u.FirstPasswordReset = false;
            u.UserRoles = urList;
            _responseDto = _unitOfWork.Users.Insert(u);
            return _responseDto;
        }
        public ResponseDto<User> UpdateUser(UserDto userDto)
        {
            var item=_unitOfWork.Users.GetFirstOrDefault(p=>p.)
            _responseDto = _unitOfWork.Users.Update(u);
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
    }
}

