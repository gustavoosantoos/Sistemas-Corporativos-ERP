﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebERP.Models;
using WebERP.Utils.Identity;

namespace WebERP.Controllers.Api
{
    [Authorize(Roles = ErpRoleNames.SuperAdmin)]
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : BaseController
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController (SignInManager<ApplicationUser> signInManager, CurrentUtils current) : base(current)
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

            var result = UserRepository.Create(user, password);
            if (!result.Succeeded)
                return BadRequest(result);
            
            if (user.UserName == "admin")
                UserRepository.SetUserAsSuperAdmin(user);

            UserRepository.SetUserAsSuperAdmin(user);

            await _signInManager.SignInAsync(user, isPersistent: false);
            return Created("", null);

        }

        [HttpDelete, Route("{id}", Name = "DeleteUser")]
        public IActionResult Delete(string id)
        {
            if (UserRepository.GetHigherRole(CurrentUser) != ErpRolesManager.SuperAdmin)
                return Unauthorized();

            var userToDelete = UserRepository.FindById(id);
            if (userToDelete == null)
                return NotFound("Usuário não encontrado.");

            UserRepository.Delete(userToDelete);
            return Ok();
        }
    }
}