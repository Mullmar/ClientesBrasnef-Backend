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
    public class DeleteClienteCommand : IRequest<Cliente>
    {
        public int Id { get; set; }

        public class DeleteClienteCommandHandler : IRequestHandler<DeleteClienteCommand, Cliente>
        {
            private readonly IUnitOfWork _unitOfWork;

            public DeleteClienteCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public async Task<Cliente> Handle(DeleteClienteCommand request, CancellationToken cancellationToken)
            {
                var deletedCliente = await _unitOfWork.ClienteRepository.DeleteCliente(request.Id);

                if (deletedCliente is null) {
                    throw new InvalidOperationException("Cliente não encontrado!");
                }

                return deletedCliente;
            }
        }
    }
}
