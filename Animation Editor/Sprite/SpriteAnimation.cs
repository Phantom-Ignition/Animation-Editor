using Microsoft.Xna.Framework;
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
            Frames = new ObservableCollection<SpriteFrame>();
            Parent = parent;
        }

        public SpriteAnimation(string name, int interval, SpriteAnimationSet parent)
        {
            Name = name;
            Interval = interval;
            Frames = new ObservableCollection<SpriteFrame>();
            var frame = new SpriteFrame();
            frame.Collisors.Add(new SpriteRectangleCollisor("defaul1", new Rectangle(0, 0, 32, 32)));
            frame.Collisors.Add(new SpriteRectangleCollisor("dee fault two", new Rectangle(32, 64, 32, 32)));
            Frames.Add(frame);
            
            Parent = parent;
        }
    }
}
