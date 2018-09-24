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
    [Route("api/Clientes")]
    public class ClientesController : BaseController
    {
        private readonly ClientesRepository _clientesRepository;

        public ClientesController(
            ClientesRepository clientesRepository,
            CurrentUtils current) : base(current)
        {
            _clientesRepository = clientesRepository;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var cliente = _clientesRepository.FindById(id);
            if (cliente == null)
                return NotFound();
            
            return Ok(cliente);
        }
    }
}