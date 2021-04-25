#region

using DocRecycle.Database.Models;

#endregion

namespace DocRecycle.Database.Repositories
{
    public class ServiceRepository : GenericRepository<Service>
    {
        /// <inheritdoc />
        public ServiceRepository(DocsDatabase context) : base(context)
        {
        }
    }
}