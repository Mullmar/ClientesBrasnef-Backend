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
    public class GetClientesQuery : IRequest<IEnumerable<Cliente>>
    {
        public class GetClientesQueryHandler : IRequestHandler<GetClientesQuery, IEnumerable<Cliente>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetClientesQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public async Task<IEnumerable<Cliente>> Handle(GetClientesQuery request, CancellationToken cancellationToken)
            {
                var clientes = await _unitOfWork.ClienteRepository.GetClientes();
                return clientes;
            }
        }
    }
}
