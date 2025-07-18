﻿using BusinessManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Data.SqlClient;

namespace BusinessManagementSystem.Data
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
                return base.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            //.Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entity in entities)
            {
                //some entity doesn't have created and updated at
                try
                {
                    if (entity.Entity is BaseEntity baseEntity)
                    {
                        var now = DateTime.Now; // current datetime

                        if (entity.State == EntityState.Added)
                        {
                            baseEntity.CreatedAt = now;
                            baseEntity.UpdatedAt = now;
                            baseEntity.CreatedBy = userName;
                            baseEntity.UpdatedBy = userName;
                        }
                        else if (entity.State == EntityState.Added || entity.State == EntityState.Modified)
                        {
                            baseEntity.UpdatedAt = now;
                            baseEntity.UpdatedBy = userName;

                            var originalCreatedAt = entity.OriginalValues[nameof(BaseEntity.CreatedAt)]==null?now: entity.OriginalValues[nameof(BaseEntity.CreatedAt)];
                            baseEntity.CreatedAt = (DateTime)originalCreatedAt;
                            var originalCreatedBy = entity.OriginalValues[nameof(BaseEntity.CreatedBy)]==null?userName: entity.OriginalValues[nameof(BaseEntity.CreatedBy)];
                            baseEntity.CreatedBy = (string)originalCreatedBy; 
                        }
                    }  
                }
                catch (Exception ex)
                {
                    
                } 
            }
        }
    }
}
