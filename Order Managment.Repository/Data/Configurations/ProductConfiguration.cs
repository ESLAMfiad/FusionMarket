using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order_Management.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Managment.Repository.Data.Configurations
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.HasKey(p => p.ProductId);

			builder.Property(p => p.Name)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(p => p.Price).IsRequired()
				.HasColumnType("decimal(18,2)");

			builder.Property(p => p.Stock)
				.IsRequired();


		}
	}
}
