using Animation_Editor.Modules.SpriteViewer.ViewModels;
using Animation_Editor.ProjectSprite;
using Caliburn.Micro;
using Gemini.Modules.MonoGame.Controls;
using Gemini.Modules.Output;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;

namespace Animation_Editor.Modules.SpriteViewer.Views
{
    /// <summary>
    /// Interaction logic for SpriteView.xaml
    /// </summary>
    public partial class SpriteView : UserControl, IDisposable
    {
        private readonly IOutput _output;
        
        private Point _previousMousePosition;

        private SpriteBatch _spriteBatch;
        private SpriteViewerSurface _surface;

        public SpriteView()
        {
            InitializeComponent();
            _output = IoC.Get<IOutput>();

            Texture.SelectionChanged += OnTextureChanged;
        }

        private void OnTextureChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Texture.SelectedIndex >= 0)
            {
                var item = (SpriteTexture)Texture.SelectedItem;
                using (FileStream fileStream = new FileStream(item.Path, FileMode.Open))
                    _surface.SpritesheetTexture = Texture2D.FromStream(_spriteBatch.GraphicsDevice, fileStream);
            }
        }

        public void Invalidate()
        {
            GraphicsControl.Invalidate();
        }

        public void Dispose()
        {
            GraphicsControl.Dispose();
        }

        private void OnGraphicsControlLoadContent(object sender, GraphicsDeviceEventArgs e)
        {
            _surface = new SpriteViewerSurface(e.GraphicsDevice);
            _spriteBatch = new SpriteBatch(e.GraphicsDevice);

            Texture.SelectedIndex = 5;
        }

        private void OnGraphicsControlDraw(object sender, DrawEventArgs e)
        {
            var model = (SpriteViewModel)DataContext;
            var selectedAnim = model.SelectedAnimation;
            SpriteViewerSurfaceData data = new SpriteViewerSurfaceData()
            {
                GridSize = model.GridSize,
                DrawEntireSpritesheet = selectedAnim is SpriteAnimationSet || selectedAnim is SpriteAnimation,
                CurrentAnimation = selectedAnim as SpriteAnimation,
                CurrentFrame = model.SelectedFrame as AnimationFrame,
                OnNewFrameSelected = model.OnNewFrameSelected,
                EditRequest = model.EditRequest,
                Request = model.Request
            };
            _surface.SetData(data);
            _surface.Update();
            _surface.Draw();
            model.ResetRequest();
        }

        private void OnGraphicsControlMouseMove(object sender, MouseEventArgs e)
        {
            var position = e.GetPosition(GraphicsControl);
            _previousMousePosition = new Point((int)position.X, (int)position.Y);
            _surface.Input.CurrentMousePosition = _previousMousePosition;
        }

        private void OnGraphicsControlHwndLButtonDown(object sender, MouseEventArgs e)
        {
            var position = e.GetPosition(GraphicsControl);
            _previousMousePosition = new Point((int)position.X, (int)position.Y);
            _surface.Input.CurrentMousePosition = _previousMousePosition;

            _surface.Input.CurrentMouseState = ButtonState.Pressed;

            GraphicsControl.CaptureMouse();
            GraphicsControl.Focus();
        }

        private void OnGraphicsControlHwndLButtonUp(object sender, MouseEventArgs e)
        {
            _surface.Input.CurrentMouseState = ButtonState.Released;
            
            _output.AppendLine("Mouse left button up");
            GraphicsControl.ReleaseMouseCapture();
        }

        private void OnGraphicsControlKeyDown(object sender, KeyEventArgs e)
        {
            _output.AppendLine("Key down: " + e.Key);
        }

        private void OnGraphicsControlKeyUp(object sender, KeyEventArgs e)
        {
            _output.AppendLine("Key up: " + e.Key);
        }

        private void OnGraphicsControlHwndMouseWheel(object sender, MouseWheelEventArgs e)
        {
            _output.AppendLine("Mouse wheel: " + e.Delta);
        }
    }
}
