#region

using System.Collections.Generic;

#endregion

namespace DocRecycle.Database.Models
{
    public class Service : IEntity
    {
        public string Name { get; set; }

        public ICollection<Template> Templates { get; set; } = new List<Template>();

        /// <inheritdoc />
        public int Id { get; set; }
    }
}