using BusinessManagementSystem.Data;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BusinessManagementSystem.Repositories
{

    public class MenuRepository : GenericRepository<Menu>, IMenu
    {
        private readonly ApplicationDBContext _db;
        public ResponseDto<Menu> _responseDto;

        public MenuRepository(ApplicationDBContext db) : base(db)
        {
            _responseDto = new ResponseDto<Menu>();
            _db = db;
        }
        public dynamic ParentList()
        {
            var parentList = _db.Menus.Where(p => p.Parent == 0 && p.Status == true).Select(p => new { Parent = p.Id, p.Name }).ToList();
            parentList.Add(new { Parent = 0, Name = "Main Parent" });
            parentList.Sort((a, b) => a.Parent.CompareTo(b.Parent));
            return parentList;
        }
        public dynamic RoleList()
        {
            var roleLIst = _db.Roles.Select(p=> new { Id=p.Id, Name=p.Name }).ToList();
            return roleLIst;
        }
        public ResponseDto<Menu> CreateMenu(Menu menu)
        {
            try
            {
                List<Role> selectedRoles = null;
                if (menu.SelectedRoles.Contains("0")) //means selected All
                {
                    selectedRoles = _db.Roles.ToList();
                }
                else
                {
                    selectedRoles = _db.Roles.Where(p => menu.SelectedRoles.Contains(p.Id.ToString())).ToList();

                }
                _db.Database.BeginTransaction();
                foreach (var role in selectedRoles)
                {
                    MenuRole menuRole = new()
                    {
                        Role = role,
                        Menu = menu
                    };
                    _db.MenuRoles.Add(menuRole);
                }
                _db.SaveChanges();
                _db.Database.CommitTransaction();

            }
            catch (Exception ex)
            {
                _responseDto.StatusCode = HttpStatusCode.InternalServerError;
                _responseDto.Message = ex.ToString();
                
            }
            return _responseDto;
        }


        public ResponseDto<Menu> UpdateMenu(Menu menu)
        {
            
            return _responseDto;
        }

        
    }
}
