using System.Collections.Generic;
using TaaontiaCore.Database.Models;

namespace TaaontiaCore.Interfaces
{
    public interface ICharacter
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Energy { get; set; }
        public int MaxEnergy { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public ICollection<Skill> Skills { get; set; }
        public ICollection<Fight> Fights { get; set; }
    }
}
