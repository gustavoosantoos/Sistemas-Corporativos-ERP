using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebERP.Data.Repositories;
using WebERP.Models;
using WebERP.Utils.Identity;

namespace WebERP.Controllers
{
    [Authorize(Roles = ErpRoleGroups.Compras)]
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
       
        public IActionResult Orcamentacao()
        {
            var solicitacoes = _repository.ListaSolicitacoesEmOrcamentacao();
            return View(solicitacoes);
        }
    }
}