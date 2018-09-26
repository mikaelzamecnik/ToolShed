using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolShed.Web.DataAccess;
using Microsoft.AspNetCore.Identity;

namespace ToolShed.Web.DataAccess
{
    public class IdentitySeeder : IIdentitySeeder
    {
        private const string _admin = "admin@example.com";
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
            try
            {
                if (!_context.Users.Any(u => u.UserName == _admin))
                {
                    var user = new IdentityUser
                    {
                        UserName = _admin,
                        Email = "admin@example.com",
                        EmailConfirmed = true
                    };

                    var result = await _userManager.CreateAsync(user, _password);
                    var test = result.Succeeded;
                }

            }
            catch (Exception ex )
            {

                throw;
            }



            //TODO: Seed admin role and add _admin to this rolesdsdf

            return true;
        }
    }
}
