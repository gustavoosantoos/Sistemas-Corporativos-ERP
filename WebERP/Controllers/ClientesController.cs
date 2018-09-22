using Microsoft.AspNetCore.Mvc;
using WebERP.Data.Repositories;
using WebERP.Models;
using WebERP.Models.Clientes;
using WebERP.Utils;

namespace WebERP.Controllers
{
    public class ClientesController : BaseController
    {
        private readonly ClientesRepository _repository;

        public ClientesController(
            ClientesRepository repository,
            CurrentUtils current) : base(current)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            RegisterActivePage();

            return View(_repository.ListAll());
        }

        public IActionResult Form(int? id)
        {
            RegisterActivePage();

            if (id.HasValue)
            {
                var cliente = _repository.FindById(id.Value);
                return View(cliente);
            }
        
            return View();
        }

        [HttpPost]
        public IActionResult Form(Cliente cliente)
        {
            RegisterActivePage();

            if (ModelState.IsValid)
            {
                _repository.Save(cliente);
                RegisterSweetAlertMessage("Cliente cadastrado", "Cliente cadastrado com sucesso!", MessageType.Success);
                return RedirectToAction(nameof(Index));
            }

            return View(cliente);
        }

        private void RegisterActivePage()
        {
            ViewBag.ClienteActive = "active";
        }
    }
}