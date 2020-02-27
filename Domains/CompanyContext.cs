using Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Utilities.Commons;
using Utilities.Constants;

namespace Domains
{
    public class CompanyContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public CompanyContext()
        {
        }

        public CompanyContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlServer(SingletonService.Instance.DefaultConnection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().ToTable(Constants.TABLE.COMPANY);
            modelBuilder.Entity<Company>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Company>().Property(c => c.Name).IsRequired();
            modelBuilder.Entity<Company>().Property(c => c.Code).IsRequired();

            modelBuilder.Entity<Employee>().ToTable(Constants.TABLE.EMPLOYEE);
            modelBuilder.Entity<Employee>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Employee>().Property(e => e.FirstName).IsRequired();
            modelBuilder.Entity<Employee>().Property(e => e.LastName).IsRequired();
            modelBuilder.Entity<Employee>().Property(e => e.CompanyId).IsRequired();
        }
    }
}