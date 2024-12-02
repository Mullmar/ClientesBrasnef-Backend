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
    public class UpdateClienteCommand : IRequest<Cliente>
    {
        public int Id { get; set; }
        public string NomeEmpresa { get; set; }

        public int IdTipoEmpresa { get; set; }

        public class UpdateClienteCommandHandler : IRequestHandler<UpdateClienteCommand, Cliente>
        {
            private readonly IUnitOfWork _unitOfWork;

            public UpdateClienteCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public async Task<Cliente> Handle(UpdateClienteCommand request, CancellationToken cancellationToken)
            {
                var existingCliente = await _unitOfWork.ClienteRepository.GetById(request.Id);

                if (existingCliente == null)
                {
                    throw new InvalidOperationException("Cliente não encontrado!");
                }

                existingCliente.Id = request.Id;
                existingCliente.NomeEmpresa = request.NomeEmpresa;
                existingCliente.IdTipoEmpresa = request.IdTipoEmpresa;

                _unitOfWork.ClienteRepository?.UpdateCliente(existingCliente);

                return existingCliente;
            }
        }
    }
}
