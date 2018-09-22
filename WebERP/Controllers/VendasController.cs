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
            return View(_repository.ListAll());
        }
    }
}