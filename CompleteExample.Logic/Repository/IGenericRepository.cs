using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CompleteExample.Logic.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        #region "Async Methods"

        #region "Get"


        Task<TEntity> GetByIdAsync(object id);
        Task<TEntity> GetAsync(int id);
        Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match);
        Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match);

        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetSingleAsync(Func<TEntity, bool> predicate);

        #endregion

        #region "Insert"

        Task<TEntity> InsertAsync(TEntity entity);

        Task<IEnumerable<TEntity>> InsertAsync(IEnumerable<TEntity> entity);

        #endregion

        #region "Delete"

        Task<int> DeleteAsync(object id);

        Task<int> DeleteAsync(TEntity t);

        #endregion

        #region "Update"

        Task<TEntity> UpdateAsync(TEntity entityToUpdate);

        Task<TEntity> UpdateAsync(TEntity updated, int key);

        #endregion

        #region procedure and query 
        IQueryable<TEntity> FromSqlExecuteQuery(string spQuery, object[] parameters);
        IQueryable<TEntity> FromSqlExecuteQuery(string spQuery);
        TEntity FromSqlExecuteQuerySingle(string spQuery);
        IQueryable<TEntity> FromSqlEntity(string spQuery);
        IQueryable<dynamic> FromSqlDynamic(string spQuery);
        #endregion

        #endregion
    }
}
