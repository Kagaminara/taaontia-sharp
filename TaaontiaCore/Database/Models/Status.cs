using System;
using System.ComponentModel.DataAnnotations;

namespace TaaontiaCore.Database.Models
{
    public class Status
    {
        [Key]
        public Guid Id { get; set; }
        public StatusType StatusType { get; set; }
        public int RemainingDuration { get; set; }
    }
}
