#region

using DocRecycle.Database.Models;

#endregion

namespace DocRecycle.Database.Repositories
{
    public class TemplateRepository : GenericRepository<Template>
    {
        /// <inheritdoc />
        public TemplateRepository(DocsDatabase context) : base(context)
        {
        }
    }
}