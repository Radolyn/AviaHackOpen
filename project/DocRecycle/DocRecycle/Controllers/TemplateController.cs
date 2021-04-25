#region

using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using DocRecycle.Database.Models;
using DocRecycle.Database.Repositories;
using DocRecycle.Models;
using DocRecycle.Signer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace DocRecycle.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TemplateController : ControllerBase
    {
        public TemplateController(UserRepository userRepository, TemplateRepository templateRepository,
            ServiceRepository serviceRepository)
        {
            UserRepository = userRepository;
            TemplateRepository = templateRepository;
            ServiceRepository = serviceRepository;
        }

        public UserRepository UserRepository { get; }
        public TemplateRepository TemplateRepository { get; }
        public ServiceRepository ServiceRepository { get; }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add([FromForm] TemplateDto data)
        {
            var service = ServiceRepository.GetById(data.ServiceId);

            if (service == null)
                return BadRequest();

            var filename = Path.Combine(AppContext.BaseDirectory, "templates", Path.GetRandomFileName());
            await using var f = System.IO.File.Open(filename, FileMode.Create);
            var copyTask = data.File.CopyToAsync(f).ConfigureAwait(false);

            var template = new Template
            {
                Name = data.Name,
                Service = service,
                File = filename
            };

            service.Templates.Add(template);
            ServiceRepository.Update(service);

            TemplateRepository.Add(template);

            // todo: ???
            await TemplateRepository.Save();
            await ServiceRepository.Save();

            await copyTask;

            return Ok(new {template.Id});
        }

        [HttpPost("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Sign(int id)
        {
            var user = UserRepository.GetById(int.Parse(User.FindFirst(ClaimTypes.Sid).Value));
            var template = TemplateRepository.GetById(id);

            if (template == null)
                return NotFound();

            await using var image = System.IO.File.Open("E:\\docs\\sign.jpg", FileMode.Open);

            var context = new SignerContext
            {
                User = user,
                SignImage = image
            };

            var file = SignerService.Sign(template.File, context);

            var f = System.IO.File.Open(file, FileMode.Open);
            return File(f, "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
        }

        [HttpGet("{id:int}/preview")]
        public async Task<IActionResult> Preview(int id, string secret = null)
        {
            var user = UserRepository.GetBySecret(secret);

            if (user == null)
                return Unauthorized();

            var template = TemplateRepository.GetById(id);

            if (template == null)
                return NotFound();

            var context = new SignerContext
            {
                User = user
            };

            var file = SignerService.Preview(template.File, context);

            var f = System.IO.File.Open(file, FileMode.Open);
            return File(f, "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
        }
    }
}