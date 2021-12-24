using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StateManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StateManagement.Services
{
    public interface ITaskHistoryApiClient : IBaseApiClient<TaskHistoryEntity>
    {
        Task<List<TaskHistoryEntity>> GetByTaskIdAsync(int id);
        Task<TaskHistoryEntity> GoToHistoryAsync(int id, int taskId);
    }
    public class TaskHistoryApiClient : BaseApiClient<TaskHistoryEntity>, ITaskHistoryApiClient
    {
        private TaskApiClient _taskApiClient { get; set; }

        public TaskHistoryApiClient(IConfiguration configuration, DbContextOptions dbContextOptions) : base(configuration, dbContextOptions)
        {
            _taskApiClient = new TaskApiClient(configuration, dbContextOptions);
        }

        public async Task<List<TaskHistoryEntity>> GetByTaskIdAsync(int id)
        {
            return await _dataSourceContext.TaskHistories.Where(s => s.TaskId == id && !s.IsDeleted).OrderByDescending(od => od.Id).ToListAsync();
        }

        public async Task<TaskHistoryEntity> GoToHistoryAsync(int id, int taskId)
        {
            var currentTask = await _taskApiClient.FindByIdAsync(taskId);
            var history = await FindByIdAsync(id);

            if (history.TaskId != taskId)
            {
                return history;
            }

            history.Id = 0;

            currentTask.Description = history.Description;
            currentTask.Name = history.Name;
            currentTask.StateId = history.StateId;
            currentTask.FlowId = history.FlowId;

            await _taskApiClient.UpdateAsync(currentTask);
            
            return history;
        }



    }
}
