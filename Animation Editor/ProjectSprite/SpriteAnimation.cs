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
            Parent = parent;
        }
    }
}
