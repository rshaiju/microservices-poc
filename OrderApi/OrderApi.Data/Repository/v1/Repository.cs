using Microsoft.Toolkit.Diagnostics;
using OrderApi.Data.Database;

namespace OrderApi.Data.Repository.v1
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly OrderContext orderContext;

        public Repository(OrderContext orderContext)
        {
            this.orderContext = orderContext;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            Guard.IsNotNull(entity, nameof(entity));

            try
            {
                await orderContext.AddAsync(entity);
                await orderContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {

                throw new Exception("Error adding entity");
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            try
            {
                return orderContext.Set<TEntity>();
            }
            catch (Exception)
            {

                throw new Exception("Error reading entities");
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            Guard.IsNotNull(entity, nameof(entity));

            try
            {
                orderContext.Update(entity);
                await orderContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {

                throw new Exception("Error updating entity");
            }
        }

        public async Task UpdateRangeAsync(List<TEntity> entities)
        {
            try
            {
                orderContext.UpdateRange(entities);
                await orderContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Error updating entities");
            }
        }
    }
}
