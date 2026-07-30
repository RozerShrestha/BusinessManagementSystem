using TattooAppointmentSystem.Data;
using TattooAppointmentSystem.Dto;
using TattooAppointmentSystem.Models;
using TattooAppointmentSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data;
using System.Linq;
using System.Net;

namespace TattooAppointmentSystem.Repositories
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
        public async Task<ResponseDto<Menu>> CreateMenu(Menu menu)
        {
            try
            {
                List<Role> selectedRoles = null;
                var selectedRoles1 = menu.Multiselect.SelectedItems.ToList();
                selectedRoles = _db.Roles.Where(p => selectedRoles1.Contains(p.Id)).ToList();
                await _db.Database.BeginTransactionAsync();
                foreach (var role in selectedRoles)
                {
                    MenuRole menuRole = new()
                    {
                        Role = role,
                        Menu = menu
                    };
                    await _db.MenuRoles.AddAsync(menuRole);
                }
                await _db.SaveChangesAsync();
                await _db.Database.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                _responseDto.StatusCode = HttpStatusCode.InternalServerError;
                _responseDto.Message = ex.ToString();
                _db.Database.RollbackTransaction();  
            }
            return _responseDto;
        }
        public ResponseDto<Menu> GetMenuById(int id)
        {
            return _responseDto;
        }
        public ResponseDto<Menu> UpdateMenu(Menu menu)
        {
            try
            {
                _db.Database.BeginTransaction();
                //_db.MenuRoles.RemoveRange(menu.MenuRoles);
                var previousMenuRoles = _db.Menus.Include(m => m.MenuRoles).Where(p => p.Id == menu.Id).SingleOrDefault();
                List<Role> selectedRoles = null;
                var selectedRoles1 = menu.Multiselect.SelectedItems.ToList();
                selectedRoles = _db.Roles.Where(p => selectedRoles1.Contains(p.Id)).ToList();

                _db.MenuRoles.RemoveRange(previousMenuRoles.MenuRoles);
                foreach (var role in selectedRoles)
                {
                    MenuRole menuRole = new()
                    {
                        RoleId = role.Id,
                        MenuId = menu.Id
                    };
                    _db.MenuRoles.AddRange(menuRole);
                    //_db.SaveChanges();
                }
                var menuToUpdate = _db.Menus.Where(m => m.Id == menu.Id).SingleOrDefault();
                _db.Entry(menuToUpdate).CurrentValues.SetValues(menu);
                _db.Entry(menuToUpdate).State = EntityState.Modified;
                _db.SaveChanges();
                _db.Database.CommitTransaction();
            }
            catch (Exception ex)
            {
                _responseDto.StatusCode = HttpStatusCode.InternalServerError;
                _responseDto.Message = ex.ToString();
                _db.Database.RollbackTransaction();
            }
            return _responseDto;
        }
        public async Task<ResponseDto<Menu>> GetAllMenuAsync()
        {
            var groupedData = await (from m in _db.Menus
                                     join mr in _db.MenuRoles on m.Id equals mr.MenuId
                                     join r in _db.Roles on mr.RoleId equals r.Id
                                     group r by new { m.Id, m.Parent, m.Name, m.Url, m.Sort, m.Status, m.Icon } into g
                                     select new
                                     {
                                         g.Key.Id,
                                         g.Key.Parent,
                                         g.Key.Name,
                                         g.Key.Url,
                                         g.Key.Sort,
                                         g.Key.Status,
                                         g.Key.Icon,
                                         RoleNames = g.Select(x => x.Name).Distinct()
                                     })
                             .ToListAsync();  // 👈 Only database-translatable parts here

            var result = groupedData.Select(g => new Menu
            {
                Id = g.Id,
                Parent = g.Parent,
                Name = g.Name,
                Url = g.Url,
                Sort = g.Sort,
                Status = g.Status,
                Icon = g.Icon,
                Roles = string.Join(", ", g.RoleNames) // 👈 This runs in memory (safe)
            })
            .OrderBy(x => x.Id)
            .ToList();
            _responseDto.Datas = result;
            return _responseDto;
        }
    }
}

