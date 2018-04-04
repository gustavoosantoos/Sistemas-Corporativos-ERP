using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebERP.Data;
using WebERP.Extensions;
using WebERP.Models;
using WebERP.Utils.Identity;
using WebERP.ViewModels.ManageViewModels;

namespace WebERP.Controllers
{
    [Authorize(Roles = ErpRoleNames.SuperAdmin)]
    public class UsersController : BaseController
    {
        public UsersController(UserManager<ApplicationUser> userManager, IHttpContextAccessor accessor) : base(userManager, accessor)
        {
        }

        public IActionResult Index()
        {
            var users = UserManager.Users.ToList().Select(e => new UserViewModel()
            {
                Nome = e.Nome,
                Sobrenome = e.Sobrenome,
                Username = e.UserName,
                HigherRole = e.HigherRole(UserManager)
            }).ToList();

            return View(users);
        }
    }
}