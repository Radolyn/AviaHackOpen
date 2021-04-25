namespace DocRecycle.Database.Models
{
    public class DocumentType : IEntity
    {
        public string Name { get; set; }
        public string Mask { get; set; }

        /// <inheritdoc />
        public int Id { get; set; }
    }
}