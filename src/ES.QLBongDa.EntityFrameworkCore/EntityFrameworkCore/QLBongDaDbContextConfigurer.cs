using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace ES.QLBongDa.EntityFrameworkCore
{
    public static class QLBongDaDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<QLBongDaDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<QLBongDaDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}