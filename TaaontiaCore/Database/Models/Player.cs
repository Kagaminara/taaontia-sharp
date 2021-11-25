using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaaontiaCore.Database.Models
{
    public class Player
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CharacterForeignKey { get; set; }
        public Character Character { get; set; }
        public ICollection<Fight> Fights { get; set; }

    }
}
