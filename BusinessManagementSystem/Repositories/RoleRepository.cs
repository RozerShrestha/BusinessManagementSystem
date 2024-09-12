using BusinessManagementSystem.Data;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;
using System.Net;

namespace BusinessManagementSystem.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRole
    {
        private readonly ApplicationDBContext _db;
        public RoleRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

        public dynamic GetRoles()
        {
            var roleList = _db.Roles.Select(p => new { p.Id, p.Name }).ToList();
            roleList.Add(new { Id = 0, Name = "--Select--" });
            roleList.Sort((a, b) => a.Id.CompareTo(b.Id));
            return roleList;
        }
        
    }

}
