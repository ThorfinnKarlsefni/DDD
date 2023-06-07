using System;
using MicroServer.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroServer.Infrastructure.Configs
{
    public class UserLoginHistoryConfig : IEntityTypeConfiguration<UserLoginHistory>
    {
        public void Configure(EntityTypeBuilder<UserLoginHistory> builder)
        {
            builder.ToTable("T_UserLoginHistories");
            builder.OwnsOne(x => x.PhoneNumber, nb =>
            {
                nb.Property(x => x.RegionCode).HasMaxLength(5).IsUnicode(false);
                nb.Property(x => x.Number).HasMaxLength(20).IsUnicode(false);
            });
        }
    }
}

