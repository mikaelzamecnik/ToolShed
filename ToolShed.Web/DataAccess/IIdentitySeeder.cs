using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToolShed.Web.DataAccess
{
    public interface IIdentitySeeder
    {
        Task<bool> CreateAdminAccountIfEmpty();
    }
}
