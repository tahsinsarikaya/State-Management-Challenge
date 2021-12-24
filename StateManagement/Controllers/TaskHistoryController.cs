using Microsoft.AspNetCore.Mvc;
using StateManagement.Entities;
using StateManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StateManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskHistoryController : Controller
    {
        private readonly ITaskHistoryApiClient _taskHistoryApiClient;

        public TaskHistoryController(ITaskHistoryApiClient taskHistoryApiClient)
        {
            _taskHistoryApiClient = taskHistoryApiClient;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<TaskHistoryEntity> Get(int id)
        {
            return await _taskHistoryApiClient.FindByIdAsync(id);
        }

        [HttpGet]
        [Route("GetByTaskId")]
        public async Task<List<TaskHistoryEntity>> GetByTaskId(int id)
        {
            return await _taskHistoryApiClient.GetByTaskIdAsync(id);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<TaskHistoryEntity>> Get()
        {
            var result = await _taskHistoryApiClient.GetAllAsync();
            return result;
        }

        [HttpPost]
        [Route("GoToHistory")]
        public async Task<TaskHistoryEntity> GoToHistory(int id, int taskId)
        {
            return await _taskHistoryApiClient.GoToHistoryAsync(id, taskId);
        }

    }
}
