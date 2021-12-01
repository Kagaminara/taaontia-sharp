using System;
using System.Collections.Generic;

namespace TaaontiaLoader
{
    public class LoadedFiendTypes
    {
        public ulong Id { get; set; } = 0;
        public string Name { get; set; }
        public string Description { get; set; }
        public int BaseHealth { get; set; } = 10;
        public int BaseEnergy { get; set; } = 10;
        public ICollection<ulong> Skills { get; set; } = new List<ulong>();
    }
}
