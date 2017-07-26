using Microsoft.Xna.Framework;

namespace Animation_Editor.Sprite
{
    class SpriteRectangleCollisor : SpriteCollisorBase
    {
        public Vector2 Size { get; set; }

        public SpriteRectangleCollisor(string name, Vector2 size, Vector2 position)
        {
            Name = name;
            Size = size;
            Position = position;
        }

        public SpriteRectangleCollisor(string name, Rectangle rect)
        {
            Name = name;
            Size = rect.Size.ToVector2();
            Position = rect.Location.ToVector2();
        }
    }
}
