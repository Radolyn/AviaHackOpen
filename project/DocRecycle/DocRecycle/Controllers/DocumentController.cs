#region

using System.Security.Claims;
using System.Threading.Tasks;
using DocRecycle.Database.Models;
using DocRecycle.Database.Repositories;
using DocRecycle.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace DocRecycle.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        public DocumentController(UserRepository userRepository, DocumentRepository documentRepository,
            DocumentTypeRepository documentTypeRepository)
        {
            UserRepository = userRepository;
            DocumentRepository = documentRepository;
            DocumentTypeRepository = documentTypeRepository;
        }

        public UserRepository UserRepository { get; }
        public DocumentRepository DocumentRepository { get; }
        public DocumentTypeRepository DocumentTypeRepository { get; }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] DocumentDto data)
        {
            var type = DocumentTypeRepository.GetById(data.TypeId);
            var user = UserRepository.GetById(int.Parse(User.FindFirst(ClaimTypes.Sid).Value));

            if (type == null)
                return BadRequest();

            var doc = new Document
            {
                User = user,
                Type = type,
                Value = data.Value
            };

            DocumentRepository.Add(doc);
            await DocumentRepository.Save();

            return Ok(new {doc.Id});
        }

        [HttpPost("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] DocumentDto data)
        {
            var doc = DocumentRepository.GetById(id);

            if (doc == null)
                return NotFound();

            doc.Value = data.Value;

            await DocumentRepository.Save();

            return Ok(doc);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var doc = DocumentRepository.GetById(id);

            if (doc == null)
                return NotFound();

            DocumentRepository.Remove(doc);
            await DocumentRepository.Save();

            return Ok();
        }
    }
}