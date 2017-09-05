namespace ILearn.Data.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using ILearn.Entities;

    public interface IEntityBaseRepository<T> where T : class, IBaseEntity, new()
    {
        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        
        IQueryable<T> GetAll();
        
        T GetSingle(int id);

        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);

        int Add(T entity);

        void Delete(T entity);

        void Update(T entity);
    }
}