using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;


namespace TaaontiaCore.Database
{
    public partial class TaaontiaEntities : DbContext
    {
        public virtual DbSet<EightBallAnswer> EightBallAnswer { get; set; }
        public virtual DbSet<Character> Character { get; set; }
        public virtual DbSet<Player> Player { get; set; }
        public virtual DbSet<Fiend> Fiend { get; set; }
        public virtual DbSet<FiendType> FiendType { get; set; }
        public virtual DbSet<Fight> Fight { get; set; }
        public virtual DbSet<Event> Event { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "DiscordBot.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);
            optionsBuilder.UseSqlite(connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>()
                .HasOne(b => b.Fiend)
                .WithOne(i => i.Character)
                .HasForeignKey<Fiend>(b => b.CharacterForeignKey);
            modelBuilder.Entity<Character>()
                .HasOne(b => b.Player)
                .WithOne(i => i.Character)
                .HasForeignKey<Player>(b => b.CharacterForeignKey);
        }
    }
}
