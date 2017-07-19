using Animation_Editor.Sprite;
using Gemini.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
