using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaaontiaCore.Events.Fight
{
    public enum EFightResult
    {
        SUCCESS = 0,
        FAILURE,
        ERROR,
    }
    public class FightResult
    {
        public EFightResult Result;
        public int DamageValue;
    }
}
