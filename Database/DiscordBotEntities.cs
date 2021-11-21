using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Discord_Bot.Database
{
    public partial class DiscordBotEntities : DbContext
    {
        public virtual DbSet<EightBallAnswer> EightBallAnswer { get; set; }
        public virtual DbSet<Character> Character { get; set; }
        public virtual DbSet<Fiend> Fiend { get; set; }
        public virtual DbSet<Fight> Fight{ get; set; }
        public virtual DbSet<FightEvent> FightEvent { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "DiscordBot.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);
            optionsBuilder.UseSqlite(connection);
        }
    }
}
