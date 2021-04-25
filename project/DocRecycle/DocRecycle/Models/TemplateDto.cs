#region

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

#endregion

namespace DocRecycle.Models
{
    public class TemplateDto
    {
        [Required] public string Name { get; set; }

        [Required] public IFormFile File { get; set; }

        [Required] public int ServiceId { get; set; }
    }
}