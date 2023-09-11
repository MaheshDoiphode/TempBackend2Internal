using System.Collections.Generic;
using System.Linq;
using DomainProject.DomainModels;
using InfraProject.Context;
using Microsoft.AspNetCore.Identity;

namespace InfraProject.Repositories
{
    public class UserRepository
    {
        private readonly VisitorManagementDbContext _context;
        private readonly UserManager<User> _userManager;

        public UserRepository(VisitorManagementDbContext context, UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public void CreateAdminUser(User adminUser)
        {
            adminUser.RoleID = "Admin";
            _context.Users.Add(adminUser);
            _context.SaveChanges();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public void UpdateUserRole(string userId, string newRoleId)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                user.RoleID = newRoleId;
                _context.SaveChanges();
            }
        }

        public User GetUserById(string id)
        {
            return _context.Users.Find(id);
        }
        public IEnumerable<User> GetUsersByRole(string role)
        {
            return _context.Users.Where(u => u.RoleID == role).ToList();
        }
    }
}
