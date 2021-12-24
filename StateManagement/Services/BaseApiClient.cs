using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StateManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StateManagement.Services
{
    public interface IBaseApiClient<TEntity>
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> FindByIdAsync(int id);
        Task<TEntity> AddAsync(TEntity model);
        Task<TEntity> UpdateAsync(TEntity model);
        Task<TEntity> DeleteAsync(int id);
    }

    public class BaseApiClient<TEntity> : IBaseApiClient<TEntity> where TEntity : BaseEntity
    {
        public DataSourceContext _dataSourceContext { get; set; }
        public IConfiguration _configuration { get; set; }
        public readonly DbContextOptions _dbContextOptions;

        public BaseApiClient(IConfiguration configuration, DbContextOptions dbContextOptions)
        {
            _configuration = configuration;
            _dbContextOptions = dbContextOptions;
            _dataSourceContext = new DataSourceContext(dbContextOptions);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _dataSourceContext.Set<TEntity>().Where(s => !s.IsDeleted).OrderByDescending(od => od.Id).ToListAsync();
        }

        public async Task<TEntity> FindByIdAsync(int id)
        {
            return await _dataSourceContext.Set<TEntity>().FirstOrDefaultAsync(s => !s.IsDeleted && s.Id == id);
        }

        public virtual async Task<TEntity> AddAsync(TEntity model)
        {
            model.Created = DateTime.Now;
            model.Updated = DateTime.Now;
            model.IsDeleted = false;

            await _dataSourceContext.Set<TEntity>().AddAsync(model);
            await _dataSourceContext.SaveChangesAsync();
            return model;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity model)
        {
            model.Updated = DateTime.Now;
            _dataSourceContext.Set<TEntity>().Update(model);
            _dataSourceContext.Entry(model).State = EntityState.Modified;
            await _dataSourceContext.SaveChangesAsync();
            return model;
        }

        public async Task<TEntity> DeleteAsync(int id)
        {
            var model = await FindByIdAsync(id);
            model.IsDeleted = true;
            return await UpdateAsync(model);
        }

    }
}
