using System.Collections.Generic;
using System.Linq;
using DomainProject.DomainModels;
using InfraProject.Context;
using Microsoft.EntityFrameworkCore;

namespace InfraProject.Repositories
{
    public class RoleRepository
    {
        private readonly VisitorManagementDbContext _context;

        public RoleRepository(VisitorManagementDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Role> GetAllRoles()
        {
            return _context.Roles.ToList();
        }

        public Role GetRoleById(string id)
        {
            return _context.Roles.Find(id);
        }

        public void AddRole(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
        }

        public void UpdateRole(Role role)
        {
            _context.Entry(role).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteRole(string id)
        {
            var role = _context.Roles.Find(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                _context.SaveChanges();
            }
        }

        public void AddOrUpdateRole(Role role)
        {
            var existingRole = _context.Roles.Find(role.RoleID);
            if (existingRole != null)
            {
                _context.Entry(existingRole).CurrentValues.SetValues(role);
            }
            else
            {
                _context.Roles.Add(role);
            }
            _context.SaveChanges();
        }
        public IEnumerable<Role> GetRolesByPermission(string permission)
        {
            return _context.Roles.Where(r => r.Permissions.Contains(permission)).ToList();
        }
    }
}
