using Microsoft.EntityFrameworkCore;
using Order_Management.Core.Entities;
using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Order_Management.Repository.Data
{
	public class OrderManagementDbContext : IdentityDbContext<User>
	{
        public OrderManagementDbContext(DbContextOptions<OrderManagementDbContext> options):base(options)
        {
                
        }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Invoice> Invoices { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//modelBuilder.ApplyConfiguration(new CustomerConfiguration());
			//modelBuilder.ApplyConfiguration(new OrderConfiguration());
			//modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
			//modelBuilder.ApplyConfiguration(new ProductConfiguration());
			//modelBuilder.ApplyConfiguration(new InvoiceConfiguration());
			//modelBuilder.ApplyConfiguration(new UserConfiguration());
			//modelBuilder.Entity<Order>()
			

			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			base.OnModelCreating(modelBuilder);
		}
	}
}
