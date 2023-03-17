using AOSharp.Common.Unmanaged.Interfaces;
using AOSharp.Core;
using AOSharp.Core.UI;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MalisGridNavigator
{
    public class Utils
    {
        public static void LoadCustomTextures(string path, int startId)
        {
            DirectoryInfo textureDir = new DirectoryInfo(path);

            foreach (var file in textureDir.GetFiles("*.png").OrderBy(x => x.Name))
            {
                GuiResourceManager.CreateGUITexture(file.Name.Replace(".png", "").Remove(0, 4), startId++, file.FullName);
            }
        }
    }

    public class PlayfieldIds
    {
        public const int Grid = 152;
        public const int FixerGrid = 4107;
    }
}