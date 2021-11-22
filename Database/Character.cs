using Discord_Bot.Utils;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Discord_Bot.Database
{
    public class Character: Actor
    {
        public ulong DiscordId { get; set; }
        public string DiscordDiscriminator { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public ICollection<Fight> Fights { get; set; }

    }
}
