using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animation_Editor.Modules.SpriteViewer
{
    class SpriteViewerSurfaceInput
    {
        public ButtonState CurrentMouseState { get; set; }
        public ButtonState PreviousMouseState { get; set; }

        public Point CurrentMousePosition { get; set; }
        public Point PreviousMousePosition { get; set; }

        public void Update()
        {
            if (CurrentMouseState == ButtonState.Pressed)
            {
                var a = 2;
            }
        }

        public void PostUpdate()
        {
            PreviousMouseState = CurrentMouseState;
            PreviousMousePosition = CurrentMousePosition;
        }

        public bool MousePressed()
        {
            return CurrentMouseState == ButtonState.Pressed &&
                PreviousMouseState == ButtonState.Released;
        }

        public bool MouseDown()
        {
            return CurrentMouseState == ButtonState.Pressed;
        }

        public bool MouseReleased()
        {
            return PreviousMouseState == ButtonState.Pressed &&
                CurrentMouseState == ButtonState.Released;
        }
    }
}
