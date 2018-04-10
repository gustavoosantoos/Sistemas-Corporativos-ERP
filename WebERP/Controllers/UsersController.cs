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
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using WebERP.Data;
using WebERP.Data.Repositories;
using WebERP.Extensions;
using WebERP.Models;
using WebERP.Utils;
using WebERP.Utils.Identity;
using WebERP.ViewModels.AccountViewModels;
using WebERP.ViewModels.ManageViewModels;

namespace WebERP.Controllers
{
    [Authorize(Roles = ErpRoleNames.SuperAdmin)]
    public class UsersController : BaseController
    {
        public UsersController(CurrentUtils current) : base(current)
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            RegisterActivePage();

            var users = UserRepository.Users().Select(e => new UserViewModel()
            {
                Id = e.Id,
                Nome = e.Nome,
                Sobrenome = e.Sobrenome,
                Username = e.UserName,
                PhoneNumber = e.PhoneNumber,
                HigherRole = UserRepository.GetHigherRole(e)
            }).Where(e => UserRepository.GetHigherRole(CurrentUser).Nivel < e.HigherRole.Nivel).ToList();

            return View(users);
        }

        [HttpGet]
        public IActionResult UserForm(string id)
        {
            RegisterActivePage();

            RegisterViewModel viewModel = new RegisterViewModel();

            if (!string.IsNullOrWhiteSpace(id))
            {
                var userToEdit = UserRepository.FindById(id);
                if (userToEdit == null)
                    return BadRequest();

                viewModel.Nome = userToEdit.Nome;
                viewModel.Sobrenome = userToEdit.Sobrenome;
                viewModel.UserName = userToEdit.UserName;
                viewModel.Email = userToEdit.Email;
                viewModel.UserRoles = UserRepository.GetUserRoles(userToEdit).ToList();
            }

            ViewBag.AvailableRoles = new MultiSelectList(
                 UserRepository.GetAuthorizedRolesToCreate(CurrentUser),
                 nameof(ErpRole.RoleName), nameof(ErpRole.FormmatedName), viewModel.UserRoles);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult UserForm(RegisterViewModel model)
        {
            RegisterActivePage();

            if (!model.IsNewUser)
            {
                ModelState.Remove("Password");
            }

            if (ModelState.IsValid == false)
            {
                ViewBag.AvailableRoles = new MultiSelectList(
                    UserRepository.GetAuthorizedRolesToCreate(CurrentUser),
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

                UserRepository.Create(user, model.Password);
                UserRepository.AddClaims(user, model.UserRoles.Select(e => new Claim(ClaimTypes.Role, e)));

                RegisterSweetAlertMessage("Usuário criado",
                    $"O usuário {user.UserName} foi criado com sucesso",
                    MessageType.Success);

                return RedirectToAction(actionName: nameof(Index));
            }
            else
            {
                var user = UserRepository.FindById(model.Id);
                user.Nome = model.Nome;
                user.Sobrenome = model.Sobrenome;
                user.UserName = model.UserName;
                user.Email = model.Email;

                var currentRoles = UserRepository.GetUserRoles(user).ToList();

                if (currentRoles.Count > model.UserRoles.Count)
                {
                    currentRoles.RemoveAll(e => model.UserRoles.Contains(e));
                    UserRepository.RemoveClaims(user,
                        currentRoles.Select(e => new Claim(ClaimTypes.Role, e)));
                }
                else
                {
                    model.UserRoles.RemoveAll(e => currentRoles.Contains(e));
                    UserRepository.AddClaims(user,
                        model.UserRoles.Select(e => new Claim(ClaimTypes.Role, e)));
                }

                RegisterSweetAlertMessage("Usuário atualizado.", $"O usuário {user.UserName} foi alterado com sucesso", MessageType.Success);

                return RedirectToAction(actionName: nameof(Index));
            }
        }

        private void RegisterActivePage()
        {
            ViewData["GerenciamentoDeUsuarios"] = "active";
        }
    }
}