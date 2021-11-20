using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;

namespace Discord_Bot.Database
{
    public partial class DiscordBotEntities : DbContext
    {
        public virtual DbSet<EightBallAnswer> EightBallAnswer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "DiscordBot.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);
            optionsBuilder.UseSqlite(connection);
        }
    }
}
