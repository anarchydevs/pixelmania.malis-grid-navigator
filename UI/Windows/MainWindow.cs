using AOSharp.Common.GameData.UI;
using AOSharp.Core;
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
        private Dictionary<GridExit, Dictionary<GridSide, GridExitInfo>> _gridInfo;
       
        public GridNavWindow(string name, Dictionary<GridExit, Dictionary<GridSide, GridExitInfo>> gridInfo, string path, WindowStyle windowStyle = WindowStyle.Popup, WindowFlags flags = WindowFlags.AutoScale | WindowFlags.NoFade) : base(name, path, windowStyle, flags)
        {
            _gridInfo = gridInfo;
        }

        protected override void OnWindowCreating()
        {
            if (Window.FindView("FloorRoot", out _viewCache.FloorRoot)) { };

            if (Window.FindView("ExitRoot", out _viewCache.ExitRoot)) { };

            if (Window.FindView("LeftTitle", out _viewCache.LeftTitle)) { };

            if (Window.FindView("RightTitle", out _viewCache.RightTitle)) { };

            if (Window.FindView("Background", out BitmapView background)) { background.SetBitmap(Textures.Background); };
         
            if (Window.FindView("CancelBackground", out BitmapView cancelBg)) 
            {
                cancelBg.SetColor(0xFF8888);
                cancelBg.SetBitmap(Textures.FloorButton); 
            };

            if (Window.FindView("Cancel", out _viewCache.Cancel)) 
            {
                _viewCache.Cancel.Clicked = CancelClicked;
            };


            Floor currFloor = Floor.None;

            foreach (var gridEntry in _gridInfo)
            {
                foreach (var exitInfo in gridEntry.Value)
                {
                    Floor floor = exitInfo.Value.Floor;

                    if (currFloor != floor)
                    {
                        _viewCache.FloorView.Add(new FloorView(_viewCache.FloorRoot, exitInfo.Value.Floor.ToString()));
                        currFloor = floor;
                    }
                }

                _viewCache.FloorView.LastOrDefault().GridEntryView.Add(new GridEntryView(gridEntry));
            }

            _viewCache.FloorRoot.FitToContents();
            _viewCache.ExitRoot.FitToContents();
        }

        public void SetLabels()
        {
            _viewCache.LeftTitle.Text = Playfield.ModelIdentity.Instance == PlayfieldIds.Grid ? "" : "L  M  R";
            _viewCache.RightTitle.Text = "Grid Exit";
        }

        private void CancelClicked(object sender, ButtonBase e)
        {
            Main.GridNav.Halt();
            Main.FixerGridNav.Halt();
            ResetGfx();
            Midi.Play("Click");
        }

        public void UpdateViewSelector(List<GridEntryView> gridEntryView)
        {
            foreach (var entry in _viewCache.GridEntryView)
                _viewCache.ExitRoot.RemoveChild(entry.Root);

            _viewCache.GridEntryView = gridEntryView;

            foreach (var entry in _viewCache.GridEntryView)
                _viewCache.ExitRoot.AddChild(entry.Root, true);

            SetLabels();
            _viewCache.ExitRoot.FitToContents();
        }

        public void ResetGfx()
        {
            foreach (var buttonView in _viewCache.GridEntryView.SelectMany(x=>x.ExitButtonView))
                buttonView.Button.SetAllGfx(Textures.RedCircle);
        }

        public void ResetFloorButtonColors()
        {
            foreach (var floorView in _viewCache.FloorView)
                floorView.ResetBackgroundColor();
        }

        public void Dispose()
        {
            foreach (var gridEntryView in _viewCache.GridEntryView)
                gridEntryView.Dispose();

            foreach (var floorView in _viewCache.FloorView)
                floorView.Dispose();

            _viewCache.GridEntryView.Clear();
            _viewCache.FloorView.Clear();

            if (Window.IsValid)
                Window.Close();
        }

        public class Views
        {
            public View FloorRoot;
            public View ExitRoot;
            public TextView LeftTitle;
            public TextView RightTitle;
            public Button Cancel;
            public List<FloorView> FloorView = new List<FloorView>();
            public List<GridEntryView> GridEntryView = new List<GridEntryView>();
        }

        internal static class Textures
        {
            public const int RedCircle = 1430135;
            public const int GreenCircle = 1430136;
            public const int Background = 1430137;
            public const int FloorButton = 1430138;
        }
    }
}