using Microsoft.AspNetCore.Mvc;
using StateManagement.Entities;
using StateManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StateManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlowController : Controller
    {
        private readonly IFlowApiClient _flowApiClient;

        public FlowController(IFlowApiClient flowApiClient)
        {
            _flowApiClient = flowApiClient;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<FlowEntity> Get(int id)
        {
            return await _flowApiClient.FindByIdAsync(id);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<FlowEntity>> Get()
        {
            var result = await _flowApiClient.GetAllAsync();
            return result;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<FlowEntity> Add(string name)
        {
            var model = new FlowEntity();
            model.Name = name;
            var result = await _flowApiClient.AddAsync(model);
            return result;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<FlowEntity> Update(int id, string name)
        {
            var model = await _flowApiClient.FindByIdAsync(id);
            if (model != null)
            {
                model.Name = name;
                model = await _flowApiClient.UpdateAsync(model);
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
                var model = await _flowApiClient.DeleteAsync(id);
                result = model != null;
            }
            return result;
        }
    }
}
