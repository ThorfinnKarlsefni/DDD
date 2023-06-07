using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MicroServer.Infrastructure
{
    public class DbContextFactory : IDesignTimeDbContextFactory<UserDbContext>
    {
        public UserDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<UserDbContext>();
            builder.UseSqlServer("Server=49.235.117.63;Database=DDD;TrustServerCertificate=True;User ID=sa;Password=zzm11.21");
            return new UserDbContext(builder.Options);
        }
    }
}

