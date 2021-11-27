using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaaontiaCore.Enums
{
    [Flags]
    public enum EBuffEffect
    {
        None = 0,
        Damage = 1,
        Heal = 2,
        PhysicalArmor = 4,
        MagicArmor = 8,
    }

}
