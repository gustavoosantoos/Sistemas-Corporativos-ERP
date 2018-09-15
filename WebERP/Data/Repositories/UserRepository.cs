using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebERP.Extensions;
using WebERP.Models;
using WebERP.Utils.Identity;

namespace WebERP.Data.Repositories
{
    public class UserRepository
    {
        private UserManager<ApplicationUser> Manager { get; }

        public UserRepository(UserManager<ApplicationUser> manager)
        {
            Manager = manager;
        }

        public ApplicationUser FindById(string userId)
        {
            return Manager.Users.FirstOrDefault(e => e.Id == userId);
        }

        public IdentityResult Create(ApplicationUser user, string password)
        {
            var createTaskAsync = Manager.CreateAsync(user, password);
            createTaskAsync.Wait();
            return createTaskAsync.Result;
        }

        public void Delete(ApplicationUser user)
        {
            Manager.DeleteAsync(user).Wait();
        }

        public List<ApplicationUser> Users()
        {
            return Manager.Users.ToList();
        }

        public void AddClaim(ApplicationUser user, Claim claim)
        {
            Manager.AddClaimAsync(user, claim).Wait();
        }

        public void AddClaims(ApplicationUser user, IEnumerable<Claim> claims)
        {
            Manager.AddClaimsAsync(user, claims).Wait();
        }

        public void RemoveClaim(ApplicationUser user, Claim claim)
        {
            Manager.RemoveClaimAsync(user, claim).Wait();
        }

        public void RemoveClaims(ApplicationUser user, IEnumerable<Claim> claims)
        {
            Manager.RemoveClaimsAsync(user, claims).Wait();
        }

        public ApplicationUser GetUser(ClaimsPrincipal claims)
        {
            var currentUserTask = Manager.GetUserAsync(claims);
            currentUserTask.Wait();
            return currentUserTask.Result;
        }

        public IList<Claim> GetUserClaims(ApplicationUser user)
        {
            var userClaimsTask = Manager.GetClaimsAsync(user);
            userClaimsTask.Wait();
            return userClaimsTask.Result;
        }

        public IList<string> GetUserRoles(ApplicationUser user)
        {
            return GetUserClaims(user).Roles();
        }

        public IList<ErpRole> GetUserErpRoles(ApplicationUser user)
        {
            return GetUserClaims(user).ClaimToErpRoles();
        }

        public ErpRole GetHigherRole(ApplicationUser user)
        {
            IList<Claim> claims = GetUserClaims(user);
            
            var roles = claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();

            ErpRole higherRole = ErpRolesManager.GetHigherRole(roles);

            return higherRole;
        }

        public IList<ErpRole> GetAuthorizedRolesToCreate(ApplicationUser user)
        {
            return GetUserErpRoles(user).AuthorizedRolesToCreate();
        }

        public void SetUserAsSuperAdmin(ApplicationUser user)
        {
            AddClaim(user, new Claim(ClaimTypes.Role, ErpRoleNames.SuperAdmin));
        }

        public IEnumerable<ApplicationUser> GetSupervisoresDeVendas()
        {
            var superAdminsTask = Manager.GetUsersForClaimAsync(new Claim(ClaimTypes.Role, ErpRoleNames.SuperAdmin));
            var supervisoresTask = Manager.GetUsersForClaimAsync(new Claim(ClaimTypes.Role, ErpRoleNames.SupervisorDeCompras));

            superAdminsTask.Wait();
            supervisoresTask.Wait();

            var distinctUsers = superAdminsTask.Result
                .Concat(supervisoresTask.Result)
                .GroupBy(u => u.Email)
                .Select(g => g.First())
                .ToList();

            return distinctUsers;
        }
    }
}
