using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animation_Editor.Sprite
{
    class SpriteFrame
    {
        public string Name { get; set; }
        public Rectangle FrameRect { get; set; } = Rectangle.Empty;
        public Color Color { get; set; }
        public bool Selected { get; set; }
    }
}
