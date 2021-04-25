namespace DocRecycle.Database.Models
{
    public class Template : IEntity
    {
        public string Name { get; set; }
        public string File { get; set; }

        public Service Service { get; set; }

        /// <inheritdoc />
        public int Id { get; set; }
    }
}