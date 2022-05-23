using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        //tiene acceso a todos los métodos del Repositorio base
        public EmployeeRepository(RepositoryContext repositoryContext) :
            base(repositoryContext)
        {
        }
    }
}
