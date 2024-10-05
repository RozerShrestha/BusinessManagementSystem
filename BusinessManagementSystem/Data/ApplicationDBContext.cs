using BusinessManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Data.SqlClient;

namespace BusinessManagementSystem.Data
{
    public class ApplicationDBContext : DbContext
    {
        //Adding Domain Classes as DbSet Properties
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<BasicConfiguration> BasicConfigurations { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<MenuRole> MenuRoles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        //Constructor calling the Base DbContext Class Constructor
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            
        }
        public ApplicationDBContext(DbContextOptionsBuilder<ApplicationDBContext> options)
        {

        }
        //OnConfiguring() method is used to select and configure the data source
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //test
            var configuation = GetConfiguration();
            var con = new SqlConnection(configuation.GetSection("ConnectionStrings").GetSection("BMSConnection").Value);
            optionsBuilder.UseSqlServer(con.ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new MenuEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new MenuRoleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new BasicConfigurationEntityConfiguration());


        }
        private IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }

        public override int SaveChanges()
        {
            try
            {
                AddTimestamps();
            }
            catch (Exception)
            {

            }
            return base.SaveChanges();
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddTimestamps();
            return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries();
            //.Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entity in entities)
            {
                var now = DateTime.Now; // current datetime

                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedAt = now;
                }
                ((BaseEntity)entity.Entity).UpdatedAt = now;
            }
        }

    }
}
