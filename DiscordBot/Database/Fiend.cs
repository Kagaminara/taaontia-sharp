using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Discord_Bot.Database
{
    public class Fiend
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Energy { get; set; }
        public int MaxEnergy { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public FiendType FiendType { get; set; }
        public ICollection<Fight> Fights { get; set; }
    }
}
