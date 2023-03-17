using AOSharp.Common.GameData;
using AOSharp.Core.UI;
using static MalisGridNavigator.GridNavWindow;

namespace MalisGridNavigator
{
    public class GridEntryView
    {
        public View Root;
        private View _view;
        private TextView _textView;
        private Button _button;
        private GridExit _gridExit;

        public GridEntryView(View rootView, GridExit gridExitName)
        {
            Root = rootView;

            _view = View.CreateFromXml($"{Main.PluginDir}\\UI\\Views\\GridEntryView.xml");
            _gridExit = gridExitName;

            if (_view.FindChild("Text", out _textView)) { _textView.Text = gridExitName.ToString(); };
          
            if (_view.FindChild("Button", out _button)) 
            {
                _button.SetAllGfx(Textures.RedCircle);
                _button.Clicked = ButtonClick; 
            };

            Root.AddChild(_view, true);
            Root.FitToContents();
        }

        public void SetGfx(int id) => _button.SetAllGfx(id);

        private void ButtonClick(object sender, ButtonBase e)
        {
            Main.Window.ResetGfx();
            SetGfx(Textures.GreenCircle);
            Main.Grid.SetExit(_gridExit);

            Chat.WriteLine($"Destination set to:{_gridExit}");
            Midi.Play("Click");
        }

        public void Dispose()
        {
            Root.RemoveChild(_view);
            _view.Dispose();
        }
    }
}