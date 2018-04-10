using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebERP.Data.Repositories;
using WebERP.Models;
using WebERP.Models.Estoque;
using WebERP.Utils;
using WebERP.Utils.Identity;
using WebERP.ViewModels.EstoqueViewModels;

namespace WebERP.Controllers
{
    [Authorize(Roles = "SuperAdmin, SupervisorDeEstoque, Estoque")]
    public class ProductsController : BaseController
    {
        private readonly ProductRepository _repository;
        private readonly SolicitacaoRepository _solicitacaoRepository;

        public ProductsController(CurrentUtils current, ProductRepository repository, SolicitacaoRepository solicitacaoRepository) : base(current)
        {
            _repository = repository;
            _solicitacaoRepository = solicitacaoRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            RegisterActivePage();

            List<ProdutoViewModel> produtos = _repository
                .ListAll().Select(e => new ProdutoViewModel()
                {
                    Produto = e,
                    Solicitacao = _solicitacaoRepository.PegaSolicitacaoEmAbertoDoProduto(e.Id)
                }).ToList();
            
            return View(produtos);
        }

        [HttpGet]
        public IActionResult Form(int? id)
        {
            RegisterActivePage();
            
            if (id.HasValue)
            {
                var produto = _repository.FindById(id.Value);
                return View(produto);
            }

            return View();
        }

        [HttpPost]
        public IActionResult Form(Produto produto)
        {
            if (!ModelState.IsValid)
                return View();

            _repository.Save(produto);

            RegisterSweetAlertMessage("Produto salvo!", "O produto foi salvo com sucesso!", MessageType.Success);
            return RedirectToAction(actionName: nameof(Index));
        }

        private void RegisterActivePage()
        {
            ViewBag.ProductActive = "active";
        }
    }
}