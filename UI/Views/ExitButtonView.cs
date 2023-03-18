using AOSharp.Common.GameData;
using AOSharp.Core.UI;
using System.Collections.Generic;
using static MalisGridNavigator.GridNavWindow;

namespace MalisGridNavigator
{
    public enum ExitButtonType
    {
        Blank,
        Normal
    }

    public class ExitButtonView
    {
        public Button Button;
        public GridExitDestination GridExitObject;
        private View _localRoot;
        public View ExitButtonRoot;

        public ExitButtonView(View exitButtonRoot, GridExitDestination gridExitObject)
        {
            _localRoot = View.CreateFromXml($"{Main.PluginDir}\\UI\\Views\\ExitButtonView.xml");
            GridExitObject = gridExitObject;
            ExitButtonRoot = exitButtonRoot;

            if (_localRoot.FindChild("Button", out Button))
            {
                Button.SetAllGfx(Textures.RedCircle);
                Button.Clicked = ButtonClick;
            };

            ExitButtonRoot.AddChild(_localRoot, true);
            ExitButtonRoot.FitToContents();
        }

        private void ButtonClick(object sender, ButtonBase e)
        {
            Main.Window.ResetGfx();
            ((Button)e).SetAllGfx(Textures.GreenCircle);

            if (GridExitObject.GridSide == GridSide.None)
                Main.GridNav.SetCurrentExit(GridExitObject);
            else
                Main.FixerGridNav.SetCurrentExit(GridExitObject);

            Midi.Play("Click");
        }

        public void Dispose()
        {
            _localRoot.Dispose();
        }
    }
}