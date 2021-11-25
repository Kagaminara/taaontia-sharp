using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaaontiaCore.Database.Models
{
    public class Skill
    {
        [Key]
        public uint Id { get; set; }
        public string Name { get; set; }
        public int BaseSourceDamage { get; set; }
        public int BaseTargetDamage { get; set; }
        /// <summary>
        /// Optional. Some skill can also trigger a Status
        /// </summary>
        public StatusType SourceStatus { get; set; }
        /// <summary>
        /// Optional. Some skill can also trigger a Status
        /// </summary>
        public StatusType TargetStatus { get; set; }
        public ICollection<Character> Characters { get; set; }
    }
}
