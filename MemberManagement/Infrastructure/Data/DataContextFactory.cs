using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Domain.Data
{
    class DataContextFactory : IDesignTimeDbContextFactory<ApplicationDBContext>
    {
        public ApplicationDBContext CreateDbContext(string[] args)
        {
            var dbBuilder = new DbContextOptionsBuilder<ApplicationDBContext>();
            dbBuilder.UseNpgsql(@"Server=localhost;Port=5432;Database=AssMemberManagement;User Id=postgres;Password=sa;");
            return new ApplicationDBContext(dbBuilder.Options);
        }
    }
}
