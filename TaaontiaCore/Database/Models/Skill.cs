using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaaontiaCore.Database.Models
{
    public class Skill
    {
        [Key]
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        // TODO : Add "BaseDeviation" to provide some reduceable randomness
        public Nullable<int> BaseSourceDamage { get; set; }
        public Nullable<int> BaseTargetDamage { get; set; }
        /// <summary>
        /// Optional. Some skill can also trigger a Status
        /// </summary>
        public StatusType SourceStatus { get; set; }
        /// <summary>
        /// Optional. Some skill can also trigger a Status
        /// </summary>
        public StatusType TargetStatus { get; set; }
        public ICollection<Player> Players { get; set; }
        public ICollection<Fiend> Fiends { get; set; }
        public ICollection<FiendType> FiendTypes { get; set; }
    }
}
