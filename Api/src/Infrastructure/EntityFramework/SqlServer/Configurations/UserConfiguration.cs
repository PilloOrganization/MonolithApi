using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityFramework.SqlServer.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);

            // Configure properties
            //builder.Property(p => p.Name)
            //    .IsRequired()
            //    .HasMaxLength(200);

            //builder.Property(p => p.Price)
            //    .HasPrecision(18, 2);
        }
    }
}
