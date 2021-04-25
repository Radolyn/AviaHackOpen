#region

using System.ComponentModel.DataAnnotations;

#endregion

namespace DocRecycle.Models
{
    public class AuthPnr
    {
        [Required] public string Pnr { get; set; }
    }
}