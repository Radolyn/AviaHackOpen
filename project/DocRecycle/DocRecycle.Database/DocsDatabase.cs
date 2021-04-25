#region

using DocRecycle.Database.Models;
using Microsoft.EntityFrameworkCore;

#endregion

namespace DocRecycle.Database
{
    public class DocsDatabase : DbContext
    {
        public DocsDatabase(DbContextOptions<DocsDatabase> contextOptions) : base(contextOptions)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
    }
}