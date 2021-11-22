using Discord.WebSocket;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Discord_Bot.Database
{
    public partial class DiscordBotEntities : DbContext
    {
        public virtual DbSet<EightBallAnswer> EightBallAnswer { get; set; }
        public virtual DbSet<Character> Character { get; set; }
        public virtual DbSet<Fiend> Fiend { get; set; }
        public virtual DbSet<FiendType> FiendType { get; set; }
        public virtual DbSet<Fight> Fight{ get; set; }
        public virtual DbSet<Event> Event { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "DiscordBot.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);
            optionsBuilder.UseSqlite(connection);
        }

        public async Task<Character> FindOrCreateConnectedCharacter(SocketUser user)
        {
            Character connectedCharacter = await Character.SingleOrDefaultAsync(character => character.DiscordId == user.Id);
            if (connectedCharacter == null)
            {
                connectedCharacter = new Character
                {
                    Name = user.Username,
                    DiscordId = user.Id,
                    DiscordDiscriminator = user.Discriminator,
                    Level = 1,
                    Experience = 0,
                    MaxHealth = 50,
                    MaxEnergy = 50,
                    Health = 50,
                    Energy = 50,
                };
                await Character.AddAsync(connectedCharacter);
                await SaveChangesAsync();
            }

            return connectedCharacter;
        }

        public async Task<Fight> GetCurrentFight(SocketUser user, bool global = false)
        {
            var character = await FindOrCreateConnectedCharacter(user);

            return await Fight.SingleOrDefaultAsync(fight => fight.IsActive && fight.IsGlobal == global && fight.Allies.Contains(character));
        }

        public async Task AddEvent(SocketUser user, Event.EEventType type, int value)
        {
            await Event.AddAsync(new Event
            {
                
            });
        }
    }
}
