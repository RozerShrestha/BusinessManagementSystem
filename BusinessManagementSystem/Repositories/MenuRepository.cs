using BusinessManagementSystem.Data;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;
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
        public ResponseDto<Menu> CreateMenu(Menu menu)
        {

            return _responseDto;
        }


        public ResponseDto<Menu> UpdateMenu(Menu menu)
        {
            
            return _responseDto;
        }
    }
}
