﻿using BusinessManagementSystem.Data;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Enums;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;
using BusinessManagementSystem.Utility;
using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementSystem.Repositories
{
    public class UserRepository : GenericRepository<User>, IUser
    {
        public ResponseDto<User> _responseDto;
        public ResponseDto<UserRoleDto> _responseDtoUserRole;
        public ResponseDto<UserDto> _responseDtoUser;
        public UserRepository(ApplicationDBContext dbContext) : base(dbContext) 
        {
            _responseDto = new ResponseDto<User>();
            _responseDtoUserRole = new ResponseDto<UserRoleDto>();
            _responseDtoUser=new ResponseDto<UserDto>();
        }
        public List<User> GetAllActiveUsers()
        {
            List<User> activeUsers = _dbContext.Users.Where(p => p.Status == true).ToList();
            return activeUsers;
        }
        public List<User> GetAllInactiveUsers()
        {
            List<User> activeUsers = _dbContext.Users.Where(p => p.Status == false).ToList();
            return activeUsers;
        }
        public ResponseDto<UserRoleDto> GetAllUser(string filter)
        {
            if (filter == SD.Role_Superadmin)
            {
                _responseDtoUserRole.Datas = (from u in _dbContext.Users
                                              join ur in _dbContext.UserRoles on u.Id equals ur.UserId
                                              join r in _dbContext.Roles on ur.RoleId equals r.Id
                                              select new UserRoleDto
                                              {
                                                  User = u,
                                                  RoleName = r.Name
                                              }).ToList();
            }
            else if(filter==SD.Role_TattooAdmin)
            {
                _responseDtoUserRole.Datas = (from u in _dbContext.Users
                                              join ur in _dbContext.UserRoles on u.Id equals ur.UserId
                                              join r in _dbContext.Roles on ur.RoleId equals r.Id
                                              where u.Occupation == SD.Occupations[Occupation.TattooArtist.ToString()] //this is the correct one and implement similar in all below
                                              select new UserRoleDto
                                              {
                                                  User = u,
                                                  RoleName = r.Name
                                              }).ToList();
            }
            else if (filter == SD.Role_KaffeAdmin)
            {
                _responseDtoUserRole.Datas = (from u in _dbContext.Users
                                              join ur in _dbContext.UserRoles on u.Id equals ur.UserId
                                              join r in _dbContext.Roles on ur.RoleId equals r.Id
                                              where r.Name == SD.Role_KaffeAdmin || u.Occupation == "Barista"
                                              select new UserRoleDto
                                              {
                                                  User = u,
                                                  RoleName = r.Name
                                              }).ToList();
            }
            else if (filter == SD.Role_ApartmentAdmin)
            {
                _responseDtoUserRole.Datas = (from u in _dbContext.Users
                                              join ur in _dbContext.UserRoles on u.Id equals ur.UserId
                                              join r in _dbContext.Roles on ur.RoleId equals r.Id
                                              where r.Name == SD.Role_ApartmentAdmin || r.Name == SD.Role_ApartmentEmployee
                                              select new UserRoleDto
                                              {
                                                  User = u,
                                                  RoleName = r.Name
                                              }).ToList();
            }
            
            return _responseDtoUserRole;
        }
        public dynamic RoleList()
        {
            var roleLIst = _dbContext.Roles.Select(p => new { Id = p.Id, Name = p.Name }).ToList();
            return roleLIst;
        }
        public dynamic ArtistList()
        {
            var artistList = _dbContext.Users.Where(p=>p.Occupation.Equals("Tattoo Artist") && p.Status==true).Select(p => new { Id = p.Id, Name = p.FullName }).ToList().OrderBy(x=>x.Name);
            artistList= artistList.Union(new[] { new { Id = 0, Name = "All" } }).OrderBy(x => x.Name);
            return artistList;
        }
        public dynamic ArtistListWithoutAll()
        {
            var artistList = _dbContext.Users.Where(p => p.Occupation.Equals("Tattoo Artist") && p.Status == true).Select(p => new { Id = p.Id, Name = p.FullName }).ToList().OrderBy(x => x.Name);
            return artistList;
        }

        public dynamic ArtistSelf(Guid guid)
        {
            var artistList = _dbContext.Users.Where(p => p.Occupation.Equals("Tattoo Artist") && p.Status == true && p.Guid==guid).Select(p => new { Id = p.Id, Name = p.FullName }).ToList();
            return artistList;
        }

        public ResponseDto<User> GetAllSuperAdmin()
        {
            _responseDto.Datas = (from u in _dbContext.Users
                                join ur in _dbContext.UserRoles on u.Id equals ur.UserId
                                join r in _dbContext.Roles on ur.RoleId equals r.Id
                                where r.Name == "superadmin"
                                select u).ToList();
            return _responseDto;
            
        }
    }
}
