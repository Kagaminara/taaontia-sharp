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
        public int Mana { get; set; }
        public int Level { get; set; }
        public ICollection<Fight> Fights { get; set; }
    }
}
