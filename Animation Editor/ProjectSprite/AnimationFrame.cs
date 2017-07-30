using Microsoft.Xna.Framework;
using System.Collections.ObjectModel;

namespace Animation_Editor.ProjectSprite
{
    public class AnimationFrame
    {
        public string Name { get; set; }
        public Rectangle FrameRect { get; set; }
        public Color Color { get; set; }
        public ObservableCollection<AnimationColliderBase> Colliders { get; set; }

        public AnimationFrame()
        {
            Name = "";
            FrameRect = Rectangle.Empty;
            Color = Color.IndianRed;
            Colliders = new ObservableCollection<AnimationColliderBase>();
        }
    }
}
