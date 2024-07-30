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
	public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
	{
		public void Configure(EntityTypeBuilder<Customer> builder)
		{
			builder.HasKey(c => c.CustomerId);

			builder.Property(c => c.Name)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(c => c.Email)
				.IsRequired()
				.HasMaxLength(100);

			builder.HasOne(c => c.User)
				.WithOne(u => u.Customer)
				.HasForeignKey<Customer>(c => c.UserId).IsRequired(false); 

			builder.HasMany(c => c.Orders)
				.WithOne(o => o.Customer)
				.HasForeignKey(o => o.CustomerId);
		}
	}
}
