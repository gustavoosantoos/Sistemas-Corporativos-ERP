using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebERP.Extensions;
using WebERP.Models;

namespace WebERP.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public HomeController(UserManager<ApplicationUser> userManager, IHttpContextAccessor accessor) : base(userManager, accessor)
        {
        }

        public IActionResult Index()
        {
            var roles = User.Roles();

            return View();
        }

    }
}
