using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StateManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StateManagement.Services
{
    public interface IFlowApiClient: IBaseApiClient<FlowEntity>
    {
        Task<FlowEntity> GetById(int id);
    }

    public class FlowApiClient: BaseApiClient<FlowEntity>, IFlowApiClient
    {
        public FlowApiClient(IConfiguration configuration, DbContextOptions dbContextOptions): base(configuration, dbContextOptions)
        {
        }

        public async Task<FlowEntity> GetById(int id)
        {
            var result = await _dataSourceContext.Flows.FirstOrDefaultAsync(s=>s.Id == id && !s.IsDeleted);
            
            return result;
        }
    }
}
