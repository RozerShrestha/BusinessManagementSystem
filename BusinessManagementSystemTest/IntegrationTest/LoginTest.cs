using BusinessManagementSystem.Data;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Repositories;
using BusinessManagementSystem.Services;
using BusinessManagementSystem.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessManagementSystemTest.IntegrationTest
{
    [TestFixture]
    public class LoginTest
    {
        ILogin<LoginResponseDto> _iLogin;
        private ApplicationDBContext _dbContext;
        public readonly IConfiguration _config;

        
        [SetUp]
        public void Setup()
        {
            var connectionString = "Data Source=ICS-LT-7PVSFY3;Initial Catalog=BusinessManagement;TrustServerCertificate=True;User Id=sa;Password=P@ssw0rd";
            var options = new DbContextOptionsBuilder<ApplicationDBContext>().UseSqlServer(connectionString);
            _dbContext = new ApplicationDBContext(options);
            _iLogin = new LoginRepository(_dbContext, _config);
        }

        [TearDown]
        public void TearDown()
        {
            // Clear the in-memory database after each test
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [Test]
        public void RegisterCustomer_Success()
        {
            UserDto userDto = new UserDto()
            {
                UserId = 1,
                UserName = "rozer.shrestha",
                Email = "rozer.shrestha611@gmail.com",
                Password = "P@ssw0rd",
                FullName = "Rozer Shrestha",
                Address = "Bhimsensthan",
                DateOfBirth = "1991/03/01",
                MobileNumber = "9818136562",
                Gender = "Male",
                Occupation = "IT",
                RoleId = 0,
                RoleName = SD.Role_Superadmin
            };

            var response = _iLogin.Register_User(userDto);
            Assert.Pass();
        }
    }
}