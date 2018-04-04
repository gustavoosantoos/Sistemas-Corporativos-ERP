using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebERP.Models;
using WebERP.Utils.Identity;

namespace WebERP.Extensions
{
    public static  class IdentityExtensions
    {
        public static List<string> Roles(this ClaimsPrincipal identity)
        {
            return identity.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();
        }

        public static ErpRole HigherRole(this ApplicationUser user, UserManager<ApplicationUser> manager)
        {
            Task<IList<Claim>> claims = manager.GetClaimsAsync(user);
            claims.Wait();

            var roles = claims.Result
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();
            
            ErpRole higherRole = null;

            foreach (var roleName in roles)
            {
                var role = ErpRoles.RoleForName(roleName);

                if (higherRole == null)
                    higherRole = role;

                if (role.Nivel > higherRole.Nivel)
                    higherRole = role;
            }

            return higherRole;
        }
    }
}
