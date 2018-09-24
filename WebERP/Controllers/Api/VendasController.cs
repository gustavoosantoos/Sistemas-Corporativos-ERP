using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebERP.Data.Repositories;
using WebERP.Models;
using WebERP.Models.Vendas;
using WebERP.Services;

namespace WebERP.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Vendas")]
    public class VendasController : BaseController
    {
        private readonly IEmailSender _emailSender;
        private readonly VendasRepository _vendasRepository;
        private readonly ClientesRepository _clientesRepository;
        private readonly ProductRepository _productRepository;

        public VendasController(
            IEmailSender emailSender,
            VendasRepository vendasRepository,
            ClientesRepository clientesRepository,
            ProductRepository productRepository,
            CurrentUtils current) : base(current)
        {
            _emailSender = emailSender;
            _vendasRepository = vendasRepository;
            _clientesRepository = clientesRepository;
            _productRepository = productRepository;
        }

        [HttpPost]
        public IActionResult Post(Venda venda)
        {
            venda.DataHoraVenda = DateTime.Now;

            venda.Produtos.ForEach(p =>
            {
                var product = _productRepository.FindById(p.ProdutoId);
                product.Quantidade -= p.Quantidade;
                _productRepository.Save(product);
            });

            _vendasRepository.Save(venda);
            return Ok();
        }

        [HttpPost]
        [Route("notificar")]
        public IActionResult Notificar(Venda venda)
        {
            venda.Cliente = _clientesRepository.FindById(venda.ClienteId);

            var mailTitle = "Novo orçamento pendente de avaliação.";
            var mailMessage = "Você possui um novo orçamento para avaliar!<br /><br />";

            venda.Produtos.ForEach(produto =>
            {
                produto.Produto = _productRepository.FindById(produto.ProdutoId);
                mailMessage += $"<b>{produto.Produto.Nome}</b>, quantidade solicitada: {produto.Quantidade} {produto.Produto.UnidadeDeMedida}, valor: R$ {Math.Round(produto.Quantidade * produto.PrecoUnitario, 2)} <br /><br />";
            });

            mailMessage += $"<br/><br/><b> Valor total do orçamento: R$ {Math.Round(venda.Produtos.Sum(p => p.Quantidade * p.PrecoUnitario), 2)}";

            _emailSender.SendEmailAsync(venda.Cliente.Email, mailTitle, mailMessage);

            return Ok();
        }
    }
}