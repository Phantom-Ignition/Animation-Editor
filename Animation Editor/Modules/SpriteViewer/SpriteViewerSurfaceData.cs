using Animation_Editor.ProjectSprite;
using Animation_Editor.Modules.SpriteViewer.ViewModels;
using System;

namespace Animation_Editor.Modules.SpriteViewer
{
    class SpriteViewerSurfaceData
    {
        public int GridSize { get; set; }
        public bool DrawEntireSpritesheet { get; set; }
        public SpriteAnimation CurrentAnimation { get; set; }
        public AnimationFrame CurrentFrame { get; set; }
        public object EditRequest { get; set; }
        public SpriteViewerRequests Request { get; set; }
        public Action<AnimationFrame> OnNewFrameSelected { get; set; }
        public Action<AnimatedSprite> OnSpriteCreated { get; set; }
    }
}
