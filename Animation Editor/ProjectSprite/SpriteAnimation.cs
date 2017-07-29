using Microsoft.Xna.Framework;
using System.Collections.ObjectModel;

namespace Animation_Editor.ProjectSprite
{
    public class SpriteAnimation
    {
        public string Name { get; set; }
        public int Interval { get; set; }
        public bool Reset { get; set; }
        public bool Loop { get; set; }
        public ObservableCollection<AnimationFrame> Frames { get; set; }
        public SpriteAnimationSet Parent { get; set; } 

        public SpriteAnimation(SpriteAnimationSet parent)
        {
            Frames = new ObservableCollection<AnimationFrame>();
            Parent = parent;
        }

        public SpriteAnimation(string name, int interval, SpriteAnimationSet parent)
        {
            Name = name;
            Interval = interval;
            Loop = interval > 0;
            Reset = Loop;

            Frames = new ObservableCollection<AnimationFrame>();
            var frame = new AnimationFrame("new name");
            frame.Colliders.Add(new AnimationRectangleCollisor("defaul1", new Rectangle(0, 0, 32, 32)));
            frame.Colliders.Add(new AnimationRectangleCollisor("dee fault two", new Rectangle(32, 64, 32, 32)));
            Frames.Add(frame);
            
            Parent = parent;

            
        }
    }
}
