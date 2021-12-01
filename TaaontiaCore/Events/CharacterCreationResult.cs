using TaaontiaCore.Database.Models;
using TaaontiaCore.Enums;

namespace TaaontiaCore.Events
{
    public class CharacterResult: ResultBase
    {
        public ECharacterCreationError? Error;
        public Character Character;
    }
}
