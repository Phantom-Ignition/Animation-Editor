using Animation_Editor.ProjectSprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animation_Editor.Modules.SpriteViewer
{
    class SpriteViewerSurfaceData
    {
        public int GridSize { get; set; }
        public bool DrawEntireSpritesheet { get; set; }
        public SpriteAnimation CurrentAnimation { get; set; }
    }
}
