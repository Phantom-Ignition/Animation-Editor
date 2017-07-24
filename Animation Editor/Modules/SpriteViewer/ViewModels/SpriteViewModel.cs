using Animation_Editor.Sprite;
using Caliburn.Micro;
using Gemini.Framework;
using Gemini.Modules.Output;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows;
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

        private object _selectedAnimation;
        public object SelectedAnimation
        {
            get { return _selectedAnimation; }
            set
            {
                if (_selectedAnimation != value)
                {
                    _selectedAnimation = value;
                    NotifyOfPropertyChange(() => SelectedAnimation);
                    NotifyOfPropertyChange(() => Frames);
                }
            }
        }

        //--------------------------------------------------
        // Frames

        ObservableCollection<SpriteFrame> teste = new ObservableCollection<SpriteFrame>();
        public ObservableCollection<SpriteFrame> Frames
        {
            get
            {
                var anim = SelectedAnimation as SpriteAnimation;
                if (anim == null)
                {
                    return new ObservableCollection<SpriteFrame>();
                } 
                else
                {
                    return anim.Frames;
                }
            }
        }

        //--------------------------------------------------
        // Commands

        public RelayCommand NewAnimationSetCommand { get; set; }
        public RelayCommand NewAnimationCommand { get; set; }

        //----------------------//------------------------//

        public SpriteViewModel()
        {
            NewAnimationSetCommand = new RelayCommand(NewAnimationSet);
            NewAnimationCommand = new RelayCommand(NewAnimation);

            CreateEmptySprite();
            DisplayName = _sprite.Name;

            LoadTextures();
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

        public void NewAnimationSet(object obj)
        {
            _animations.Add(new SpriteAnimationSet("New Set"));
        }

        private void NewAnimation(object obj)
        {
            var anim = SelectedAnimation as SpriteAnimationSet;
            if (anim != null)
            {
                var newAnimation = new SpriteAnimation("New Animation", 100, anim);
                anim.Animations.Add(newAnimation);
            }
            else
            {
                var animationSet = (SelectedAnimation as SpriteAnimation).Parent;
                var newAnimation = new SpriteAnimation("New Animation", 100, animationSet);
                animationSet.Animations.Add(newAnimation);
            }
        }

        private void OnAnimationChanged()
        {
            //IoC.Get<IOutput>().AppendLine(_isAnimationSelected.ToString());
            IoC.Get<IOutput>().AppendLine(SelectedAnimation.ToString());
        }
    }
}
