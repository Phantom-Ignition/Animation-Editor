using Animation_Editor.ProjectSprite;
using Caliburn.Micro;
using Gemini.Framework;
using Gemini.Modules.Output;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows;

namespace Animation_Editor.Modules.SpriteViewer.ViewModels
{
    public enum SpriteViewerRequests
    {
        Standard,
        CreateSprite,
        EditFrame,
        EditCollider
    }

    [Export(typeof(SpriteViewModel))]
    class SpriteViewModel : Document
    {
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
        // Sprite
        
        public AnimatedSprite Sprite { get; set; }

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
        
        public ObservableCollection<AnimationFrame> Frames
        {
            get
            {
                var anim = SelectedAnimation as SpriteAnimation;
                if (anim == null)
                {
                    return new ObservableCollection<AnimationFrame>();
                } 
                else
                {
                    return anim.Frames;
                }
            }
        }

        private object _selectedFrame;
        public object SelectedFrame
        {
            get { return _selectedFrame; }
            set
            {
                if (_selectedFrame != value)
                {
                    _selectedFrame = value;
                    NotifyOfPropertyChange(() => SelectedFrame);
                }
            }
        }

        //--------------------------------------------------
        // Grid Size

        private int _gridSize = 32;
        public int GridSize
        {
            get { return _gridSize; }
            set
            {
                _gridSize = value;
                NotifyOfPropertyChange(() => GridSize);
            }
        }

        //--------------------------------------------------
        // Frames delay

        private int _framesDelay = 120;
        public int FramesDelay
        {
            get { return _framesDelay; }
            set
            {
                _framesDelay = value;
                NotifyOfPropertyChange(() => FramesDelay);
            }
        }

        //--------------------------------------------------
        // Commands

        public RelayCommand NewAnimationSetCommand { get; set; }
        public RelayCommand NewAnimationCommand { get; set; }
        public RelayCommand NewFrameCommand { get; set; }
        public RelayCommand NewColliderCommand { get; set; }
        public RelayCommand PlayAnimationCommand { get; set; }
        public RelayCommand StopAnimationCommand { get; set; }

        //--------------------------------------------------
        // Request

        public object EditRequest { get; set; }
        public SpriteViewerRequests Request { get; set; }

        //----------------------//------------------------//

        public SpriteViewModel()
        {
            NewAnimationSetCommand = new RelayCommand(NewAnimationSet);
            NewAnimationCommand = new RelayCommand(NewAnimation);
            NewFrameCommand = new RelayCommand(NewFrame);
            NewColliderCommand = new RelayCommand(NewCollider);
            PlayAnimationCommand = new RelayCommand(PlayAnimation);
            StopAnimationCommand = new RelayCommand(StopAnimation);

            CreateSprite();
            DisplayName = "[New Sprite]";

            _animations = new ObservableCollection<SpriteAnimationSet>();
            _animations.Add(new SpriteAnimationSet("Default Folder"));

            LoadTextures();
        }

        private void CreateSprite()
        {
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

        private void NewAnimationSet(object obj)
        {
            _animations.Add(new SpriteAnimationSet("New Set"));
        }

        private void NewAnimation(object obj)
        {
            if (SelectedAnimation == null)
            {
                MessageBox.Show("No animation set selected!");
            }
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

        private void NewFrame(object obj)
        {
            var anim = SelectedAnimation as SpriteAnimation;
            if (anim == null)
            {
                MessageBox.Show("No animation set selected!");
                return;
            }
            var newFrame = new AnimationFrame();
            newFrame.Name = anim.Frames.Count.ToString();
            EditRequest = newFrame;
            Request = SpriteViewerRequests.EditFrame;
        }

        private void NewCollider(object obj) { }

        public void OnNewFrameSelected(AnimationFrame newFrame)
        {
            var anim = SelectedAnimation as SpriteAnimation;
            if (anim == null) return;

            newFrame.Name = anim.Frames.Count.ToString();

            anim.Frames.Add(newFrame);
        }

        public void OnSpriteCreated(AnimatedSprite sprite)
        {
            Sprite = sprite;
            var list = new List<SpriteAnimationSet>(_animations);
            Sprite.Animations = list;
        }

        private void PlayAnimation(object obj)
        {
            var list = new List<SpriteAnimationSet>(_animations);
            Sprite.Animations = list;

            var anim = SelectedAnimation as SpriteAnimation;
            if (anim == null)
            {
                MessageBox.Show("Invalid animation!");
                return;
            }
            Sprite.Play(anim);
        }

        private void StopAnimation(object obj)
        {

        }

        private void OnAnimationChanged()
        {
            //IoC.Get<IOutput>().AppendLine(_isAnimationSelected.ToString());
            IoC.Get<IOutput>().AppendLine(SelectedAnimation.ToString());
        }

        public void ResetRequest()
        {
            EditRequest = null;
            Request = SpriteViewerRequests.Standard;
        }
    }
}
