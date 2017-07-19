using Animation_Editor.Sprite;
using Gemini.Framework;
using System.ComponentModel;
using System.ComponentModel.Composition;

namespace Animation_Editor.Modules.SpriteViewer.ViewModels
{
    [Export(typeof(SpriteViewModel))]
    class SpriteViewModel : Document
    {
        private SpriteObject _sprite;
        public SpriteObject Sprite => _sprite;

        private string _name;
        [DisplayName("Name")]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                DisplayName = value;
                NotifyOfPropertyChange(() => Name);
            }
        }


        public SpriteViewModel()
        {
            _sprite = new SpriteObject();
            DisplayName = _sprite.Name;
        }
    }
}
