using BancoSENAIAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace BancoSENAIAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Agencia> Agencia { get; set; }

        public DbSet<Carteira> Carteira { get; set; }

        public DbSet<Usuario> Usuario{ get; set; }

        public DbSet<Cliente> Cliente { get; set; }

        public DbSet<DocumentoMetadados> Documento { get; set; }
    }
}