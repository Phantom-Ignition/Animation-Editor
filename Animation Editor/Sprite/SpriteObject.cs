using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Animation_Editor.Sprite
{
    class SpriteObject : PropertyChangedBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        public BitmapSource Spritesheet { get; set; }

        private ObservableCollection<SpriteAnimation> _animationList;
        public ObservableCollection<SpriteAnimation> Animations => _animationList;

        public SpriteObject()
        {
            _name = "[New Sprite]";
            _animationList = new ObservableCollection<SpriteAnimation>();
            CreateAnimation();
        }
        
        public void CreateAnimation()
        {
            var animation = new SpriteAnimation();
            animation.Name = "New Animation";
            animation.Frames = new List<SpriteFrame>();
            animation.Frames.Add(new SpriteFrame() { Name = "Initial Frame" });
            _animationList.Add(animation);
            NotifyOfPropertyChange(() => Animations);
        }

        public void CreateFrame()
        {
            var frame = new SpriteFrame();
            frame.Name = "Test Name";
            NotifyOfPropertyChange(() => Animations);
        }

    }
}
