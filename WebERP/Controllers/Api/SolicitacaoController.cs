using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebERP.Data.Repositories;
using WebERP.Models;
using WebERP.Models.Compras;
using WebERP.Models.Dto;
using WebERP.Models.Estoque;

namespace WebERP.Controllers.Api
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Solicitacao")]
    public class SolicitacaoController : BaseController
    {
        private readonly ProductRepository _productRepository;
        private readonly SolicitacaoRepository _solicitacaoRepository;

        public SolicitacaoController(CurrentUtils current, ProductRepository productRepository, SolicitacaoRepository solicitacaoRepository) : base(current)
        {
            _productRepository = productRepository;
            _solicitacaoRepository = solicitacaoRepository;
        }

        [Authorize(Roles = "SuperAdmin, SupervisorDeEstoque, Estoque")]
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
        [Authorize(Roles = "SuperAdmin, SupervisorDeCompras, Compras")]
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
        [Authorize(Roles = "SuperAdmin, SupervisorDeCompras, Compras")]
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
    }
}