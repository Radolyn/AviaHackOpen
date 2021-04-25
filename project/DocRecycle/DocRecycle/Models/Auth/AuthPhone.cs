#region

using System.ComponentModel.DataAnnotations;

#endregion

namespace DocRecycle.Models
{
    public class AuthPhone
    {
        [Required] [Phone] public string Phone { get; set; }
    }
}