#region

using DocRecycle.Database.Models;

#endregion

namespace DocRecycle.Database.Repositories
{
    public class DocumentRepository : GenericRepository<Document>
    {
        /// <inheritdoc />
        public DocumentRepository(DocsDatabase context) : base(context)
        {
        }
    }
}