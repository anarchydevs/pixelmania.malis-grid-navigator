using AOSharp.Common.GameData.UI;
using AOSharp.Core.Misc;
using AOSharp.Core.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MalisGridNavigator
{
    public class GridNavWindow : AOSharpWindow
    {
        private Views _viewCache = new Views();
        private Dictionary<GridExit,ExitInfo> _gridExitInfo = new Dictionary<GridExit,ExitInfo>(); 

        public GridNavWindow(string name, Dictionary<GridExit, ExitInfo> gridExitInfo, string path, WindowStyle windowStyle = WindowStyle.Popup, WindowFlags flags = WindowFlags.AutoScale | WindowFlags.NoFade) : base(name, path, windowStyle, flags)
        {
            _gridExitInfo = gridExitInfo;
        }

        protected override void OnWindowCreating()
        {
            if (Window.FindView("Icon", out BitmapView icon)) { icon.SetBitmap(Textures.Icon); };
           
            if (Window.FindView("Background", out BitmapView background)) { background.SetBitmap(Textures.Background); };
            
            if (Window.FindView("GridGroupViewRoot", out View gridGroupViewRoot))
            {
                List<GridGroupView> gridGroupView = new List<GridGroupView>();
                int currFloor = -1;

                foreach (var exit in _gridExitInfo)
                {
                    if ((int)exit.Value.Floor > currFloor)
                    {
                        gridGroupView.Add(new GridGroupView(gridGroupViewRoot, exit.Value.Floor.ToString()));
                        currFloor = (int)exit.Value.Floor;
                    }

                    _viewCache.GridEntryView.Add(new GridEntryView(gridGroupView.LastOrDefault().GridEntryRoot, exit.Key));
                }
            };
        }

        public void ResetGfx()
        {
            foreach (var exit in _viewCache.GridEntryView) { exit.SetGfx(Textures.RedCircle); }
        }

        public void Dispose()
        {
            foreach (var entryView in _viewCache.GridEntryView)
                entryView.Dispose();

            _viewCache.GridEntryView.Clear();

            if (Window.IsValid)
                Window.Close();
        }

        public class Views
        {
            public View GridExitRoot;
            public List<GridEntryView> GridEntryView = new List<GridEntryView>();
        }

        internal static class Textures
        {
            public const int Icon = 1430135;
            public const int RedCircle = 1430136;
            public const int GreenCircle = 1430137;
            public const int Background = 1430138;
        }
    }
}