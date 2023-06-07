using System;
using MicroServer.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroServer.Infrastructure.Configs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("T_Users");
            builder.OwnsOne(user => user.PhoneNumber, nb =>
            {
                nb.Property(x => x.RegionCode).HasMaxLength(5).IsUnicode(false);
                nb.Property(x => x.Number).HasMaxLength(20).IsUnicode(false);
            });

            builder.Property("PasswordHash").HasMaxLength(100).IsUnicode(false);
            builder.HasOne(x => x.AccessFail).WithOne(x => x.User).HasForeignKey<UserAccessFail>(x => x.UserId);
        }
    }
}

