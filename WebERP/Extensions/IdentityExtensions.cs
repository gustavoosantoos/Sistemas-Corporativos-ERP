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

        public static List<string> Roles(this IList<Claim> claims)
        {
            return claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();
        }

        public static List<ErpRole> ClaimToErpRoles(this IList<Claim> claims)
        {
            return claims.Roles().Select(ErpRoles.RoleForName).ToList();
        }

        public static ErpRole HigherRole(this ApplicationUser user, UserManager<ApplicationUser> manager)
        {
            Task<IList<Claim>> claims = manager.GetClaimsAsync(user);
            claims.Wait();

            var roles = claims.Result
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();

            ErpRole higherRole = GetHigherRole(roles);

            return higherRole;
        }

        public static ErpRole GetHigherRole(List<string> roles)
        {
            return roles.Select(ErpRoles.RoleForName).OrderBy(e => e.Nivel).FirstOrDefault();
        }

        public static List<ErpRole> AuthorizedRolesToCreate(this ErpRole role)
        {
            var allRoles = ErpRoles.GetAllRoles();
            if (role.RoleName == ErpRoleNames.SuperAdmin)
                return allRoles;

            var authorizedRoles = allRoles
                .Where(e => e.Departamento == role.Departamento && e.Nivel >= role.Nivel)
                .ToList();
            return authorizedRoles;
        }

        public static List<ErpRole> AuthorizedRolesToCreate(this IList<ErpRole> roles)
        {
            var allRoles = ErpRoles.GetAllRoles();
            if (roles.Contains(ErpRoles.SuperAdmin))
                return allRoles;

            var authorizedRoles = new List<ErpRole>();

            foreach (var role in roles.GroupBy(e => e.Departamento))
            {
                var higherRoleInDep = GetHigherRole(role.Select(e => e.RoleName).ToList());
                authorizedRoles.AddRange(higherRoleInDep.AuthorizedRolesToCreate());
            }

            return authorizedRoles;
        }

        public static bool IsInAnyRole(this ClaimsPrincipal claimsPrincipal, params string[] roles)
        {
            return roles.Any(claimsPrincipal.IsInRole);
        }
    }
}
