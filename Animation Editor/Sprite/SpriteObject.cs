using Caliburn.Micro;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows.Media.Imaging;

namespace Animation_Editor.Sprite
{
    [Export(typeof(SpriteObject))]
    class SpriteObject : PropertyChangedBase
    {
        private string _name;
        [DisplayName("Name")]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        private BitmapSource _spritesheet;
        [DisplayName("Spritesheet")]
        public BitmapSource Spritesheet
        {
            get { return _spritesheet; }
            set
            {
                _spritesheet = value;
                NotifyOfPropertyChange(() => Spritesheet);
            }
        }

        public SpriteObject()
        {
            _name = "[New Sprite]";
        }
    }
}
