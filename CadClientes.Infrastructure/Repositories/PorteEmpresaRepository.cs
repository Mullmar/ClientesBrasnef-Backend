using CadClientes.Domain.Abstractions;
using CadClientes.Domain.Entities;
using CadClientes.Infrastructure.Context;
using MongoDB.Driver;

public class PorteEmpresaRepository : IPorteEmpresaRepository
{
    private readonly AppDbContextMongo dbMongo;

    public PorteEmpresaRepository(AppDbContextMongo dbMongo)
    {
        this.dbMongo = dbMongo;
    }

    public async Task<IEnumerable<TipoEmpresa>> GetAll()
    {
        var tipoEmpresas = await dbMongo.TipoEmpresa.Find(_ => true).ToListAsync();
        return tipoEmpresas;
    }
}
