using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TestGradjFirma.Data
{
    public class Context : IdentityDbContext
    {
        public Context() : base()
        {

        }
        public Context(DbContextOptions optionsBuilder) : base(optionsBuilder)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("Baza");
                optionsBuilder.UseSqlServer(connectionString);
            }

        }
        public DbSet<User> Users { get; set; }
    }
}
