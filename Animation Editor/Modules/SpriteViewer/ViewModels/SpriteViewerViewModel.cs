using Animation_Editor.Sprite;
using Gemini.Framework;
using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows.Media.Imaging;

namespace Animation_Editor.Modules.SpriteViewer.ViewModels
{
    [Export(typeof(SpriteViewerViewModel))]
    class SpriteViewerViewModel : Document
    {
        private SpriteObject _sprite;
        public SpriteObject Sprite => _sprite;

        public SpriteViewerViewModel()
        {
            _sprite = new SpriteObject();
            _sprite.PropertyChanged += OnSpritePropertyChanged;
            DisplayName = _sprite.Name;
        }

        private void OnSpritePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            DisplayName = _sprite.Name;
            NotifyOfPropertyChange(e.PropertyName); // Sorry
        }
    }
}
