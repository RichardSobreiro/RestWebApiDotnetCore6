using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SimpleWebApi.Data.SQLServer.Context;
using SimpleWebApi.Domain.Base;
using SimpleWebApi.Domain.Interfaces;
using System.Linq.Expressions;

namespace SimpleWebApi.Data.SQLServer.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly SQLServerContext sqlServerContext;
        public Repository(SQLServerContext sQLServerContext) 
        { 
            this.sqlServerContext = sQLServerContext;
        }
        protected DbSet<TEntity> DbSet
        {
            get
            {
                return sqlServerContext.Set<TEntity>();
            }
        }

        public TEntity Create(TEntity model)
        {
            try
            {
                DbSet.Add(model);
                Save();
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<TEntity> Create(List<TEntity> models)
        {
            try
            {
                DbSet.AddRange(models);
                Save();
                return models;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Update(TEntity model)
        {
            try
            {
                EntityEntry<TEntity> entry = NewMethod(model);

                DbSet.Attach(model);

                entry.State = EntityState.Modified;

                return Save() > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Update(List<TEntity> models)
        {
            try
            {
                foreach (TEntity register in models)
                {
                    EntityEntry<TEntity> entry = sqlServerContext.Entry(register);
                    DbSet.Attach(register);
                    entry.State = EntityState.Modified;
                }

                return Save() > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Delete(TEntity model)
        {
            try
            {
                if(model is Entity)
                {
                    (model as Entity).IsDeleted = true;
                    EntityEntry<TEntity> entry = sqlServerContext.Entry(model);
                    DbSet.Attach(model);
                    entry.State = EntityState.Modified;
                }
                else
                {
                    EntityEntry<TEntity> _entry = sqlServerContext.Entry(model);
                    DbSet.Attach(model);
                    _entry.State = EntityState.Deleted;
                }
                return Save() > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Delete(params object[] Keys)
        {
            try
            {
                TEntity model = DbSet.Find(Keys);
                return (model != null) && Delete(model);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Delete(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                TEntity model = DbSet.Where<TEntity>(where).FirstOrDefault<TEntity>();
                return (model != null) && Delete(model);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #region 'Methods: Search'
        public TEntity Find(params object[] Keys)
        {
            try
            {
                return DbSet.Find(Keys);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public TEntity Find(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                return DbSet.AsNoTracking().FirstOrDefault(where);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public TEntity Find(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, object> includes)
        {
            try
            {
                IQueryable<TEntity> _query = DbSet;
                if (includes != null)
                    _query = includes(_query) as IQueryable<TEntity>;
                return _query.SingleOrDefault(predicate);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                return DbSet.Where(where);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, object> includes)
        {
            try
            {
                IQueryable<TEntity> _query = DbSet;
                if (includes != null)
                    _query = includes(_query) as IQueryable<TEntity>;
                return _query.Where(predicate).AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
        #region 'Assyncronous Methods'

        public async Task<TEntity> CreateAsync(TEntity model)
        {
            try
            {
                DbSet.Add(model);
                await SaveAsync();
                return model;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity model)
        {
            try
            {
                EntityEntry<TEntity> entry = sqlServerContext.Entry(model);

                DbSet.Attach(model);

                entry.State = EntityState.Modified;

                await SaveAsync();

                return model;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteAsync(TEntity model)
        {
            try
            {
                EntityEntry<TEntity> entry = sqlServerContext.Entry(model);

                DbSet.Attach(model);

                entry.State = EntityState.Deleted;

                return await SaveAsync() > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteAsync(params object[] Keys)
        {
            try
            {
                TEntity model = DbSet.Find(Keys);
                return (model != null) && await DeleteAsync(model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                TEntity model = DbSet.FirstOrDefault(where);

                return (model != null) && await DeleteAsync(model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> SaveAsync()
        {
            try
            {
                return await sqlServerContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region 'Search Methods Async'

        public async Task<TEntity> GetAsync(params object[] Keys)
        {
            try
            {
                return await DbSet.FindAsync(Keys);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                return await DbSet.AsNoTracking().FirstOrDefaultAsync(where);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                return await DbSet.Where(where).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, object> includes)
        {
            try
            {
                IQueryable<TEntity>? _query = DbSet;
                if (includes != null)
                    _query = includes(_query) as IQueryable<TEntity>;
                return await _query.Where(predicate).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion
        public void Dispose()
        {
            try
            {
                if (sqlServerContext != null)
                    sqlServerContext.Dispose();
                GC.SuppressFinalize(this);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int Save()
        {
            try 
            {
                return sqlServerContext.SaveChanges();
            }
            catch(Exception)
            {
                throw;
            }
        }
        private EntityEntry<TEntity> NewMethod(TEntity model)
        {
            return sqlServerContext.Entry(model);
        }
    }
}
