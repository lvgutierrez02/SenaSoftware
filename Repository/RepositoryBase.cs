using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    //Repositorio genérico que nos proporcione los métodos CRUD
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class 
    {
        protected RepositoryContext RepositoryContext;
        public RepositoryBase(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }
        //trackChanges: mejora el rendimiento de nuestras consultas de solo lectura
        //Cuando se establece en false, adjuntamos el método AsNoTracking a nuestra consulta para informar a EF Core
        //que no necesita realizar un seguimiento de los cambios para las entidades requeridas.Esto mejora enormemente
        //la velocidad de una consulta.
        public IQueryable<T> FindAll(bool trackChanges) => !trackChanges ? RepositoryContext.Set<T>().AsNoTracking() : RepositoryContext.Set<T>(); 
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) => !trackChanges ? RepositoryContext.Set<T>().Where(expression).AsNoTracking() : RepositoryContext.Set<T>().Where(expression);
        public void Create(T entity) => RepositoryContext.Set<T>().Add(entity);
        public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);
        public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);
    }
}
