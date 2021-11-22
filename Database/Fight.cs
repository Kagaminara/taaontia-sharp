using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Discord_Bot.Database
{
    public class Fight
    {
        [Key]
        public long Id { get; set; }
        public ICollection<Character>Allies { get; set; }
        public ICollection<Fiend> Fiends { get; set; }    
        public ICollection<Event> Events { get; set; }
        public bool IsActive { get; set; }
        public bool IsGlobal { get; set; }
    }
}
