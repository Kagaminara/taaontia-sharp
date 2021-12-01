using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaaontiaCore.Database.Models
{
    public class Character
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Energy { get; set; }
        public int MaxEnergy { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public ICollection<Skill> Skills { get; set; }
        public Player Player { get; set; }
        public Fiend Fiend { get; set; }

        public Character()
        {
            Experience = 0;
            Level = 1;
            Health = MaxHealth = 10;
            Energy = MaxEnergy = 10;
        }
        public Character(FiendType fiendType) : base()
        {
            if (fiendType != null)
            {
                Name = fiendType.Name;
                Health = MaxHealth = fiendType.BaseHealth;
                Energy = MaxEnergy = fiendType.BaseEnergy;
            }
        }
    }
}
