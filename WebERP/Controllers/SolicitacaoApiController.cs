using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebERP.Data.Repositories;
using WebERP.Models;
using WebERP.Models.Compras;
using WebERP.Models.Dto;
using WebERP.Models.Estoque;

namespace WebERP.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Solicitacao")]
    public class SolicitacaoApiController : BaseController
    {
        private readonly ProductRepository _productRepository;
        private readonly SolicitacaoRepository _solicitacaoRepository;

        public SolicitacaoApiController(CurrentUtils current, ProductRepository productRepository, SolicitacaoRepository solicitacaoRepository) : base(current)
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
            solicitacao.Status = StatusSolicitacao.Pendente;

            _solicitacaoRepository.Save(solicitacao);

            return Ok();
        }
    }
}