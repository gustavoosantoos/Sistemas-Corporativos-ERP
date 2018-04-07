using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebERP.Data;
using WebERP.Extensions;
using WebERP.Models;
using WebERP.Utils;
using WebERP.Utils.Identity;
using WebERP.ViewModels.AccountViewModels;
using WebERP.ViewModels.ManageViewModels;

namespace WebERP.Controllers
{
    //[Authorize(Roles = ErpRoleNames.SuperAdmin)]
    public class UsersController : BaseController
    {
        public UsersController(UserManager<ApplicationUser> userManager, IHttpContextAccessor accessor) : base(userManager, accessor)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["GerenciamentoDeUsuarios"] = "active";

            var currentUser = await UserManager.GetUserAsync(User);

            var users = UserManager.Users.ToList().Select(e => new UserViewModel()
            {
                Id = e.Id,
                Nome = e.Nome,
                Sobrenome = e.Sobrenome,
                Username = e.UserName,
                PhoneNumber = e.PhoneNumber,
                HigherRole = e.HigherRole(UserManager)
            }).Where(e => currentUser.HigherRole(UserManager).Nivel < e.HigherRole.Nivel).ToList();

            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> UserForm(string id)
        {
            ViewData["GerenciamentoDeUsuarios"] = "active";

            RegisterViewModel viewModel = new RegisterViewModel();
            var currentUser = await UserManager.GetUserAsync(User);

            if (!string.IsNullOrWhiteSpace(id))
            {
                var userToEdit = UserManager.Users.FirstOrDefault(e => e.Id == id);
                if (userToEdit == null)
                    return BadRequest();

                var roles = UserManager.GetClaimsAsync(userToEdit);

                viewModel.Nome = userToEdit.Nome;
                viewModel.Sobrenome = userToEdit.Sobrenome;
                viewModel.UserName = userToEdit.UserName;
                viewModel.Email = userToEdit.Email;

                roles.Wait();
                viewModel.UserRoles = roles.Result.Roles();
            }

            var currentUserClaims = await UserManager.GetClaimsAsync(currentUser);
            ViewBag.AvailableRoles = new MultiSelectList(
                 currentUserClaims.ClaimToErpRoles().AuthorizedRolesToCreate(),
                 nameof(ErpRole.RoleName), nameof(ErpRole.FormmatedName), viewModel.UserRoles);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UserForm(RegisterViewModel model)
        {
            ViewData["GerenciamentoDeUsuarios"] = "active";

            var currentUser = await UserManager.GetUserAsync(User);

            if (!model.IsNewUser)
            {
                ModelState.Remove("Password");
            }

            if (ModelState.IsValid == false)
            {
                ViewBag.AvailableRoles = new MultiSelectList(
                    currentUser.HigherRole(UserManager).AuthorizedRolesToCreate(),
                    nameof(ErpRole.RoleName), nameof(ErpRole.FormmatedName), model.UserRoles);

                return View(model);
            }

            if (model.IsNewUser)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome
                };

                var result = await UserManager.CreateAsync(user, model.Password);
                await UserManager.AddClaimsAsync(user, model.UserRoles.Select(e => new Claim(ClaimTypes.Role, e)));

                SweetAlertBuilder alert = new SweetAlertBuilder(
                    "Usuário criado",
                    $"O usuário {user.UserName} foi criado com sucesso",
                    MessageType.Success);
                ScriptManager.SetStartupScript(TempData, alert.BuildScript());

                return RedirectToAction(actionName: nameof(Index));
            }
            else
            {
                var user = UserManager.Users.FirstOrDefault(e => e.Id == model.Id);
                user.Nome = model.Nome;
                user.Sobrenome = model.Sobrenome;
                user.UserName = model.UserName;
                user.Email = model.Email;

                var currentClaims = await UserManager.GetClaimsAsync(user);
                var currentRoles = currentClaims.Roles();

                if (currentRoles.Count > model.UserRoles.Count)
                {
                    currentRoles.RemoveAll(e => model.UserRoles.Contains(e));
                    await UserManager.RemoveClaimsAsync(user,
                        currentRoles.Select(e => new Claim(ClaimTypes.Role, e)));
                }
                else
                {
                    model.UserRoles.RemoveAll(e => currentRoles.Contains(e));
                    await UserManager.AddClaimsAsync(user, 
                        model.UserRoles.Select(e => new Claim(ClaimTypes.Role, e)));
                }

                SweetAlertBuilder alert = new SweetAlertBuilder(
                    "Usuário atualizado.",
                    $"O usuário {user.UserName} foi alterado com sucesso",
                    MessageType.Success);
                ScriptManager.SetStartupScript(TempData, alert.BuildScript());

                return RedirectToAction(actionName: nameof(Index));
            }
        }
    }
}