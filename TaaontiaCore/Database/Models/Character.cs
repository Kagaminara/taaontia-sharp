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
    }
}
