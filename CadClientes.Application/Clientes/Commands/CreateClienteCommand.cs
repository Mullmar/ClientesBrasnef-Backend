using CadClientes.Domain.Abstractions;
using CadClientes.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadClientes.Application.Clientes.Commands
{
    public class CreateClienteCommand : IRequest<Cliente>
    {
        public string NomeEmpresa { get; set; }

        public int IdTipoEmpresa { get; set; }

        public class CreateClienteCommandHandler : IRequestHandler<CreateClienteCommand, Cliente>
        {
            private readonly IUnitOfWork _unitOfWork;

            public CreateClienteCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public async Task<Cliente> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
            {
                var newCliente = new Cliente(request.NomeEmpresa, request.IdTipoEmpresa);
                await _unitOfWork.ClienteRepository.AddCliente(newCliente);

                return newCliente;
            }
        }
    }
}
