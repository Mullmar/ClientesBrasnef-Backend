using CadClientes.Application.Clientes.Commands;
using CadClientes.Application.Clientes.Queries;
using CadClientes.Domain.Abstractions;
using CadClientes.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CadClientes.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public ClienteController(IMediator mediator, IUnitOfWork unitOfWork)
        {
            this._mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult> GetClientes()
        {
            var query = new GetClientesQuery();
            var clientes = await _mediator.Send(query);
            return Ok(clientes);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult> GetCliente(int id)
        {
            var query = new GetClienteByIdQuery { Id = id };
            var cliente = await _mediator.Send(query);
            return cliente != null ? Ok(cliente) : NotFound("Cliente não encontrado!");
        }

        [HttpPost("Inserir")]
        public async Task<ActionResult> AddCliente(CreateClienteCommand command)
        {

            var createdCliente = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCliente), new { id = createdCliente.Id }, createdCliente);

        }

        [HttpPut("Editar")]

        public async Task<ActionResult> UpdateCliente(UpdateClienteCommand command)
        {
            var updatedCliente = await _mediator.Send(command);

            return updatedCliente != null ? Ok(updatedCliente) : NotFound("Cliente não encontrado!");
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteCliente(int id)
        {
            var command = new DeleteClienteCommand { Id = id };

            var deletedCliente = await _mediator.Send(command);

            return deletedCliente != null ? Ok(deletedCliente) : NotFound("Cliente não encontrado!");
        }

    }
}
