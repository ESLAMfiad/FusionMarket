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
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{

			builder.HasKey(u => u.Id);

			builder.Property(u => u.Id)
		   .HasColumnName("UserId");

			builder.Property(u => u.Role)
				.HasMaxLength(50);

			builder.HasOne(u => u.Customer)
				.WithOne(c => c.User)
				.HasForeignKey<Customer>(c => c.UserId);

		}
	}
}
