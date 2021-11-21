using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Discord_Bot.Database
{
    public class Character
    {
        [Key]
        public long Id { get; set; }
        public string Username { get; set; }
        public int Health { get; set; }
        public int Mana { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public ICollection<Fight> Fights { get; set; }

    }
}
