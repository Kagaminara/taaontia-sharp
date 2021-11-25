using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaaontiaCore.Database.Models
{
    public class Fight
    {
        [Key]
        public Guid Id { get; set; }
        public ICollection<Character> Allies { get; set; }
        public ICollection<Character> Fiends { get; set; }
        public ICollection<Event> Events { get; set; }
        public bool IsActive { get; set; }
        public bool IsGlobal { get; set; }
    }
}
