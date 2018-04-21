using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebERP.Data.Repositories;
using WebERP.Models;
using WebERP.Models.Compras;
using WebERP.Utils.Identity;
using WebERP.ViewModels.ComprasViewModels;

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

        public IActionResult Orcamentos(int? id)
        {
            if (id.HasValue == false)
                return BadRequest();

            Solicitacao solicitacao = _repository.FindById(id.Value, includeProduto: true, includeSolicitante: true);

            OrcamentoViewModel vm = new OrcamentoViewModel();
            vm.Produto = solicitacao.Produto;
            vm.Solicitacao = solicitacao;
            vm.Solicitante = solicitacao.Solicitante.Nome;
            vm.Orcamentos = solicitacao.Orcamentos.ToList();

            return PartialView("_Orcamentos", vm);
        }
    }
}