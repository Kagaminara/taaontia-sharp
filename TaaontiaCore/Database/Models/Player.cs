using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaaontiaCore.Interfaces;

namespace TaaontiaCore.Database.Models
{
    public class Player: ICharacter
    {
        // Player related fields
        [Key]
        public Guid Id { get; set; }
        [Index(IsUnique = true)]
        public ulong RemoteId { get; set; }
        
        // Character related fields
        public string Name { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Energy { get; set; }
        public int MaxEnergy { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public ICollection<Skill> Skills { get; set; }
        public ICollection<Fight> Fights { get; set; }
        public ICollection<Event> Events { get; set; }
        public bool IsHardcore { get; set; }

        public Player()
        {
            Experience = 0;
            Level = 1;
            Health = MaxHealth = 10;
            Energy = MaxEnergy = 10;
        }

    }
}
