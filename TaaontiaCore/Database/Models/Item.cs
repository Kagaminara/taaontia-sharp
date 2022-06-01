using System;
using System.ComponentModel.DataAnnotations;

namespace TaaontiaCore.Database.Models
{
    public class Item
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}