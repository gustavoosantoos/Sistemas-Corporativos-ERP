using Microsoft.AspNetCore.Mvc;
using WebERP.Data.Repositories;
using WebERP.Models;
using WebERP.Models.Estoque;
using WebERP.Utils;

namespace WebERP.Controllers
{
    public class FornecedoresController : BaseController
    {
        private readonly FornecedoresRepository _repository;

        public FornecedoresController(FornecedoresRepository repository, CurrentUtils current) : base(current)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var fornecedores = _repository.ListAll();
            return View(fornecedores);
        }

        public IActionResult Form(int? id)
        {
            if (id.HasValue)
            {
                Fornecedor fornecedor = _repository.FindById(id.Value);
                return View(fornecedor);
            }

            return View();
        }

        [HttpPost]
        public IActionResult Form(Fornecedor fornecedor)
        {
            if (ModelState.IsValid == false)
                return View(fornecedor);

            _repository.Save(fornecedor);
            RegisterSweetAlertMessage("Fornecedor cadastrado", "Fornecedor cadastrado com sucesso!", MessageType.Success);
            return RedirectToAction(nameof(Index));
        }
    }
}