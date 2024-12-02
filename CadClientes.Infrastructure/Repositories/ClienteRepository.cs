using CadClientes.Domain.Abstractions;
using CadClientes.Domain.Entities;
using CadClientes.Infrastructure.Context;
using Dapper;
using Microsoft.Data.SqlClient;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CadClientes.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly string _connectionString;
        private readonly IDbConnection _dbConnection;
        protected readonly AppDbContextMongo _dbMongo;

        public ClienteRepository(string connectionString, AppDbContextMongo dbMongo)
        {
            _connectionString = connectionString;
            _dbMongo = dbMongo;
        }

        // Método para obter todos os clientes
        public async Task<IEnumerable<Cliente>> GetClientes()
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                return await _dbMongo.Cliente.Find(_ => true).ToListAsync();
            }
        }

        // Método para adicionar um cliente
        public async Task<int> AddCliente(Cliente cliente)
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                await dbConnection.OpenAsync();

                using (var transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        var query = "INSERT INTO Cliente (NomeEmpresa, IdTipoEmpresa) VALUES (@NomeEmpresa, @IdTipoEmpresa); SELECT CAST(SCOPE_IDENTITY() as int)";
                        var id = await dbConnection.QuerySingleAsync<int>(query, new { cliente.NomeEmpresa, cliente.IdTipoEmpresa }, transaction);

                        cliente.Id = id;

                        //_dbMongo.StartSession();
                        await _dbMongo.Cliente.InsertOneAsync(cliente);

                        transaction.Commit();

                        return id;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        //_dbMongo.AbortSession();
                        throw new Exception("Erro durante o processo de transação.", ex);
                    }
                }
            }
        }

        public async Task<Cliente> GetById(int id) { 
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                {
                    var filter = Builders<Cliente>.Filter.Eq(c => c.Id, id);
                    return await _dbMongo.Cliente.Find(filter).FirstOrDefaultAsync();
                }
            }
        }

        public async Task<Cliente> UpdateCliente(Cliente cliente)
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                await dbConnection.OpenAsync();
                using (var transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        if (cliente is null)
                            throw new ArgumentNullException(nameof(cliente));

                        var existingClienteSQL = await this.GetById(cliente.Id);
                        if (existingClienteSQL == null)
                        {
                            throw new InvalidOperationException("Cliente não encontrado no SQL Server.");
                        }

                        var updateQuery = @"
                                        UPDATE Cliente
                                        SET NomeEmpresa = @NomeEmpresa, IdTipoEmpresa = @IdTipoEmpresa
                                        WHERE Id = @Id";

                        var affectedRows = await dbConnection.ExecuteAsync(updateQuery, new
                        {
                            NomeEmpresa = cliente.NomeEmpresa,
                            IdTipoEmpresa = cliente.IdTipoEmpresa,
                            Id = cliente.Id
                        }, transaction);

                        if (affectedRows == 0)
                        {
                            transaction.Rollback();
                            throw new InvalidOperationException("Não foi possível atualizar o cliente no SQL Server.");
                        }

                        var filter = Builders<Cliente>.Filter.Eq(c => c.Id, cliente.Id);
                        var update = Builders<Cliente>.Update
                            .Set(c => c.NomeEmpresa, cliente.NomeEmpresa)
                            .Set(c => c.IdTipoEmpresa, cliente.IdTipoEmpresa);

                        //_dbMongo.StartSession();
                        await _dbMongo.Cliente.UpdateOneAsync(filter, update);

                        transaction.Commit();

                        return cliente;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        //_dbMongo.AbortSession();
                        throw new Exception("Erro durante o processo de transação.", ex);
                    }
                }
            }
        }

        public async Task<Cliente> DeleteCliente(int clienteId)
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                await dbConnection.OpenAsync();
                using (var transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        var existingClienteSQL = await this.GetById(clienteId);
                        if (existingClienteSQL == null)
                        {
                            throw new InvalidOperationException("Cliente não encontrado no SQL Server.");
                        }

                        var deleteQuery = "DELETE FROM Cliente WHERE Id = @Id";
                        await dbConnection.ExecuteAsync(deleteQuery, new { Id = clienteId }, transaction);

                        var filter = Builders<Cliente>.Filter.Eq(c => c.Id, clienteId);
                        var clienteMongo = await _dbMongo.Cliente.FindOneAndDeleteAsync(filter);

                        transaction.Commit();

                        return clienteMongo;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        //_dbMongo.AbortSession();
                        throw new Exception("Erro durante o processo de transação.", ex);
                    }
                }
            }
        }
    }
}
