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
        // DbSet: representa una tabla en la base de datos, diciendo el modelo y como se va a llamar en la DB
        public DbSet<Company> Companies { get; set; } 
        public DbSet<Employee> Employees { get; set; }
    }
}
