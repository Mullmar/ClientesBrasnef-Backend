using CadClientes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadClientes.Domain.Abstractions
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> GetClientes();

        Task<Cliente> GetById(int id);

        Task<int> AddCliente(Cliente cliente);

        Task<Cliente> UpdateCliente(Cliente cliente);

        Task<Cliente> DeleteCliente(int clienteId);
    }
}
