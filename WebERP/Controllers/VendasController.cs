using Microsoft.AspNetCore.Mvc;
using WebERP.Data.Repositories;
using WebERP.Models;

namespace WebERP.Controllers
{
    public class VendasController : BaseController
    {
        private readonly VendasRepository _repository;

        public VendasController(
            VendasRepository repository,
            CurrentUtils current) : base(current)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            RegisterActivePage();
            return View(_repository.ListAll());
        }

        private void RegisterActivePage()
        {
            ViewBag.ClienteActive = "active";
        }
    }
}