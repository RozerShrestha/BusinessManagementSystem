using AspNetCore;
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
        ResponseDto<User> _responseDto;

        public UserImpl(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _responseDto = new ResponseDto<User>();
        }
        public ResponseDto<User> GetAllUser()
        {
            var response = _unitOfWork.Users.GetAll();
            return response;
        }
        public ResponseDto<User> GetUserById(int id)
        {
            var response = _unitOfWork.Users.GetFirstOrDefault(p =>p.Id==id);
            return response;
        }
        public ResponseDto<User> GetAllActiveUsers()
        {
            var response = _unitOfWork.Users.GetAll(p => p.Status == true);
            return response;
        }
        public ResponseDto<User> GetAllInactiveUsers()
        {
            var response = _unitOfWork.Users.GetAll(p => p.Status == false);
            return response;
        }
        public ResponseDto<User> Create(UserDto userDto)
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
            u.CreatedBy = BaseController.username; //accessing static variables from BaseController
            u.UpdatedBy = BaseController.username;
            _responseDto = _unitOfWork.Users.Insert(u);
            return _responseDto;
        }
        public ResponseDto<User> Update(User u)
        {
             u.UpdatedBy = BaseController.username;
             _responseDto= _unitOfWork.Users.Update(u);
             return _responseDto;
        }
        public ResponseDto<User> Delete(int id)
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
    }
}

