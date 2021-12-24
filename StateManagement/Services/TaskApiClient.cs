using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StateManagement.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StateManagement.Services
{
    public interface ITaskApiClient : IBaseApiClient<TaskEntity>
    {
        Task<List<TaskEntity>> GetByFlowIdAsync(int id);
        Task<TaskEntity> GoToNextStateAsync(int id);
        Task<TaskEntity> GoToStateAsync(int id, int stateId);
        Task<TaskEntity> InsertOrUpdateAsync(TaskEntity requestModel);

    }
    public class TaskApiClient : BaseApiClient<TaskEntity>, ITaskApiClient
    {
        private StateApiClient _stateApiClient { get; set; }
        private TaskHistoryApiClient _taskHistoryApiClient { get; set; }

        public TaskApiClient(IConfiguration configuration, DbContextOptions dbContextOptions) : base(configuration, dbContextOptions)
        {
            _stateApiClient = new StateApiClient(configuration, dbContextOptions);   
        }

        public async Task<List<TaskEntity>> GetByFlowIdAsync(int id)
        {
            return await _dataSourceContext.Tasks.Where(s => s.FlowId == id && !s.IsDeleted).OrderByDescending(od => od.Updated).ToListAsync();
        }

        public async Task<TaskEntity> GoToNextStateAsync(int id)
        {
            var task = await FindByIdAsync(id);
            if (task != null)
            {
                var states = await _stateApiClient.GetByFlowIdAsync(task.FlowId);
                var currentIndex = states.FindIndex(s => s.Id == task.StateId);
                if (currentIndex != -1 && currentIndex != states.Count() - 1)
                {
                    var nextState = states[currentIndex + 1];
                    task.StateId = nextState.Id;
                    task = await UpdateAsync(task);
                }
            }
            return task;
        }

        public async Task<TaskEntity> GoToStateAsync(int id, int stateId)
        {
            var task = await FindByIdAsync(id);

            if (task != null)
            {
                var states = await _stateApiClient.GetByFlowIdAsync(task.FlowId);
                var currentIndex = states.FindIndex(s => s.Id == task.StateId);
                var goToStateIndex = states.FindIndex(s => s.Id == stateId);
                if (currentIndex != -1 && goToStateIndex != -1 && (goToStateIndex - currentIndex <= 1))
                {
                    task.StateId = states[goToStateIndex].Id;
                    task = await UpdateAsync(task);
                }
            }
            return task;
        }

        public async Task<TaskEntity> InsertOrUpdateAsync(TaskEntity requestModel)
        {
            _taskHistoryApiClient = new TaskHistoryApiClient(this._configuration, this._dbContextOptions);

            if (requestModel.Id > 0)
            {
                var taskHistory = new TaskHistoryEntity();
                taskHistory.TaskId = requestModel.Id;
                taskHistory.Description = requestModel.Description;
                taskHistory.FlowId = requestModel.FlowId;
                taskHistory.Name = requestModel.Name;
                taskHistory.StateId = requestModel.StateId;

                var taskModel = await base.UpdateAsync(requestModel);
                await _taskHistoryApiClient.AddAsync(taskHistory);
                return taskModel;
            }
            else
            {
                var taskHistory = new TaskHistoryEntity();
                taskHistory.Description = requestModel.Description;
                taskHistory.FlowId = requestModel.FlowId;
                taskHistory.Name = requestModel.Name;
                var states = await _stateApiClient.GetByFlowIdAsync(requestModel.FlowId);
                var stateIndex = states.FindIndex(s => s.Id == requestModel.Id);
                requestModel.StateId = stateIndex == 0 ? requestModel.StateId : states[0].Id;
                taskHistory.StateId = requestModel.StateId;
                var taskModel = await base.AddAsync(requestModel);
                taskHistory.TaskId = taskModel.Id;
                await _taskHistoryApiClient.AddAsync(taskHistory);
                return taskModel;
            }
        }
    }
}
