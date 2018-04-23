using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebERP.Data.Repositories;
using WebERP.Models;
using WebERP.Models.Compras;
using WebERP.Models.Dto;
using WebERP.Models.Estoque;
using WebERP.Utils.Identity;

namespace WebERP.Controllers.Api
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Solicitacao")]
    public class SolicitacaoController : BaseController
    {
        private readonly ProductRepository _productRepository;
        private readonly SolicitacaoRepository _solicitacaoRepository;
        private readonly FornecedoresRepository _fornecedoresRepository;
        private readonly OrcamentosRepository _orcamentosRepository;

        public SolicitacaoController(
            CurrentUtils current, 
            ProductRepository productRepository,
            SolicitacaoRepository solicitacaoRepository, 
            FornecedoresRepository fornecedoresRepository,
            OrcamentosRepository orcamentosRepository) : base(current)
        {
            _productRepository = productRepository;
            _solicitacaoRepository = solicitacaoRepository;
            _fornecedoresRepository = fornecedoresRepository;
            _orcamentosRepository = orcamentosRepository;
        }

        [Authorize(Roles = ErpRoleGroups.Estoque)]
        [HttpPost]
        [Route("AdicionarSolicitacao")]
        public IActionResult AdicionarSolicitacao(NovaSolicitacaoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Produto produto = _productRepository.FindById(dto.ProductId);

            if (produto == null || !produto.PossuiEstoqueBaixoDoMinimo())
                return BadRequest("O produto informado não foi encontrado ou não se encaixa nos requisitos para solicitações.");

            if (_solicitacaoRepository.ProdutoPossuiSolicitacaoAberta(produto.Id))
                return BadRequest("Produto já possui solicitação em aberto.");

            Solicitacao solicitacao = new Solicitacao();
            solicitacao.Data = DateTime.Today;
            solicitacao.ProdutoId = produto.Id;
            solicitacao.SolicitanteId = CurrentUser.Id;
            solicitacao.QuantidadeSolicitada = dto.Quantidade;
            _solicitacaoRepository.Save(solicitacao);

            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = ErpRoleGroups.Compras)]
        [Route("AprovarSolicitacao")]
        public IActionResult AprovarSolicitacao(int solicitacaoId)
        {
            Solicitacao solicitacao = _solicitacaoRepository.FindById(solicitacaoId);

            if (solicitacao == null)
                return NotFound("Solicitação não encontrada.");
            if (solicitacao.Status != StatusSolicitacao.Pendente)
                return BadRequest("O status da solicitação é invalido para a operação solicitada.");

            solicitacao.AprovarSolicitacaoParaOrcamentacao();
            _solicitacaoRepository.Save(solicitacao);

            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = ErpRoleGroups.Compras)]
        [Route("NegarSolicitacao")]
        public IActionResult NegarSolicitacao(int solicitacaoId)
        {
            Solicitacao solicitacao = _solicitacaoRepository.FindById(solicitacaoId);

            if (solicitacao == null)
                return NotFound("Solicitação não encontrada.");
            if (solicitacao.Status != StatusSolicitacao.Pendente)
                return BadRequest("O status da solicitação é invalido para a operação solicitada.");

            solicitacao.NegarSolicitacao();
            _solicitacaoRepository.Save(solicitacao);

            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = ErpRoleGroups.Compras)]
        [Route("AdicionarOrcamento")]
        public IActionResult AdicionarOrcamento(NovoOrcamentoDto dto)
        {
            Solicitacao solicitacao = _solicitacaoRepository.FindById(dto.SolicitacaoId);
            Fornecedor fornecedor = _fornecedoresRepository.FindById(dto.FornecedorId);

            if (fornecedor == null)
                return NotFound("Fornecedor não encontrado.");
            if (solicitacao == null)
                return NotFound("Solicitação não encontrada");

            Orcamento orcamento = new Orcamento();
            orcamento.SolicitacaoId = dto.SolicitacaoId;
            orcamento.FornecedorId = dto.FornecedorId;
            orcamento.PrecoUnitario = dto.PrecoUnitario;

            _orcamentosRepository.Save(orcamento);
            return Ok(new {orcamentoId = orcamento.Id});
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin, SupervisorDeCompras")]
        [Route("GerarPedido/{id}")]
        public IActionResult GerarPedido(int? id)
        {
            if (id == null)
                return BadRequest("Informe um id de orçamento válido.");

            Orcamento orcamento = _orcamentosRepository.FindById(id.Value);

            if (orcamento == null)
                return NotFound("Orçamento não encontrado.");

            Produto produto = _productRepository.FindById(orcamento.Solicitacao.ProdutoId);
            produto.Quantidade += orcamento.Solicitacao.QuantidadeSolicitada;
            _productRepository.Save(produto);

            Solicitacao solicitacao = orcamento.Solicitacao;
            solicitacao.AprovarSolicitacaoFinalizada();
            _solicitacaoRepository.Save(solicitacao);

            return Ok();
        }
    }
}