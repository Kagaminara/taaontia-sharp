using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaaontiaCore.Database.Models;
using TaaontiaCore.Events;
using TaaontiaCore.Enums;
using System;

namespace TaaontiaCore.Database
{
    public partial class TaaontiaEntities : DbContext
    {
        public virtual DbSet<Player> Player { get; set; }
        public virtual DbSet<Fiend> Fiend { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<FiendType> FiendType { get; set; }
        public virtual DbSet<Fight> Fight { get; set; }
        public virtual DbSet<Skill> Skill { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<StatusType> StatusType { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "..\\TaaontiaCore\\Taaontia.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);
            optionsBuilder.UseSqlite(connection);
        }
    }
}
