using Microsoft.AspNetCore.Mvc;
using StateManagement.Entities;
using StateManagement.Models;
using StateManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StateManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : Controller
    {
        private readonly IStateApiClient _stateApiClient;

        public StateController(IStateApiClient stateApiClient)
        {
            _stateApiClient = stateApiClient;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<StateEntity> Get(int id)
        {
            return await _stateApiClient.FindByIdAsync(id);
        }

        [HttpGet]
        [Route("GetByFlowId")]
        public async Task<List<StateEntity>> GetByFlowId(int id)
        {
            return await _stateApiClient.GetByFlowIdAsync(id);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<StateEntity>> Get()
        {
            var result = await _stateApiClient.GetAllAsync();
            return result;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<StateEntity> Add(StateAddRequestModel requestModel)
        {
            var model = new StateEntity();
            model.Name = requestModel.Name;
            model.Sequence = requestModel.Sequence;
            model.FlowId = requestModel.FlowId;
            var result = await _stateApiClient.AddAsync(model);
            return result;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<StateEntity> Update(StateUpdateRequestModel requestModel)
        {
            var model = await _stateApiClient.FindByIdAsync(requestModel.Id);
            if (model != null)
            {
                model.Name = requestModel.Name;
                model = await _stateApiClient.UpdateAsync(model);
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
                var model = await _stateApiClient.DeleteAsync(id);
                result = model != null;
            }
            return result;
        }
    }
}
