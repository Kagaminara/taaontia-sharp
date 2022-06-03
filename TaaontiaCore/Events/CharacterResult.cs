using TaaontiaCore.Database.Models;
using TaaontiaCore.Enums;
using TaaontiaCore.Interfaces;

namespace TaaontiaCore.Events
{
    public class CharacterResult: ResultBase
    {
        public ECharacterError? Error;
        public Player Character;
    }
}
