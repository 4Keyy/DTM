using System.Data.Common;

namespace Application.FunctionalTests
{
    public interface ITestDatabase
    {
        public Task InitialiseAsync();

        public DbConnection GetConnection();

        public Task ResetAsync();

        public Task DisposeAsync();
    }
}
