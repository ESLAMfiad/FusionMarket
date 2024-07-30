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
	public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
	{
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			builder.HasKey(oi => oi.OrderItemId);

			//builder.Property(oi => oi.OrderId)
			//.IsRequired();

			//builder.Property(oi => oi.ProductId)
			//	.IsRequired();

			//builder.Property(oi => oi.Quantity)
			//	.IsRequired();

			builder.Property(oi => oi.UnitPrice).IsRequired()
				.HasColumnType("decimal(18,2)");

			builder.Property(oi => oi.Discount)
				.HasColumnType("decimal(18,2)");

			builder.HasOne(oi => oi.Order)
			 .WithMany(o => o.OrderItems)
			 .HasForeignKey(oi => oi.OrderId);

			builder.HasOne(oi => oi.Product)
				   .WithMany(p => p.OrderItems)
				   .HasForeignKey(oi => oi.ProductId);

			//builder.HasOne<Order>()
			//	.WithMany(o => o.OrderItems)
			//	.HasForeignKey(oi => oi.OrderId);

			//builder.HasOne<Product>()
			//	.WithMany(p => p.OrderItems)
			//	.HasForeignKey(oi => oi.ProductId);
		}
	}
}
