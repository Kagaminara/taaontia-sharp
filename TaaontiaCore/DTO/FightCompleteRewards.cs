using System.Collections.Generic;
using TaaontiaCore.Database.Models;

namespace TaaontiaCore.DTO
{
    public class FightCompleteRewards
    {
        public int Currency;
        public int Experience;
        public ICollection<Item> Items;
    }
}