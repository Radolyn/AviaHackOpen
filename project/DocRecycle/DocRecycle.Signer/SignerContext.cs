#region

using System.IO;
using DocRecycle.Database.Models;

#endregion

namespace DocRecycle.Signer
{
    public class SignerContext
    {
        public User User { get; init; }
        public Stream SignImage { get; set; } // image
    }
}