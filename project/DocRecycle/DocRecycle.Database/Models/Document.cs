namespace DocRecycle.Database.Models
{
    public class Document : IEntity
    {
        public DocumentType Type { get; set; }
        public string Value { get; set; }

        public User User { get; set; }

        /// <inheritdoc />
        public int Id { get; set; }
    }
}