using System.Collections.ObjectModel;

namespace Animation_Editor.Sprite
{
    class SpriteAnimation
    {
        public string Name { get; set; }
        public int Interval { get; set; }
        public ObservableCollection<SpriteFrame> Frames { get; set; }
    }
}
