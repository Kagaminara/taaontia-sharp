using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaaontiaCore.Database.Models
{
    public class Fight
    {
        [Key]
        public Guid Id { get; set; }
        public Player Player { get; set; }
        public Fiend Fiend { get; set; }
        public bool IsActive { get; set; }
        public bool IsGlobal { get; set; }
    }
}
