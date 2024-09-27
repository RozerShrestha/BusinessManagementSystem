using AspNetCore;
using BusinessManagementSystem.BusinessLayer.Services;
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
        ResponseDto<User> _responseDto;

        public UserImpl(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _responseDto = new ResponseDto<User>();
        }

        public ResponseDto<User> Create(UserDto userDto)
        {
            try
            {
                var hashInfo = Helper.Helpers.GetHashPassword(userDto.Password);
                List<UserRole> urList = new List<UserRole>();
                urList.Add(new UserRole {RoleId = userDto.RoleId });
                User u = new User();
                u.Guid = Helper.Helpers.GenerateGUID();
                u.UserName = userDto.UserName;
                u.Email = userDto.Email;
                u.FullName = userDto.FullName;
                u.DateOfBirth = DateOnly.Parse(userDto.DateOfBirth);
                u.Gender=userDto.Gender;
                u.Address=userDto.Address;
                u.PhoneNumber = userDto.MobileNumber;
                u.Occupation=userDto.Occupation;
                u.HashPassword = hashInfo.Hash;
                u.Status = true;
                u.Salt=hashInfo.Salt;
                u.UserRoles = urList;
                u.CreatedBy = "system";
                u.UpdatedBy = "system";
                _unitOfWork.BeginTransaction();
                _responseDto = _unitOfWork.Users.Insert(u);
                _unitOfWork.SaveChanges();
                _unitOfWork.Commit();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _unitOfWork.Rollback();
                _responseDto.StatusCode = HttpStatusCode.BadRequest;
                _responseDto.Message = ex.Message;
            }

            return _responseDto;
        }

        public ResponseDto<User> Update(User u)
        {
            try
            {
                _unitOfWork.BeginTransaction();
               _responseDto= _unitOfWork.Users.Update(u);
                _unitOfWork.SaveChanges();
                _unitOfWork.Commit();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _unitOfWork.Rollback();
                _responseDto.StatusCode = HttpStatusCode.BadRequest;
                _responseDto.Message=ex.Message;
            }

            return _responseDto;
        }

        public ResponseDto<User> Delete(int id)
        {
            var user = _unitOfWork.Users.GetById(id);
            if (user.Data != null)
            {
                try
                {
                    _unitOfWork.BeginTransaction();
                    _responseDto = _unitOfWork.Users.Delete(user.Data);
                    _unitOfWork.SaveChanges();
                    _unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    _unitOfWork.Rollback();
                    _responseDto.StatusCode = HttpStatusCode.BadRequest;
                    _responseDto.Message = ex.Message;
                }
            }
            else
            {
                _responseDto.StatusCode = HttpStatusCode.NotFound;
                _responseDto.Message = "User Not Found";
            }
            return _responseDto;
        }

        public ResponseDto<User> GetAllActiveUsers()
        {
            _unitOfWork.BeginTransaction();
            var response =_unitOfWork.Users.GetAll(p => p.Status == true);
            _unitOfWork.Commit();
            return response;
        }

        public ResponseDto<User> GetAllInactiveUsers()
        {
            _unitOfWork.BeginTransaction();
            var response = _unitOfWork.Users.GetAll(p => p.Status == false);
            _unitOfWork.Commit();
            return response;
        }

        public ResponseDto<User> GetAllUser()
        {
            _unitOfWork.BeginTransaction();
            var response = _unitOfWork.Users.GetAll();
            _unitOfWork.Commit();
            return response;
        }

        public ResponseDto<User> GetUserById(int id)
        {
            _unitOfWork.BeginTransaction();
            var response = _unitOfWork.Users.GetFirstOrDefault(p => p.Status == false);
            _unitOfWork.Commit();
            return response;
        }

        

       
    }
}
