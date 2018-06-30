using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebERP.Data.Repositories;
using WebERP.Models;

namespace WebERP.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Products")]
    public class ProductsController : BaseController
    {
        private readonly ProductRepository _productRepository;

        public ProductsController(
            CurrentUtils current,
            ProductRepository productRepository) : base(current)
        {
            this._productRepository = productRepository;
        }

        [Authorize(Roles = "SuperAdmin, SupervisorDeEstoque")]
        [HttpPost]
        [Route("AtualizarQuantidade/{id}/{novaQuantidade}")]
        public IActionResult AtualizarQuantidade(int id, double novaQuantidade)
        {
            var produto = this._productRepository.FindById(id);
            if (produto == null)
                return NotFound();

            produto.Quantidade = novaQuantidade;
            this._productRepository.Save(produto);
            return Ok();
        }
    }
}