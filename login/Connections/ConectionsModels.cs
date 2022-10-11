using Microsoft.EntityFrameworkCore;

namespace login.Connections
{
    public class ConectionsModels : DbContext
    {
        public ConectionsModels(DbContextOptions options) : base(options)
        {
        }
    }
}
