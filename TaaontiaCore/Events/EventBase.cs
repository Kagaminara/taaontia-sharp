using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaaontiaCore.Events
{
    public abstract class EventBase
    {
        public abstract bool ActionStatus { get; }
        public abstract string ResultContent { get; }

        public EventTypeEnum EventType { get; }
        public EventBase(EventTypeEnum t)
        {
            EventType = t;
        }
    }
}
