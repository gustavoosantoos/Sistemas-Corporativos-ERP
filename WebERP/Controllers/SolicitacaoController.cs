using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebERP.Data.Repositories;
using WebERP.Models;

namespace WebERP.Controllers
{
    public class SolicitacaoController : BaseController
    {
        private readonly SolicitacaoRepository _repository;

        public SolicitacaoController(CurrentUtils current, SolicitacaoRepository repository) : base(current)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var solicitacoes = _repository.ListAll();
            return View(solicitacoes);
        }
    }
}