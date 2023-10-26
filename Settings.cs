using AOSharp.Common.GameData;
using AOSharp.Core.UI;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MalisGridNavigator
{
    public class Settings
    {
        public bool AutoLoadUi;

        public void Save()
        {
            File.WriteAllText($"{Main.PluginDir}\\JSON\\Settings.json", JsonConvert.SerializeObject(this));
        }

        public static Settings Load(string path)
        {
            try
            {
                return JsonConvert.DeserializeObject<Settings>(File.ReadAllText(path));
            }
            catch
            {
                Chat.WriteLine($"Config file can't be loaded.");
                return null;
            }
        }
    }
}