using Caliburn.Micro;
using Gemini.Framework;
using System.ComponentModel.Composition;

namespace Animation_Editor.Modules.Startup
{
    [Export(typeof(IModule))]
    class Module : ModuleBase
    {
        public override void Initialize()
        {
            MainWindow.Title = "Animation Editor";
            MainWindow.WindowState = System.Windows.WindowState.Maximized;
            Shell.ToolBars.Visible = true;
        }
    }
}
