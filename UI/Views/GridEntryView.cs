using AOSharp.Common.GameData;
using AOSharp.Core;
using AOSharp.Core.UI;
using System.Collections.Generic;
using System.Linq;

namespace MalisGridNavigator
{
    public class GridEntryView
    {
        public View Root;
        private View ExitButtonRoot;
        private TextView _textView;
        public List<ExitButtonView> ExitButtonView = new List<ExitButtonView>();
        public List<BlankView> BlankView = new List<BlankView>();

        public GridEntryView(KeyValuePair<GridExit, Dictionary<GridSide, GridExitInfo>> gridExitName)
        {
            Root = View.CreateFromXml($"{Main.PluginDir}\\UI\\Views\\GridEntryView.xml");

            if (Root.FindChild("Text", out _textView)) { _textView.Text = gridExitName.Key.ToString(); };
            
            if (Root.FindChild("ExitButtonRoot", out ExitButtonRoot)) {};

            if (Playfield.ModelIdentity.Instance == PlayfieldIds.FixerGrid)
            {
                var reversedGridInfo = gridExitName.Value.Reverse();

                switch (reversedGridInfo.Count())
                {
                    case 1:
                        BlankView.Add(new BlankView(ExitButtonRoot));
                        ExitButtonView.Add(new ExitButtonView(ExitButtonRoot, new GridExitDestination { GridExit = gridExitName.Key, GridSide = reversedGridInfo.First().Key }));
                        BlankView.Add(new BlankView(ExitButtonRoot));
                        break;
                    case 2:
                        ExitButtonView.Add(new ExitButtonView(ExitButtonRoot, new GridExitDestination { GridExit = gridExitName.Key, GridSide = reversedGridInfo.First().Key }));
                        BlankView.Add(new BlankView(ExitButtonRoot));
                        ExitButtonView.Add(new ExitButtonView(ExitButtonRoot, new GridExitDestination { GridExit = gridExitName.Key, GridSide = reversedGridInfo.Last().Key }));
                        break;
                    case 3:
                        foreach (var gridExitInfo in reversedGridInfo)
                            ExitButtonView.Add(new ExitButtonView(ExitButtonRoot, new GridExitDestination { GridExit = gridExitName.Key, GridSide = gridExitInfo.Key }));
                        break;
                }
            }
            else
            {
                foreach (var gridExitInfo in gridExitName.Value)
                    ExitButtonView.Add(new ExitButtonView(ExitButtonRoot, new GridExitDestination { GridExit = gridExitName.Key, GridSide = gridExitInfo.Key }));
            }
        }

        public void Dispose()
        {
            foreach (ExitButtonView exitButtonView in ExitButtonView)
                exitButtonView.Dispose();

            Root.Dispose();
        }
    }
}