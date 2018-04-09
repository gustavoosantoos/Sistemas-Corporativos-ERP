using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebERP.Data.Repositories;
using WebERP.Extensions;
using WebERP.Models;
using WebERP.Utils;

namespace WebERP.Controllers
{
    public class BaseController : Controller
    {
        protected readonly CurrentUtils Current;

        protected UserManager<ApplicationUser> UserManager => Current.UserManager;
        protected UserRepository UserRepository => Current.UserRepository;
        protected IHttpContextAccessor Acessor => Current.Accessor;

        protected readonly string UserId;
        protected ApplicationUser CurrentUser;
        protected IEnumerable<string> UserRoles;

        public BaseController(CurrentUtils current)
        {
            Current = current;
            UserId = Current.UserManager.GetUserId(Current.Accessor.HttpContext.User);
            UserRoles = Current.Accessor.HttpContext.User.Roles();
            CurrentUser = Current.UserRepository.GetUser(Current.Accessor.HttpContext.User);
        }

        protected void RegisterSweetAlertMessage(string messageTitle, string message, MessageType type)
        {
            SweetAlertBuilder alert = new SweetAlertBuilder(messageTitle, message, type);
            ScriptManager.SetStartupScript(TempData, alert.BuildScript());
        }
        
        protected void RegisterBootstraAlertMessage(string title, string message, MessageType type)
        {
            BootstrapAlertBuilder alert = new BootstrapAlertBuilder(title, message, type);
            ScriptManager.SetStartupScript(TempData, alert.BuildScript());
        }
    }
}