
using TaaontiaCore.Enums;
using TaaontiaCore.Interfaces;

namespace TaaontiaCore.Events
{
    public class CharacterEvent : EventBase
    {
        public ICharacter Character;
        public ECharacterEventType EventType;
    }
}
