using System;
using Microsoft.EntityFrameworkCore;
using Restful.Api.Entities;

namespace Restful.Api.Repositories
{
    public class SqliteDbContext : DbContext
    {
        public SqliteDbContext(DbContextOptions<SqliteDbContext> options)
            : base(options)
        {

        }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
            .Property(x => x.Name).IsRequired().HasMaxLength(100);

            modelBuilder.Entity<Company>()
            .Property(x => x.Introduction).HasMaxLength(500);

            modelBuilder.Entity<Employee>()
            .Property(x => x.EmployeeNo).IsRequired().HasMaxLength(10);

            modelBuilder.Entity<Employee>()
            .Property(x => x.FirstName).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<Employee>()
            .Property(x => x.LastName).IsRequired().HasMaxLength(50);

            //一对多，外键
            modelBuilder.Entity<Employee>()
            .HasOne(x => x.Company)
            .WithMany(x => x.Employees)
            .HasForeignKey(x => x.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

            //初始化种子数据
            InitSeedData(modelBuilder);
        }

        public void InitSeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = Guid.NewGuid(),
                    Name = "Mircosoft",
                    Introduction = "Great Company"
                },
                  new Company
                  {
                      Id = Guid.NewGuid(),
                      Name = "Google",
                      Introduction = "No Evil Company..."
                  },
                  new Company
                  {
                      Id = Guid.NewGuid(),
                      Name = "Mircosoft",
                      Introduction = "Fubao Company..."
                  }
            );

        }
    }
}