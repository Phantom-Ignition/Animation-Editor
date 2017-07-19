using Animation_Editor.Extensions;
using Animation_Editor.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animation_Editor.Modules.SpriteViewer
{
    class SpriteViewerSurface
    {
        private GraphicsDevice _graphicsDevice;
        private SpriteBatch _spriteBatch;

        //--------------------------------------------------
        // Input

        private SpriteViewerSurfaceInput _input;
        public SpriteViewerSurfaceInput Input => _input;

        //--------------------------------------------------
        // Spritesheet

        public Texture2D SpritesheetTexture { get; set; }

        //--------------------------------------------------
        // Selection

        private Texture2D _selectionTexture;
        private Point _selectionInitial;
        private bool _drawSelection;

        //--------------------------------------------------
        // Current Frame

        private SpriteFrame _currentFrame;
        private Texture2D _currentFrameTexure;

        //----------------------//------------------------//

        public SpriteViewerSurface(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
            _spriteBatch = new SpriteBatch(_graphicsDevice);
            _input = new SpriteViewerSurfaceInput();

            _selectionTexture = new Texture2D(_graphicsDevice, 1, 1);
            _selectionTexture.SetData(new[] { Color.Orange });

            _currentFrame = new SpriteFrame();
            _currentFrameTexure = new Texture2D(_graphicsDevice, 1, 1);
        }

        public void Update()
        {
            _input.Update();
            
            UpdateSelection();

            _input.PostUpdate();
        }
        
        private void UpdateSelection()
        {
            _drawSelection = _input.MouseDown();
            if (_input.MousePressed())
            {
                _selectionInitial = _input.CurrentMousePosition;
            }
            if (_input.MouseReleased())
            {
                _currentFrame.FrameRect = GetSelectionRectangle();
                _currentFrame.Color = Color.IndianRed;
            }
        }

        public void Draw()
        {
            _graphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            DrawSpritesheet();
            DrawFrameRect();
            DrawSelection();

            _spriteBatch.End();
        }

        private void DrawSpritesheet()
        {
            if (SpritesheetTexture != null)
            {
                var texture = SpritesheetTexture;
                var vp = _graphicsDevice.Viewport;
                var dest = new Vector2((vp.Width - texture.Width) / 2, (vp.Height - texture.Height) / 2);
                var alpha = _currentFrame.FrameRect == Rectangle.Empty ? 0.5f : 1.0f;
                _spriteBatch.Draw(texture, dest, Color.White * alpha);
            }
        }

        private void DrawFrameRect()
        {
            if (_currentFrame.FrameRect != Rectangle.Empty)
            {
                var rect = _currentFrame.FrameRect;
                _spriteBatch.Draw(_currentFrameTexure, rect, Color.White * 0.5f);
                _spriteBatch.DrawRectangleBorder(_currentFrameTexure, rect, 2);
            }
        }

        private void DrawSelection()
        {
            if (_drawSelection)
            {
                var rect = GetSelectionRectangle();
                _spriteBatch.Draw(_selectionTexture, rect, Color.White * 0.5f);
                _spriteBatch.DrawRectangleBorder(_selectionTexture, rect, 2);
            }
        }

        private Rectangle GetSelectionRectangle()
        {
            var pPosition = _selectionInitial;
            var cPosition = _input.CurrentMousePosition;
            var newSize = new Point(cPosition.X - pPosition.X, cPosition.Y - pPosition.Y);
            return new Rectangle(pPosition, newSize);
        }
    }
}
