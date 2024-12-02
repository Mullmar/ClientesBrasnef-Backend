using CadClientes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadClientes.Infrastructure.Context
{
    public class AppDbContextSQL : DbContext
    {
        public AppDbContextSQL(DbContextOptions<AppDbContextSQL> options) : base(options)
        {
                
        }

        public DbSet<Cliente> Cliente { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
                .HasKey(c => c.Id);
            
            modelBuilder.Entity<Cliente>()
                .Property(c => c.IdTipoEmpresa)
                .HasDefaultValue(1);

            modelBuilder.Entity<Cliente>().Property(c => c.NomeEmpresa).HasMaxLength(255);

            base.OnModelCreating(modelBuilder);
        }


    }
}
