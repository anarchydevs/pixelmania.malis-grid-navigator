using AOSharp.Common.GameData;
using AOSharp.Core.UI;
using System.Collections.Generic;
using static MalisGridNavigator.GridNavWindow;

namespace MalisGridNavigator
{
    public class BlankView
    {
        private View _localRoot;
        public View ExitButtonRoot;

        public BlankView(View exitButtonRoot)
        {
            _localRoot = View.CreateFromXml($"{Main.PluginDir}\\UI\\Views\\BlankView.xml");
            ExitButtonRoot = exitButtonRoot;

            ExitButtonRoot.AddChild(_localRoot, true);
            ExitButtonRoot.FitToContents();
        }

        public void Dispose()
        {
            _localRoot.Dispose();
        }
    }
}