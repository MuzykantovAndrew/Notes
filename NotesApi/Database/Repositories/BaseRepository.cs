using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotesApi.Database.Context;
using NotesApi.Database.Interfaces;

namespace NotesApi.Database.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext context;
        DbSet<TEntity> dbSet;
        public BaseRepository(DbContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }
        public async Task AddAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task<TEntity> FindByIdAsync(int id)
        {
            return await dbSet.FindAsync();
        }

        public async Task<IEnumerable<TEntity>> ListAsync()
        {
            return await dbSet.ToListAsync();
        }

        public void Remove(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            dbSet.Update(entity);
        }
    }
}