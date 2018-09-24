using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebERP.Data.Repositories;
using WebERP.Models;
using WebERP.Models.Clientes;
using WebERP.Models.Estoque;
using WebERP.Models.Vendas;

namespace WebERP.Controllers
{
    public class VendasController : BaseController
    {
        private readonly VendasRepository _repository;
        private readonly ClientesRepository _clientesRepository;
        private readonly ProductRepository _produtoRepository;

        public VendasController(
            VendasRepository repository,
            ClientesRepository clientesRepository,
            ProductRepository produtoRepository,
            CurrentUtils current) : base(current)
        {
            _repository = repository;
            _clientesRepository = clientesRepository;
            _produtoRepository = produtoRepository;
        }

        public IActionResult Index()
        {
            RegisterActivePage();
            return View(_repository.ListAll());
        }
        
        public IActionResult Form()
        {
            ViewBag.Clientes = new MultiSelectList(
                _clientesRepository.ListAll(),
                nameof(Cliente.Id), 
                nameof(Cliente.NomeCompleto));

            ViewBag.Produtos = new MultiSelectList(
                _produtoRepository.ListAll(),
                nameof(Produto.Id),
                nameof(Produto.Nome));
            
            return View();
        }

        [HttpPost]
        public IActionResult Form(Venda venda)
        {
            if (ModelState.IsValid)
            {
                _repository.Save(venda);
                return RedirectToAction(nameof(Index));
            }

            return View(venda);
        }

        private void RegisterActivePage()
        {
            ViewBag.ClienteActive = "active";
        }
    }
}