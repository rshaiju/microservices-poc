using CustomerApi.Data.Database;
using CustomerApi.Domain.Entities;
using Microsoft.Toolkit.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApi.Data.Repository.v1
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly CustomerContext customerContext;

        public Repository(CustomerContext customerContext)
        {
            this.customerContext = customerContext;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            Guard.IsNotNull(entity, nameof(entity));

            try
            { 
                await customerContext.AddAsync(entity);
                await customerContext.SaveChangesAsync();
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
                return customerContext.Set<TEntity>();
            }
            catch (Exception)
            {

                throw new Exception("Error reading customers");
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            Guard.IsNotNull(entity, nameof(entity));

            try
            {
                customerContext.Update(entity);
                await customerContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {

                throw new Exception("Error updating entity");
            }
        }
    }
}
