using Animation_Editor.Sprite;
using Gemini.Framework;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows.Controls;

namespace Animation_Editor.Modules.SpriteViewer.ViewModels
{
    [Export(typeof(SpriteViewModel))]
    class SpriteViewModel : Document
    {
        //--------------------------------------------------
        // Sprite

        private SpriteObject _sprite;
        public SpriteObject Sprite => _sprite;

        //--------------------------------------------------
        // Textures & Current Texture

        private ObservableCollection<SpriteTexture> _textures;
        public ObservableCollection<SpriteTexture> Textures
        {
            get { return _textures; }
            set
            {
                _textures = value;
                NotifyOfPropertyChange(() => Textures);
            }
        }

        private SpriteTexture _texture;
        public SpriteTexture Texture
        {
            get { return _texture; }
            set
            {
                _texture = value;
                NotifyOfPropertyChange(() => Texture);
            }
        }

        //--------------------------------------------------
        // Animations

        private ObservableCollection<SpriteAnimationSet> _animations;
        public ObservableCollection<SpriteAnimationSet> Animations
        {
            get { return _animations; }
            set
            {
                _animations = value;
                NotifyOfPropertyChange(() => Animations);
            }
        }

        private ObservableCollection<TreeViewItem> _animationItems;
        public ObservableCollection<TreeViewItem> AnimationItems
        {
            get { return _animationItems; }
            set
            {
                _animationItems = value;
                NotifyOfPropertyChange(() => AnimationItems);
            }
        }

        //----------------------//------------------------//

        public SpriteViewModel()
        {
            CreateEmptySprite();
            DisplayName = _sprite.Name;

            LoadTextures();
            LoadAnimations();
        }

        private void CreateEmptySprite()
        {
            _sprite = new SpriteObject();
            _animations = new ObservableCollection<SpriteAnimationSet>();
            _animations.Add(new SpriteAnimationSet("Default Folder"));
        }

        private void LoadTextures()
        {
            _textures = new ObservableCollection<SpriteTexture>();
            string[] textureFiles = Directory.GetFiles(Environment.CurrentDirectory + "/textures", "*.png");
            foreach (var file in textureFiles)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                _textures.Add(new SpriteTexture() { DisplayName = fileName, Path = file.ToString() });
            }
        }

        private void LoadAnimations()
        {
            _animationItems = new ObservableCollection<TreeViewItem>();
            foreach (var animationSet in _animations)
            {
                var treeItem = new TreeViewItem();
                treeItem.Header = animationSet.Name;
                treeItem.IsExpanded = true;

                foreach (var animation in animationSet.Animations)
                {
                    treeItem.Items.Add(new TreeViewItem() { Header = animation.Name });
                }

                _animationItems.Add(treeItem);
            }
        }
    }
}
