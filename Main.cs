using AOSharp.Common.GameData;
using AOSharp.Common.Unmanaged.Imports;
using AOSharp.Core;
using AOSharp.Core.UI;
using System;

namespace MalisGridNavigator
{
    public class Main : AOPluginEntry
    {
        public static string PluginDir;
        public static GridNavWindow Window;
        public static GridNav Grid;
        private bool _test = false;

        public override void Run(string pluginDir)
        {
            Chat.WriteLine("- Mali's Grid Navigator-", ChatColor.Gold);

            PluginDir = pluginDir;
            Utils.LoadCustomTextures($"{PluginDir}\\UI\\Textures\\", 1430135);

            Grid = new GridNav();

            Game.OnUpdate += Grid.OnUpdate;
            Game.TeleportEnded += Grid.TeleportEnded;
            Game.TeleportStarted += Grid.TeleportStarted;

            Midi.Play("Alert");
        }

        public override void Teardown()
        {
            Midi.TearDown();
            Window.Dispose();
        }
    }
}