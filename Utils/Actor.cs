using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Utils
{
    public abstract class Actor
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Energy { get; set; }
        public int MaxEnergy { get; set; }

    }
}
