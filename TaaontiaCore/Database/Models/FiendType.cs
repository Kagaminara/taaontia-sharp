using System;
using System.ComponentModel.DataAnnotations;

namespace TaaontiaCore.Database.Models
{
    public class FiendType
    {
        [Key]
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int BaseHealth { get; set; }
        public int BaseEnergy { get; set; }
    }
}
