using CadClientes.Domain.Abstractions;
using CadClientes.Infrastructure.Context;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadClientes.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IClienteRepository _clienteRepository;
        private readonly string _connectionString;
        private readonly AppDbContextMongo _appContextMongo;
        

        public UnitOfWork (IConfiguration configuration, AppDbContextMongo appDbContextMongo){
            _appContextMongo = appDbContextMongo;
            _connectionString = configuration?.GetConnectionString("SQLServer");
        }
        public IClienteRepository ClienteRepository {  
            get {
                return _clienteRepository = _clienteRepository ??
                 new ClienteRepository(_connectionString, _appContextMongo); 
            } 
        }
        public async Task Rollback()
        {
            _appContextMongo.AbortSession();
        }
    }
}
