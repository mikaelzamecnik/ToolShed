using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolShed.Web.DataAccess;
using Microsoft.AspNetCore.Identity;

namespace ToolShed.Web.DataAccess
{
    public class IdentitySeeder:IIdentitySeeder
    {
        private const string _admin = "admin";
        private const string _password = "buggeroff";

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IdentitySeeder(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> CreateAdminAccountIfEmpty()
        {
            if(!_context.Users.Any(u => u.UserName == _admin))
            {
                await _userManager.CreateAsync(new IdentityUser {
                    UserName = _admin,
                    Email = "admin@example.com",
                    EmailConfirmed = true
                }, _password);
            }
            //TODO Seed admin role and add_admin to this role
            return true;
        }
    }
}
