using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaaontiaCore.Enums;

namespace TaaontiaCore.Events
{
    public class NewFightEvent : EventBase
    {

    }

    public class FightEvent : EventBase
    {
        public ulong SkillId;
        public EFightEventTarget Target;
    }
}
