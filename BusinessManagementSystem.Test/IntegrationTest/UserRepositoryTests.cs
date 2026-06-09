using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementSystem.Test.IntegrationTest
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private string _connectionString;

        [SetUp]
        public void Setup()
        {
            _connectionString = TestConfiguration.GetConnectionString();
        }

        [Test]
        public async Task GetUserById_ShouldReturnExpectedUser()
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand("SELECT FullName FROM Users WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", 1);

            var result = await command.ExecuteScalarAsync();

            Assert.AreEqual("Rozer Shrestha", result?.ToString());
        }
    }
}
