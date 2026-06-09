using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementSystem.Test
{
    public static class TestConfiguration
    {
        private static IConfigurationRoot _configuration;

        static TestConfiguration()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public static string GetConnectionString(string name = "BMSConnectionTest")
        {
            return _configuration.GetConnectionString(name);
        }
    }
}
