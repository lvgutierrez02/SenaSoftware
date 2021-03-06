using Entities.Configuracion;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
        }

        //Esto lo utilizamos para llenar las tablas (seeder) al ejecutar la migración.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        }

        //DbSet: Representa una tabla en la base de datos
        public DbSet<Company> Companies { get; set; } 
        public DbSet<Employee> Employees { get; set; }
    }
}
