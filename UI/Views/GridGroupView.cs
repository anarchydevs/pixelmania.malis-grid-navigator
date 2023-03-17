using AOSharp.Common.GameData;
using AOSharp.Core.UI;
using System.Collections.Generic;
using static MalisGridNavigator.GridNavWindow;

namespace MalisGridNavigator
{
    public class GridGroupView
    {
        public View Root;
        public View GridEntryRoot;
        private View _view;

        public GridGroupView(View rootView, string text)
        {
            Root = rootView;

            _view = View.CreateFromXml($"{Main.PluginDir}\\UI\\Views\\GridGroupView.xml");

            if (_view.FindChild("Text", out TextView textView)) { textView.Text = text; };

            if (_view.FindChild("GridEntryRoot", out GridEntryRoot)) { };

            rootView.AddChild(_view, true);
            rootView.FitToContents();
        }

        public void Dispose()
        {
            GridEntryRoot.RemoveChild(_view);
            _view.Dispose();
        }
    }
}