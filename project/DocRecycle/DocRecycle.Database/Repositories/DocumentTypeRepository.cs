#region

using DocRecycle.Database.Models;

#endregion

namespace DocRecycle.Database.Repositories
{
    public class DocumentTypeRepository : GenericRepository<DocumentType>
    {
        /// <inheritdoc />
        public DocumentTypeRepository(DocsDatabase context) : base(context)
        {
        }
    }
}