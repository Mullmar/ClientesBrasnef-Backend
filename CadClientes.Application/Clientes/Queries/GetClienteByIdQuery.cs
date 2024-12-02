using CadClientes.Domain.Abstractions;
using CadClientes.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadClientes.Application.Clientes.Queries
{
    public class GetClienteByIdQuery :IRequest<Cliente>
    {
        public int Id { get; set; }
        public class GetClienteByIdQueryHandler : IRequestHandler<GetClienteByIdQuery, Cliente>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetClienteByIdQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public async Task<Cliente> Handle(GetClienteByIdQuery request, CancellationToken cancellationToken)
            {
                var cliente = await _unitOfWork.ClienteRepository.GetById(request.Id);
                return cliente;
            }
        }
    }
}
