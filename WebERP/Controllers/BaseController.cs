using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebERP.Extensions;
using WebERP.Models;

namespace WebERP.Controllers
{
    public class BaseController : Controller
    {
        public BaseController(UserManager<ApplicationUser> userManager, IHttpContextAccessor accessor)
        {
            UserManager = userManager;
            Accessor = accessor;
            CodigoUsuario = userManager.GetUserId(Accessor.HttpContext.User);
            UserRoles = Accessor.HttpContext.User.Roles();
        }

        protected readonly UserManager<ApplicationUser> UserManager;
        protected readonly IHttpContextAccessor Accessor;
        protected readonly string CodigoUsuario;
        protected IEnumerable<string> UserRoles;
    }
}