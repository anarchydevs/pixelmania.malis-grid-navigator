using AOSharp.Common.GameData;
using AOSharp.Core.UI;
using System;
using System.Collections.Generic;
using static MalisGridNavigator.GridNavWindow;

namespace MalisGridNavigator
{
    public class FloorView
    {
        public View FloorRootView;
        public TextView TextView;
        public Button Button;
        public BitmapView Background;
        private View _localRoot;
        public List<GridEntryView> GridEntryView = new List<GridEntryView>();

        public FloorView(View rootView, string text)
        {
            FloorRootView = rootView;

            _localRoot = View.CreateFromXml($"{Main.PluginDir}\\UI\\Views\\FloorView.xml");

            if (_localRoot.FindChild("Text", out TextView)) { TextView.Text = text; };
           
            if (_localRoot.FindChild("Button", out Button)) { Button.Clicked = ButtonClick; };
           
            if (_localRoot.FindChild("Background", out Background)) { Background.SetBitmap(Textures.FloorButton); };

            rootView.FitToContents();
            rootView.AddChild(_localRoot, true);
        }

        private void ButtonClick(object sender, ButtonBase e)
        {
            Main.Window.ResetFloorButtonColors();
            Background.SetColor(0xAAFFAA);
            Main.Window.UpdateViewSelector(GridEntryView);
            Midi.Play("Click");
        }

        public void ResetBackgroundColor() => Background.SetColor(0xFFFFFF);

        public void Dispose()
        {
            foreach (var gridEntryView in GridEntryView)
                gridEntryView.Dispose();

            FloorRootView.Dispose();
        }
    }
}