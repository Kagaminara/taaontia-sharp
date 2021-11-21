using Discord.Commands;
using System.ComponentModel.DataAnnotations;

namespace Discord_Bot.Database
{
    [NamedArgumentType]
    public class FiendType
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int BaseHealth { get; set; }
        public int BaseEnergy { get; set; }
    }
}
