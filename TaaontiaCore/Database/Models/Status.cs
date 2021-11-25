using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaaontiaCore.Database.Models
{
    public class Status
    {
        [Key]
        public Guid Id { get; set; }
        public StatusType StatusType { get; set; }
        /// <summary>
        /// Reference to the Character that casted the `Skill` inducing this status
        /// </summary>
        public Character Source { get; set; }
        public Character Target { get; set; }
        public int RemainingDuration { get; set; }
    }
}
