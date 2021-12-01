using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaaontiaLoader
{
    public class LoadedData
    {
        public ICollection<LoadedSkill> Skills { get; set; }
        public ICollection<LoadedStatus> Statuses { get; set; }
        public ICollection<LoadedFiendTypes> FiendTypes { get; set; }
    }
}
