using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StateManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StateManagement.Services
{
    public interface IStateApiClient : IBaseApiClient<StateEntity>
    {
        Task<List<StateEntity>> GetByFlowIdAsync(int id);
    }
    public class StateApiClient : BaseApiClient<StateEntity>, IStateApiClient
    {
        public StateApiClient(IConfiguration configuration, DbContextOptions dbContextOptions) : base(configuration, dbContextOptions)
        {
        }

        public async Task<List<StateEntity>> GetByFlowIdAsync(int id)
        {
            return await _dataSourceContext.States.Where(s => s.FlowId == id && !s.IsDeleted).OrderBy(o => o.Sequence).ToListAsync();
        }

        public override async Task<StateEntity> AddAsync(StateEntity requestModel)
        {
            var model = new StateEntity();
            model.Sequence = requestModel.Sequence;
            model.FlowId = requestModel.FlowId;
            model.Name = requestModel.Name;
            var states = await GetByFlowIdAsync(requestModel.FlowId);
            if (states.Where(s=>s.Sequence == requestModel.Sequence).Any())
            {
                return null;
            }
            model = await base.AddAsync(model);
            return model;
        }
    }
}
