#region

using System.ComponentModel.DataAnnotations;

#endregion

namespace DocRecycle.Models
{
    public class DocumentDto
    {
        [Required] public string Value { get; set; }

        [Required] public int TypeId { get; set; }
    }
}