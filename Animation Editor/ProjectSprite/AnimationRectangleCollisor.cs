using Microsoft.Xna.Framework;

namespace Animation_Editor.ProjectSprite
{
    class AnimationRectangleCollisor : AnimationColliderBase
    {
        public Vector2 Size { get; set; }

        public AnimationRectangleCollisor(string name, Vector2 size, Vector2 position)
        {
            Name = name;
            Size = size;
            Position = position;
        }

        public AnimationRectangleCollisor(string name, Rectangle rect)
        {
            Name = name;
            Size = rect.Size.ToVector2();
            Position = rect.Location.ToVector2();
        }
    }
}
