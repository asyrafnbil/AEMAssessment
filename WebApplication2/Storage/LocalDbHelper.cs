using AEMAssessment.Models;
using Microsoft.EntityFrameworkCore;

namespace AEMAssessment.Storage
{
    public interface IContextBuilder
    {
        LocalDbContext GetContext();
    }

    public class ContextBuilder : IContextBuilder
    {
        public readonly string _connectionString;

        public ContextBuilder(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("default");
        }

        public LocalDbContext GetContext()
        {
            var options = DbContextOptions();
            return new LocalDbContext(options);
        }

        private DbContextOptions<LocalDbContext> DbContextOptions()
        {
            var options = new DbContextOptionsBuilder<LocalDbContext>().UseSqlServer(_connectionString).Options;
            return options;
        }
    }
}