using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order_Management.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Management.Repository.Data.Configurations
{
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.HasKey(o => o.OrderId);

			builder.Property(o => o.TotalAmount).IsRequired()
				.HasColumnType("decimal(18,2)");
		
			builder.HasMany(o => o.OrderItems)
				.WithOne()
				.HasForeignKey(oi => oi.OrderId);

			builder.HasOne(o => o.Customer)
				.WithMany(c => c.Orders)
				.HasForeignKey(o => o.CustomerId);

			builder.HasOne(o => o.Invoice)
			   .WithOne(i => i.Order)
			   .HasForeignKey<Invoice>(i => i.OrderId);
		}
	}
}
