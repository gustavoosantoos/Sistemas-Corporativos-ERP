using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebERP.Data.Repositories;
using WebERP.Models;
using WebERP.Models.Compras;
using WebERP.Models.Estoque;
using WebERP.Utils.Identity;
using WebERP.ViewModels.ComprasViewModels;

namespace WebERP.Controllers
{
    [Authorize(Roles = ErpRoleGroups.Compras)]
    public class SolicitacaoController : BaseController
    {
        private readonly SolicitacaoRepository _repository;
        private readonly FornecedoresRepository _fornecedoresRepository;
        private readonly OrcamentosRepository _orcamentosRepository;

        public SolicitacaoController(CurrentUtils current,
            SolicitacaoRepository repository,
            FornecedoresRepository fornecedoresRepository,
            OrcamentosRepository orcamentosRepository) : base(current)
        {
            _repository = repository;
            _fornecedoresRepository = fornecedoresRepository;
            _orcamentosRepository = orcamentosRepository;
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

            Solicitacao solicitacao = _repository.FindById(id.Value, includeProduto: true, includeSolicitante: true, includeOrcamentos: true);

            OrcamentoViewModel vm = new OrcamentoViewModel();
            vm.Produto = solicitacao.Produto;
            vm.Solicitacao = solicitacao;
            vm.Solicitante = solicitacao.Solicitante.Nome;
            vm.Orcamentos = _orcamentosRepository.GetOrcamentosDaSolicitacao(id.Value);
            vm.FornecedoresDisponiveis = _fornecedoresRepository.ListAll();

            ViewBag.FornecedoresDisponiveis = new MultiSelectList(
                vm.FornecedoresDisponiveis,
                nameof(Fornecedor.Id), 
                nameof(Fornecedor.RazaoSocial));

            return PartialView("_Orcamentos", vm);
        }
    }
}