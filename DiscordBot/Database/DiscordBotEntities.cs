using Discord.WebSocket;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Discord_Bot.Database
{
    public partial class DiscordBotEntities : DbContext
    {
        public virtual DbSet<EightBallAnswer> EightBallAnswer { get; set; }
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

        public async Task<Player> FindOrCreateConnectedCharacter(SocketUser user)
        {
            Player connectedCharacter = await Player.SingleOrDefaultAsync(player => player.DiscordId == user.Id);
            if (connectedCharacter == null)
            {
                connectedCharacter = new Player
                {
                    Name = user.Username,
                    Experience = 0,
                    Level = 1,
                    MaxHealth = 50,
                    MaxEnergy = 50,
                    Health = 50,
                    Energy = 50,
                    DiscordId = user.Id,
                    DiscordDiscriminator = user.Discriminator,
                };
                await Player.AddAsync(connectedCharacter);
                await SaveChangesAsync();
            }

            return connectedCharacter;
        }

        public async Task<Fight> GetCurrentFight(SocketUser user, bool global = false)
        {
            var character = await FindOrCreateConnectedCharacter(user);

            return await Fight.SingleOrDefaultAsync(fight => fight.IsActive && fight.IsGlobal == global && fight.Player == character);
        }
    }
}
