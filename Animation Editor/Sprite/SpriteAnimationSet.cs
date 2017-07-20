using System.Collections.ObjectModel;

namespace Animation_Editor.Sprite
{
    class SpriteAnimationSet
    {
        public string Name { get; set; }
        public ObservableCollection<SpriteAnimation> Animations { get; set; }

        public SpriteAnimationSet(string name)
        {
            Name = name;
            Animations = new ObservableCollection<SpriteAnimation>();
            Animations.Add(new SpriteAnimation("Default Animation", 100, this));
        }
    }
}
