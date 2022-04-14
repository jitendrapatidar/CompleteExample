using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using CompleteExample.Entities;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Common;
using System.Data;

namespace CompleteExample.Logic.Repository
{
    public class GenericRepository<TEntity> where TEntity : class
    {


        #region Private member variables...

        internal CompleteExampleDBContext Context;
        internal DbSet<TEntity> DbSet;

        #endregion

        #region Public Constructor...
        /// <summary>
        /// Public Constructor,initializes privately declared local variables.
        /// </summary>
        /// <param name="context"></param>
        public GenericRepository(CompleteExampleDBContext context)
        {
            this.Context = context;
            this.DbSet = context.Set<TEntity>();

        }
        #endregion

        #region Commit
        public virtual void Commit()
        {
            try
            {
                
                Context.SaveChanges();
                
            }
            catch  
            {


              
            }

        }
        public virtual void CommitAsync()
        {
            try
            {
               
                Context.SaveChangesAsync();
               
            }
            catch
            {

                


            }

        }
        #endregion

        #region GET
        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<TEntity> GetAsync(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where)
        {
            return await Context.Set<TEntity>().Where(where).ToListAsync();
        }

        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match)
        {
            return await Context.Set<TEntity>().SingleOrDefaultAsync(match);

        }
        public virtual async Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match)
        {
            return await Context.Set<TEntity>().Where(match).ToListAsync();
        }
       
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }
        public async Task<TEntity> GetSingleAsync(Func<TEntity, bool> predicate)
        {
            await Task.Delay(0);
            return DbSet.Single<TEntity>(predicate);
        }


        /// <summary>
        /// generic Get method for Entities
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Get()
        {

            IQueryable<TEntity> query = DbSet;
            return query.ToList();
        }
        /// <summary>
        /// Generic get method on the basis of id for Entities.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity GetById(object id)
        {
            return DbSet.Find(id);
        }
        
      

        /// <summary>
        /// generic method to get many record on the basis of a condition.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetMany(Func<TEntity, bool> where)
        {
            return DbSet.Where(where).ToList();
        }
       
        public virtual TEntity Find(Expression<Func<TEntity, bool>> match)
        {

            return Context.Set<TEntity>().SingleOrDefault(match);
        }
      
        public virtual ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> match)
        {
            return Context.Set<TEntity>().Where(match).ToList();
        }
        

        /// <summary>
        /// generic get method , fetches data for the entities on the basis of condition.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public TEntity Get(Func<TEntity, Boolean> where)
        {
            return DbSet.Where(where).FirstOrDefault<TEntity>();
        }
        /// <summary>
        /// generic method to fetch all the records from db
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return DbSet.ToList();
        }

       

        /// <summary>
        /// Inclue multiple
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        public IQueryable<TEntity> GetWithInclude(Expression<Func<TEntity, bool>> predicate, params string[] include)
        {
            IQueryable<TEntity> query = this.DbSet;
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            return query.Where(predicate);
        }

        /// <summary>
        /// Generic method to check if entity exists
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public bool Exists(object primaryKey)
        {
            return DbSet.Find(primaryKey) != null;
        }

        /// <summary>
        /// Gets a single record by the specified criteria (usually the unique identifier)
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A single record that matches the specified criteria</returns>
        public TEntity GetSingle(Func<TEntity, bool> predicate)
        {
            return DbSet.Single<TEntity>(predicate);
        }
       

        /// <summary>
        /// The first record matching the specified criteria
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A single record containing the first record matching the specified criteria</returns>
        public TEntity GetFirst(Func<TEntity, bool> predicate)
        {
            return DbSet.First<TEntity>(predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <param name="navigationProperties"></param>
        /// <returns></returns>
        public virtual TEntity GetSingle(Func<TEntity, bool> where,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            TEntity item = null;
            using (var context = new CompleteExampleDBContext())
            {
                IQueryable<TEntity> dbQuery = context.Set<TEntity>();

                //Apply eager loading
                foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

                item = dbQuery
                    .AsNoTracking() //Don't track any changes for the selected item
                    .FirstOrDefault(where); //Apply where clause
            }
            return item;
        }





        #endregion
        #region Insert

        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }
        public virtual void Insert(IEnumerable<TEntity> entity)
        {
           
            DbSet.AddRange(entity);
           
        }
        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
         
            return entity;
        }
        public async Task<IEnumerable<TEntity>> InsertAsync(IEnumerable<TEntity> entity)
        {
            await Context.Set<TEntity>().AddRangeAsync(entity);
           
            return entity;
        }
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
            
            return entity;
        }


        #endregion
        #region Delete

        /// <summary>
        /// Generic Delete method for the entities
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(object id)
        {
            TEntity entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual async Task<int> DeleteAsync(object id)
        {
            TEntity entityToDelete = DbSet.Find(id);
            return await DeleteAsync(entityToDelete);
        }
        /// <summary>
        /// Generic Delete method for the entities
        /// </summary>
        /// <param name="entityToDelete"></param>
        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }
        public virtual async Task<int> DeleteAsync(TEntity t)
        {
            Context.Set<TEntity>().Remove(t);
            return await Context.SaveChangesAsync();
        }
        /// <summary>
        /// generic delete method , deletes data for the entities on the basis of condition.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public void Delete(Func<TEntity, Boolean> where)
        {
            IQueryable<TEntity> objects = DbSet.Where<TEntity>(where).AsQueryable();
            foreach (TEntity obj in objects)
                DbSet.Remove(obj);
        }

        #endregion
        #region Update

        /// <summary>
        /// Generic update method for the entities
        /// </summary>
        /// <param name="entityToUpdate"></param>
        public virtual void Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        public virtual async Task<TEntity> UpdateAsync(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            
            Context.Entry(entityToUpdate).State = EntityState.Modified;
            await Context.SaveChangesAsync();
            return entityToUpdate;
        }
        public virtual TEntity Update(TEntity updated, int key)
        {
            if (updated == null)
                return null;

            TEntity existing = Context.Set<TEntity>().Find(key);
            if (existing != null)
            {
                Context.Entry(existing).CurrentValues.SetValues(updated);
                Context.SaveChanges();
            }
            return existing;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity updated, int key)
        {
            if (updated == null)
                return null;

            TEntity existing = await Context.Set<TEntity>().FindAsync(key);
            if (existing != null)
            {
                Context.Entry(existing).CurrentValues.SetValues(updated);
                await Context.SaveChangesAsync();
            }
            return existing;
        }

        #endregion

        #region procedure and query 


        public IQueryable<TEntity> FromSqlExecuteQuery(string spQuery, object[] parameters)
        {
            IQueryable<TEntity> queryable = DbSet.FromSqlRaw(spQuery, parameters);
            return queryable;

        }
        public IQueryable<TEntity> FromSqlExecuteQuery(string spQuery)
        {
            IQueryable<TEntity> queryable = DbSet.FromSqlRaw(spQuery);
            return queryable;
        }

        

        public TEntity FromSqlExecuteQuerySingle(string spQuery, object[] parameters)
        {
            return DbSet.FromSqlRaw(spQuery, parameters).FirstOrDefault();
        }
        public TEntity FromSqlExecuteQuerySingle(string spQuery)
        {
            return DbSet.FromSqlRaw(spQuery).FirstOrDefault();
        }
        public IQueryable<TEntity> FromSqlEntity(string spQuery)
        {
            IQueryable<TEntity> queryable = DbSet.FromSqlRaw(spQuery);

            return queryable;
        }
        public IQueryable<dynamic> FromSqlDynamic(string spQuery)
        {
            IQueryable<dynamic> queryable = DbSet.FromSqlRaw(spQuery);

            return queryable;
        }

        public dynamic FromSqlDynamicSingle(string spQuery)
        {
            dynamic queryable = DbSet.FromSqlRaw(spQuery);
            return queryable;
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="query"></param>
        public virtual void QueryContextExt(string query)
        {
            Context.Database.ExecuteSqlRaw(query);//<int>(query).FirstOrDefault();
        }

        public virtual async void QueryContextExtAsync(string query)
        {
            await Context.Database.ExecuteSqlRawAsync(query);//<int>(query).FirstOrDefault();
        }



        //   RawSqlQuery(
        // "SELECT TOP 10 Name, COUNT(*) FROM Users U"
        // + " INNER JOIN Signups S ON U.UserId = S.UserId"
        // + " GROUP BY U.Name ORDER BY COUNT(*) DESC",
        //  x => new TopUser { Name = (string) x[0], Count = (int)x[1] });

        public static List<T> RawSqlQuery<T>(string query, Func<DbDataReader, T> map)
        {
            var context = new CompleteExampleDBContext();

            var command = context.Database.GetDbConnection().CreateCommand();

            command.CommandText = query;
            command.CommandType = CommandType.Text;

            context.Database.OpenConnection();

            var result = command.ExecuteReader();

            var entities = new List<T>();

            while (result.Read())
            {
                entities.Add(map(result));
            }

            return entities;
        }
        public static List<T> RawSqlQuery<T>(string query)
        {
            var context = new CompleteExampleDBContext();

            var command = context.Database.GetDbConnection().CreateCommand();

            command.CommandText = query;
            command.CommandType = CommandType.Text;

            context.Database.OpenConnection();

            var result = command.ExecuteReader();

            var entities = new List<T>();
           
            return entities;
        }
        #endregion
        #region private dispose variable declaration...
        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                    Context.Dispose();
                    Context = null;
                }
            }
            this.disposed = true;
        }

        #endregion
    }
}
 