using login.Models;
using Microsoft.EntityFrameworkCore;

namespace login.Connections
{
    public class ConectionsModels : DbContext
    {
        public ConectionsModels(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Usuario> usuario { get; set; }
    }
}
