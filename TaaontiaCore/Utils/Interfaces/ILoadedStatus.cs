using TaaontiaCore.Enums;

namespace TaaontiaCore.Utils
{
    public interface ILoadedStatus
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public EBuffEffect Effect { get; set; }
        public int BaseValue { get; set; }
        public int Duration { get; set; }

    }
}
