using Microsoft.Xna.Framework;
using System.Collections.ObjectModel;

namespace Animation_Editor.Sprite
{
    class SpriteFrame
    {
        public string Name { get; set; } = "tetste";
        public Rectangle FrameRect { get; set; } = Rectangle.Empty;
        public Color Color { get; set; }
        public ObservableCollection<SpriteCollisorBase> Collisors { get; set; } = new ObservableCollection<SpriteCollisorBase>();
    }
}
