using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animation_Editor.Extensions
{
    public static class SpriteBatchExtensions
    {
        public static void DrawRectangleBorder(this SpriteBatch spriteBatch, Texture2D texture, Rectangle rect, int borderWidth)
        {
            var r = rect;
            var bw = borderWidth;
            spriteBatch.Draw(texture, new Rectangle(r.Left, r.Top, bw, r.Height), Color.Orange); // Left
            spriteBatch.Draw(texture, new Rectangle(r.Right, r.Top, bw, r.Height), Color.Orange); // Right
            spriteBatch.Draw(texture, new Rectangle(r.Left, r.Top, r.Width, bw), Color.Orange); // Top
            spriteBatch.Draw(texture, new Rectangle(r.Left, r.Bottom, r.Width, bw), Color.Orange); // Bottom
        }
    }
}
