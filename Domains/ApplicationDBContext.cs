using Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Utilities.Commons;
using Utilities.Constants;

namespace Domains
{
    public class ApplicationDBContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationDBContext()
        {
        }

        public ApplicationDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products  { get; set; }
        public DbSet<Customer> Customers  { get; set; }
        public DbSet<EmployeeNew> EmployeeNews  { get; set; }
        public DbSet<Order> Orders  { get; set; }
        public DbSet<OrderDetail> OrderDetails  { get; set; }
        public DbSet<Region> Regions  { get; set; }
        public DbSet<Shipper> Shippers  { get; set; }
        public DbSet<Supplier> Suppliers  { get; set; }
        public DbSet<Territory> Territories  { get; set; }



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