#region

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocRecycle.Database.Models;
using DocRecycle.Database.Repositories;
using DocRecycle.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

#endregion

namespace DocRecycle.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        public ServiceController(ServiceRepository serviceRepository)
        {
            ServiceRepository = serviceRepository;
        }

        public ServiceRepository ServiceRepository { get; }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ServiceDto data)
        {
            var service = new Service
            {
                Name = data.Name
            };

            ServiceRepository.Add(service);
            await ServiceRepository.Save();

            return Ok(new {service.Id});
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // todo: optimize

            var list = new List<Service>();

            foreach (var service in ServiceRepository.GetAll().Include(x => x.Templates))
            {
                var serviceShadow = new Service
                {
                    Id = service.Id,
                    Name = service.Name,
                    Templates = service.Templates.Select(x => new Template {Id = x.Id, Name = x.Name}).ToList()
                };

                list.Add(serviceShadow);
            }

            return Ok(list);
        }
    }
}