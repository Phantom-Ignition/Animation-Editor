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
using System.Windows.Media.Imaging;

namespace Animation_Editor.Modules.SpriteViewer.Views
{
    /// <summary>
    /// Interaction logic for SpriteView.xaml
    /// </summary>
    public partial class SpriteView : UserControl, IDisposable
    {
        private readonly IOutput _output;

        private System.Windows.Media.ImageSource _lastSpritesheetImage;

        private SpriteBatch _spriteBatch;

        private bool _mouseDown;
        private Point _mousePoint;
        private Point _previousMousePosition;

        private SpriteViewerSurface _surface;

        public SpriteView()
        {
            InitializeComponent();
            _output = IoC.Get<IOutput>();
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
        }

        private void OnGraphicsControlDraw(object sender, DrawEventArgs e)
        {
            _surface.Update();
            _surface.Draw();

            if (Spritesheet.Source != null && _lastSpritesheetImage != Spritesheet.Source)
            {
                _lastSpritesheetImage = Spritesheet.Source;
                var stream = (FileStream)((BitmapImage)Spritesheet.Source).StreamSource;
                using (FileStream fileStream = new FileStream(stream.Name, FileMode.Open))
                    _surface.SpritesheetTexture = Texture2D.FromStream(e.GraphicsDevice, fileStream);
            }
        }

        private void OnGraphicsControlMouseMove(object sender, MouseEventArgs e)
        {
            var position = e.GetPosition(this);
            _previousMousePosition = new Point((int)position.X, (int)position.Y);
            _surface.Input.CurrentMousePosition = _previousMousePosition;
        }

        private void OnGraphicsControlHwndLButtonDown(object sender, MouseEventArgs e)
        {
            var position = e.GetPosition(this);
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
