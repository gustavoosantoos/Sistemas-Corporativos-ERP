using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebERP.Data.Repositories;
using WebERP.Extensions;
using WebERP.Models;

namespace WebERP.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public HomeController(UserManager<ApplicationUser> userManager, UserRepository repository, IHttpContextAccessor accessor) 
            : base(userManager, repository, accessor)
        {

        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
