using Animation_Editor.Modules.SpriteViewer.ViewModels;
using Animation_Editor.Sprite;
using Caliburn.Micro;
using Gemini.Framework;
using Gemini.Framework.Services;
using Gemini.Modules.Inspector;
using Gemini.Modules.Output;
using Gemini.Modules.Toolbox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animation_Editor.Modules.SpriteViewer
{
    [Export(typeof(IModule))]
    class Module : ModuleBase
    {
        private readonly IInspectorTool _objSettings;
        private readonly IToolbox _objectListInspector;

        [ImportingConstructor]
        public Module(IInspectorTool objSettings, IToolbox objList)
        {
            _objSettings = objSettings;
            _objSettings.DisplayName = "Object Propertiespinto";
        }

        public override void Initialize()
        {
            base.Initialize();

            Shell.ActiveDocumentChanged += (sender, e) => RefreshInspector();
            RefreshInspector();
        }

        private void RefreshInspector()
        {
            if (Shell.ActiveItem is SpriteViewModel)
            {
                var document = (SpriteViewModel)Shell.ActiveItem;
                if (document == null) return;
                _objSettings.SelectedObject = new InspectableObjectBuilder()
                    .WithObjectProperties(document.Sprite, pd => pd.ComponentType == document.Sprite.GetType())
                    .ToInspectableObject();

                
                Shell.ShowTool(_objSettings);
                Shell.ShowTool(IoC.Get<IOutput>());
            }
            else
            {
                _objSettings.SelectedObject = null;
            }
        }

        public override void PostInitialize()
        {
            base.PostInitialize();

            var testSprite = new SpriteViewModel();
            IShell shell = IoC.Get<IShell>();
            shell.OpenDocument(testSprite);
        }
    }
}
