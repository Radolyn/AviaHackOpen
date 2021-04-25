#region

using System.Linq;
using System.Threading.Tasks;
using DocRecycle.Database.Models;
using DocRecycle.Database.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace DocRecycle.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class DocumentTypeController : ControllerBase
    {
        public DocumentTypeController(DocumentTypeRepository documentTypeRepository)
        {
            DocumentTypeRepository = documentTypeRepository;
        }

        public DocumentTypeRepository DocumentTypeRepository { get; }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] DocumentType documentType)
        {
            DocumentTypeRepository.Add(documentType);
            await DocumentTypeRepository.Save();

            return Ok(new {documentType.Id});
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var documents = DocumentTypeRepository.GetAll().ToList();

            return Ok(documents);
        }
    }
}