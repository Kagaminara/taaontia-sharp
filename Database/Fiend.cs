using Discord_Bot.Utils;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Discord_Bot.Database
{
    public class Fiend: Actor
    {
        public int Level { get; set; }
        public FiendType FiendType { get; set; }
        public ICollection<Fight> Fights { get; set; }
    }
}
