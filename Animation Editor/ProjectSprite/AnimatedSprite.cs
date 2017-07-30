using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;

namespace Animation_Editor.ProjectSprite
{
    //--------------------------------------------------
    // Sprite direction

    public enum SpriteDirection
    {
        Left,
        Right
    }

    //----------------------//------------------------//

    public class AnimatedSprite : Sprite
    {
        //--------------------------------------------------
        // Frames stuff

        private int _currentFrame;
        public int CurrentFrame => _currentFrame;

        private bool _looped;
        public bool Looped => _looped;

        //--------------------------------------------------
        // Animations

        private List<SpriteAnimationSet> _animations;

        public List<SpriteAnimationSet> Animations
        {
            get { return _animations; }
            set { _animations = value; }
        }
        private int _delayTick;

        private SpriteAnimation _currentAnimation;

        //----------------------//------------------------//

        public AnimatedSprite(Texture2D file) : base(file)
        {
            _animations = new List<SpriteAnimationSet>();
            _looped = false;

            Origin = Vector2.Zero;
        }

        public void CreateAnimationSet(string name)
        {
            var animSet = new SpriteAnimationSet(name);
            _animations.Add(animSet);
        }

        public void ResetCurrentFrameList()
        {
            _currentFrame = 0;
            _looped = false;
            _delayTick = 0;
        }

        public void AddFrames(string setName, string animName, List<AnimationFrame> frames)
        {
            for (var i = 0; i < frames.Count; i++)
            {
                var animations = _animations.Find(x => x.Name == setName).Animations;
                foreach (var anim in animations)
                {
                    if (anim.Name == animName)
                    {
                        anim.Frames.Add(frames[i]);
                    }
                }
            }
        }

        public void Play(SpriteAnimation animation)
        {
            _currentFrame = 0;
            _delayTick = 0;
            _currentAnimation = animation;
            _looped = false;
        }
        
        public void SetPosition(Vector2 position)
        {
            Position = new Vector2((int)position.X, (int)position.Y);
        }

        public void SetDirection(SpriteDirection direction)
        {
            if (direction == SpriteDirection.Left)
                Effect = SpriteEffects.FlipHorizontally;
            else
                Effect = SpriteEffects.None;
        }

        public void Update(GameTime gameTime)
        {
            if (_currentAnimation == null) return;

            if (_currentAnimation.Loop)
            {
                _delayTick += gameTime.ElapsedGameTime.Milliseconds;
                if (_delayTick > _currentAnimation.Interval)
                {
                    _delayTick -= _currentAnimation.Interval;
                    _currentFrame++;
                    if (_currentFrame == _currentAnimation.Frames.Count)
                    {
                        if (!_currentAnimation.Reset)
                        {
                            _currentFrame--;
                            _currentAnimation.Loop = false;
                        }
                        else _currentFrame = 0;
                        if (!_looped) _looped = true;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            if (!IsVisible) return;

            if (_currentAnimation == null)
            {
                spriteBatch.Draw(TextureRegion.Texture, position, TextureRegion.Bounds,
                                Color * Alpha, Rotation, Origin, Scale, Effect, 0);
                return;
            }

            spriteBatch.Draw(TextureRegion.Texture, position, _currentAnimation.Frames[_currentFrame].FrameRect,
                Color * Alpha, Rotation, Origin, Scale, Effect, 0);
        }
    }
}
