#region

using System.ComponentModel.DataAnnotations;

#endregion

namespace DocRecycle.Models
{
    public class ServiceDto
    {
        [Required] public string Name { get; set; }
    }
}