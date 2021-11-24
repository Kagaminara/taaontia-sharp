using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaaontiaCore.Database
{
    public class Fiend
    {
        [Key]
        public long Id { get; set; }
        public long CharacterForeignKey { get; set; }
        public Character Character { get; set; }
        public FiendType FiendType { get; set; }
        public ICollection<Fight> Fights { get; set; }
    }
}
