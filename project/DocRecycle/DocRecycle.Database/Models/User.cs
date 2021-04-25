#region

using System.Collections.Generic;

#endregion

namespace DocRecycle.Database.Models
{
    public class User : IEntity
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }

        public string Secret { get; set; }

        public ICollection<Document> Documents { get; set; } = new List<Document>();

        /// <inheritdoc />
        public int Id { get; set; }
    }
}