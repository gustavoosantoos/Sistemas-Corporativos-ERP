using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebERP.Models;

namespace WebERP.Utils.Identity
{
    public class ErpRolesManager
    {
        public void SetUserAsSuperAdmin(UserManager<ApplicationUser> manager, ApplicationUser user)
        {
            manager.AddClaimAsync(user, new Claim(ClaimTypes.Role, ErpRoleNames.SuperAdmin));
        }
    }
}
