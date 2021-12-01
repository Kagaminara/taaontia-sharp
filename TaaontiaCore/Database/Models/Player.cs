using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaaontiaCore.Database.Models
{
    public class Player
    {
        [Key]
        public Guid Id { get; set; }
        [Index(IsUnique = true)]
        public ulong RemoteId { get; set; }
        public Guid CharacterForeignKey { get; set; }
        public Character Character { get; set; }
        public ICollection<Fight> Fights { get; set; }

    }
}
