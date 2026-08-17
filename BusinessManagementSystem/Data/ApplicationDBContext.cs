using TattooAppointmentSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Data.SqlClient;

namespace TattooAppointmentSystem.Data
{
    public class ApplicationDBContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        //Adding Domain Classes as DbSet Properties
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<BasicConfiguration> BasicConfigurations { get; set; }
        public DbSet<MenuRole> MenuRoles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Referal> Referals { get; set; }
        public DbSet<Tip> Tips { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentHistory> PaymentHistories { get; set; }
        public DbSet<AdvancePayment> AdvancePayments { get; set; }
        public DbSet<OTP> OTPs { get; set; }




        //Constructor calling the Base DbContext Class Constructor
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
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
            modelBuilder.ApplyConfiguration(new MenuRoleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AppointmentEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ReferalEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TipEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AdvancePaymentEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OTPEntityConfiguration()); 

        }
        private IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }
        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddTimestamps();
            return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
        private void AddTimestamps()
        {
            var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "System";
            var entities = ChangeTracker.Entries();
            var now = DateTime.Now;

            foreach (var entity in entities)
            {
                if (entity.Entity is not BaseEntity baseEntity)
                {
                    continue;
                }

                if (entity.State == EntityState.Added)
                {
                    if (baseEntity.CreatedAt == default)
                    {
                        baseEntity.CreatedAt = now;
                    }

                    if (string.IsNullOrWhiteSpace(baseEntity.CreatedBy))
                    {
                        baseEntity.CreatedBy = userName;
                    }

                    baseEntity.UpdatedAt = now;
                    baseEntity.UpdatedBy = userName;
                }
                else if (entity.State == EntityState.Modified)
                {
                    baseEntity.UpdatedAt = now;
                    baseEntity.UpdatedBy = userName;

                    var createdAtProperty = entity.Property(nameof(BaseEntity.CreatedAt));
                    var createdByProperty = entity.Property(nameof(BaseEntity.CreatedBy));

                    if (createdAtProperty.CurrentValue is DateTime createdAt && createdAt == default)
                    {
                        createdAtProperty.CurrentValue = createdAtProperty.OriginalValue;
                    }

                    if (createdByProperty.CurrentValue is null or "")
                    {
                        createdByProperty.CurrentValue = createdByProperty.OriginalValue;
                    }

                    createdAtProperty.IsModified = false;
                    createdByProperty.IsModified = false;
                }
            }
        }
    }
}

