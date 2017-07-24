using System.Collections.ObjectModel;

namespace Animation_Editor.Sprite
{
    class SpriteAnimation
    {
        public string Name { get; set; }
        public int Interval { get; set; }
        public ObservableCollection<SpriteFrame> Frames { get; set; }
        public SpriteAnimationSet Parent { get; set; }

        public SpriteAnimation(SpriteAnimationSet parent)
        {
            Frames = new ObservableCollection<Sprite.SpriteFrame>();
            Parent = parent;
        }

        public SpriteAnimation(string name, int interval, SpriteAnimationSet parent)
        {
            Name = name;
            Interval = interval;
            Frames = new ObservableCollection<SpriteFrame>();
            Frames.Add(new SpriteFrame());
            Parent = parent;
        }
    }
}
