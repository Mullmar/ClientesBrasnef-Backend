using CadClientes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadClientes.Domain.Abstractions
{
    public interface IPorteEmpresaRepository
    {
        Task<IEnumerable<TipoEmpresa>> GetAll();

    }
}
