using Animation_Editor.Extensions;
using Animation_Editor.Modules.SpriteViewer.ViewModels;
using Animation_Editor.ProjectSprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;

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

        private Texture2D _spritesheetTexture;
        public Texture2D SpritesheetTexture
        {
            get { return _spritesheetTexture; }
            set
            {
                if (_sprite != null)
                    _sprite.TextureRegion = new TextureRegion2D(value);
                _spritesheetTexture = value;
            }
        }

        //--------------------------------------------------
        // Data

        private SpriteViewerSurfaceData _data;

        //--------------------------------------------------
        // Selection
        
        private Point _selectionInitial;
        private bool _drawSelection;

        //--------------------------------------------------
        // Dot texture
        
        private Texture2D _dotTexure;

        //--------------------------------------------------
        // Sprite

        private AnimatedSprite _sprite;

        //--------------------------------------------------
        // Game Time

        private GameTime _gameTime;

        //--------------------------------------------------
        // Current Phase

        private enum SurfacePhase
        {
            Standard,
            FrameEdit,
            ColliderEdit
        }
        private SurfacePhase _phase;

        private object _editObject;

        //----------------------//------------------------//

        public SpriteViewerSurface(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
            _spriteBatch = new SpriteBatch(_graphicsDevice);

            _input = new SpriteViewerSurfaceInput();

            _dotTexure = new Texture2D(_graphicsDevice, 1, 1);
            _dotTexure.SetData(new[] { Color.White });

            _gameTime = new GameTime();
        }

        public void SetData(SpriteViewerSurfaceData data)
        {
            _data = data;
            switch (_data.Request)
            {
                case SpriteViewerRequests.EditFrame:
                    _phase = SurfacePhase.FrameEdit;
                    _editObject = _data.EditRequest;
                    break;
                case SpriteViewerRequests.CreateSprite:
                    _sprite = new AnimatedSprite(_dotTexure);
                    _data.OnSpriteCreated(_sprite);
                    break;
            }
        }

        public void Update()
        {
            _input.Update();

            if (_phase == SurfacePhase.FrameEdit || _phase == SurfacePhase.ColliderEdit)
            {
                UpdateSelection();
            }

            _input.PostUpdate();

            
            if (_sprite != null)
            _sprite.Update(_gameTime);
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
                if (_phase == SurfacePhase.FrameEdit)
                {
                    var frame = _editObject as AnimationFrame;
                    frame.FrameRect = GetSelectionRectangle();
                    _data.OnNewFrameSelected(frame);
                }
                
                _phase = SurfacePhase.Standard;
            }
        }

        public void Draw()
        {
            _graphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            DrawGrid();

            DrawSpritesheet();
            DrawFrameRect();
            DrawColliders();
            DrawSelection();
            DrawAnimatedSprite();

            _spriteBatch.End();
        }

        private void DrawSpritesheet()
        {
            if (SpritesheetTexture != null && _data.DrawEntireSpritesheet)
            {
                var texture = SpritesheetTexture;
                var vp = _graphicsDevice.Viewport;
                var dest = new Vector2((vp.Width - texture.Width) / 2, (vp.Height - texture.Height) / 2);
                var alpha = 1.0f;
                _spriteBatch.Draw(texture, Vector2.Zero, Color.White * alpha);
            }
        }

        private void DrawFrameRect()
        {
            if (_data.CurrentFrame == null || _phase != SurfacePhase.Standard) return;
            var frame = _data.CurrentFrame;
            if (frame.FrameRect != Rectangle.Empty)
            {
                var rect = frame.FrameRect;
                _spriteBatch.Draw(_dotTexure, rect, frame.Color * 0.5f);
                _spriteBatch.DrawRectangleBorder(_dotTexure, rect, 2, frame.Color);
            }
        }

        private void DrawColliders()
        {

        }

        private void DrawSelection()
        {
            if (_drawSelection)
            {
                var rect = GetSelectionRectangle();
                _spriteBatch.Draw(_dotTexure, rect, Color.Orange * 0.5f);
                _spriteBatch.DrawRectangleBorder(_dotTexure, rect, 2, Color.Orange);
            }
        }

        private void DrawGrid()
        {
            var height = _spriteBatch.GraphicsDevice.Viewport.Height;
            var width = _spriteBatch.GraphicsDevice.Viewport.Width;
            var cols = width / _data.GridSize + 1;
            var rows = height / _data.GridSize + 1;

            for (float x = -cols; x < cols; x++)
            {
                var rect = new Rectangle((int)(x * _data.GridSize), 0, 1, height);
                _spriteBatch.Draw(_dotTexure, rect, Color.Gray * 0.8f);
            }

            for (float y = -rows; y < rows; y++)
            {
                var rect = new Rectangle(0, (int)(y * _data.GridSize), width, 1);
                _spriteBatch.Draw(_dotTexure, rect, Color.Gray * 0.8f);
            }
        }

        private void DrawAnimatedSprite()
        {
            if (_sprite == null) return;

            var rect = new Rectangle(0, 0, _graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height);
            _spriteBatch.Draw(_dotTexure, rect, Color.Black * 0.5f);
            _sprite.Draw(_spriteBatch, Vector2.Zero);
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
