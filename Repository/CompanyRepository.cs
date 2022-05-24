using Contracts;
using Entities;
using Entities.DTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        //tiene acceso a todos los métodos del Repositorio base
        public CompanyRepository(RepositoryContext repositoryContext) : 
            base(repositoryContext)
        {
        }

        public IEnumerable<Company> GetAllCompanies(bool trackChanges) => FindAll(trackChanges).OrderBy(c => c.Name).ToList();

        public Company GetCompany(Guid companyId, bool trackChanges) =>FindByCondition(c => c.Id.Equals(companyId), trackChanges).SingleOrDefault();


    }
}
