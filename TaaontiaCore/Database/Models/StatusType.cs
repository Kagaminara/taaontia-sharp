using System.ComponentModel.DataAnnotations;
using TaaontiaCore.Enums;

namespace TaaontiaCore.Database.Models
{
    /// <summary>
    /// Describe a Status, in a form of a buff,
    /// or a debuff.
    /// </summary>
    public class StatusType
    {
        /// <summary>
        /// Id is a <c>uint</c> for ease of use when creating new skills
        /// </summary>
        [Key]
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public EBuffEffect? Effect { get; set; }
        public int? BaseValue { get; set; }
        /// <summary>
        /// Duration (in turns)
        /// </summary>
        public int? Duration { get; set; } = 1;
    }
}
