using Microsoft.AspNetCore.Mvc;
using StateManagement.Entities;
using StateManagement.Models;
using StateManagement.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StateManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : Controller
    {
        private readonly ITaskApiClient _taskApiClient;

        public TaskController(ITaskApiClient taskApiClient)
        {
            _taskApiClient = taskApiClient;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<TaskEntity> Get(int id)
        {
            return await _taskApiClient.FindByIdAsync(id);
        }

        [HttpGet]
        [Route("GetByFlowId")]
        public async Task<List<TaskEntity>> GetByFlowId(int id)
        {
            return await _taskApiClient.GetByFlowIdAsync(id);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<TaskEntity>> Get()
        {
            return await _taskApiClient.GetAllAsync();
        }

        [HttpPost]
        [Route("Add")]
        public async Task<TaskEntity> Add(TaskAddRequestModel requestModel)
        {
            var model = new TaskEntity();
            model.Id = 0;
            model.Name = requestModel.Name;
            model.Description = requestModel.Description;
            model.FlowId = requestModel.FlowId;
            model.StateId = requestModel.StateId;

            var result = await _taskApiClient.InsertOrUpdateAsync(model);
            return result;
        }

        [HttpPost]
        [Route("GoToNextState")]
        public async Task<TaskEntity> GoToNextState(int taskId)
        {
            return await _taskApiClient.GoToNextStateAsync(taskId);
        }

        [HttpPost]
        [Route("GoToState")]
        public async Task<TaskEntity> GoToState(int taskId, int stateId)
        {
            return await _taskApiClient.GoToStateAsync(taskId, stateId);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<TaskEntity> Update(TaskUpdateRequestModel requestModel)
        {
            var model = await _taskApiClient.FindByIdAsync(requestModel.Id);
            if (model != null)
            {
                model.Name = requestModel.Name;
                model.Description = requestModel.Description;
                model = await _taskApiClient.InsertOrUpdateAsync(model);
            }
            return model;
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<bool> Delete(int id)
        {
            var result = false;
            if (id > 0)
            {
                var model = await _taskApiClient.DeleteAsync(id);
                result = model != null;
            }
            return result;
        }
    }
}
