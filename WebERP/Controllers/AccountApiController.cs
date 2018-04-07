using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebERP.Extensions;
using WebERP.Models;
using WebERP.Utils.Identity;

namespace WebERP.Controllers
{
    [Authorize(Roles = ErpRoleNames.SuperAdmin)]
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountApiController : BaseController
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountApiController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IHttpContextAccessor accessor) : base(userManager, accessor)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAdminUser([FromQuery] string email, [FromQuery] string password)
        {
            var user = new ApplicationUser { UserName = "admin", Email = email };

            if (user.UserName == "admin")
            {
                user.Nome = "Teste";
                user.Sobrenome = "1";
            }

            var result = await UserManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                if (user.UserName == "admin")
                    new ErpRolesManager().SetUserAsSuperAdmin(UserManager, user);

                await _signInManager.SignInAsync(user, isPersistent: false);
                return Created("", null);
            }

            return BadRequest(result);
        }

        [HttpDelete, Route("{id}", Name = "DeleteUser")]
        public async Task<IActionResult> Delete(string id)
        {
            var currentUser = await UserManager.GetUserAsync(User);
            if (currentUser.HigherRole(UserManager) != ErpRoles.SuperAdmin)
                return Unauthorized();

            var userToDelete = UserManager.Users.FirstOrDefault(e => e.Id == id);
            if (userToDelete == null)
                return NotFound("Usuário não encontrado.");

            await UserManager.DeleteAsync(userToDelete);

            return Ok();
        }
    }
}