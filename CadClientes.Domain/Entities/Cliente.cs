using CadClientes.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CadClientes.Domain.Entities
{
    public sealed class Cliente : Entity
    {
        public Cliente() { }
        public Cliente(Guid id) { }

        public Cliente(string NomeEmpresa, int tipoEmpresaId)
        {
            ValidateDomain(NomeEmpresa, tipoEmpresaId);
            this.NomeEmpresa = NomeEmpresa;
            this.IdTipoEmpresa = tipoEmpresaId;
        }

        [JsonConstructor] // Este construtor será utilizado na desserialização
        public Cliente(int id, string NomeEmpresa, int IdtipoEmpresa)
        {
            DomainValidation.When(id < 1 && id !=0, "Id do cliente inválido!");
            Id = id;
            ValidateDomain(NomeEmpresa, IdtipoEmpresa);
            this.IdTipoEmpresa = IdtipoEmpresa;
            this.NomeEmpresa = NomeEmpresa;
        }

        public string NomeEmpresa { get; set; }

        public int IdTipoEmpresa { get; set; }


        public void ValidateDomain(string NomeEmpresa, int tipoEmpresaId)
        {
            DomainValidation.When(string.IsNullOrEmpty(NomeEmpresa), "Nome da Empresa não pode ser branco.");

            DomainValidation.When(NomeEmpresa.Length > 255, "Nome da Empresa não pode ter mais do que 255 caracteres.");

            DomainValidation.When(tipoEmpresaId > 3 || tipoEmpresaId < 1, "Id do porte da Empresa inválido.");
        }
    }
}
