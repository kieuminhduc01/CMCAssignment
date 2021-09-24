using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Domain.Data
{
    class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var dbBuilder = new DbContextOptionsBuilder<DataContext>();
            dbBuilder.UseNpgsql(@"Server=localhost;Port=5432;Database=AssMemberManagement;User Id=postgres;Password=sa;");
            return new DataContext(dbBuilder.Options);
        }
    }
}
