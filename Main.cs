using AOSharp.Common.GameData;
using AOSharp.Common.Unmanaged.Imports;
using AOSharp.Core;
using AOSharp.Core.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MalisGridNavigator
{
    public class Main : AOPluginEntry
    {
        public static string PluginDir;
        public static GridNavWindow Window;

        public static GridNav GridNav;
        public static FixerGridNav FixerGridNav;

        public override void Run(string pluginDir)
        {
            Chat.WriteLine("- Mali's Grid Navigator-", ChatColor.Gold);

            PluginDir = pluginDir; 

            Utils.LoadCustomTextures($"{PluginDir}\\UI\\Textures\\", 1430135);

            GridNav = new GridNav(PlayfieldIds.Grid, GridExits, GridElevators);
            FixerGridNav = new FixerGridNav(PlayfieldIds.FixerGrid, FixerGridExits, FixerGridElevators);
        }

        public override void Teardown()
        {
            Midi.TearDown();
            Window.Dispose();
        }

        public static Dictionary<GridExit, Dictionary<GridSide, GridExitInfo>> GridExits = new Dictionary<GridExit, Dictionary<GridSide, GridExitInfo>>()
        {
            { GridExit.Newland, new Dictionary<GridSide, GridExitInfo> { { GridSide.None, new GridExitInfo { Floor = Floor.Zero, Position = new Vector3(177.8126, 3.700028, 173.0097) } } } },
            { GridExit.Tir, new Dictionary<GridSide, GridExitInfo> { { GridSide.None, new GridExitInfo { Floor = Floor.Zero, Position = new Vector3(150.8562, 3.60001, 184.7127) } } } },
            { GridExit.OmniTrade, new Dictionary<GridSide, GridExitInfo> { { GridSide.None, new GridExitInfo { Floor = Floor.Zero, Position = new Vector3(170.01, 3.6, 240.8323) } } } },
            { GridExit.Borealis, new Dictionary<GridSide, GridExitInfo> { { GridSide.None, new GridExitInfo { Floor = Floor.Zero, Position = new Vector3(242.2967, 3.1, 199.4019) } } } },
            { GridExit.OldAthen, new Dictionary<GridSide, GridExitInfo> { { GridSide.None, new GridExitInfo { Floor = Floor.Zero, Position = new Vector3(214.5266, 3.200002, 173.0108) } } } },
            { GridExit.ICC, new Dictionary<GridSide, GridExitInfo> { { GridSide.None, new GridExitInfo { Floor = Floor.Zero, Position = new Vector3(237.5, 3.1, 217.4) } } } },
            { GridExit.CoP, new Dictionary<GridSide, GridExitInfo> { { GridSide.None, new GridExitInfo { Floor = Floor.Zero, Position = new Vector3(201.7671, 3.099974, 253.0732) } } } },
            { GridExit.CoT, new Dictionary<GridSide, GridExitInfo> { { GridSide.None, new GridExitInfo { Floor = Floor.Zero, Position = new Vector3(219.2315, 3.138839, 252.2457) } } } },
            { GridExit.UniDefHub, new Dictionary<GridSide, GridExitInfo> { { GridSide.None, new GridExitInfo { Floor = Floor.Zero, Position = new Vector3(208.2991, 3.17329, 217.6294) } } } },

            { GridExit.HO2, new Dictionary<GridSide, GridExitInfo> { { GridSide.None, new GridExitInfo { Floor = Floor.One, Position = new Vector3(189.1437, 36.5, 207.9386) } } } },
            { GridExit.BrokenShores, new Dictionary<GridSide, GridExitInfo> { { GridSide.None, new GridExitInfo { Floor = Floor.One, Position = new Vector3(243.4243, 36.50009, 221.5573) } } } },
            { GridExit.FourHoles, new Dictionary<GridSide, GridExitInfo> { { GridSide.None, new GridExitInfo { Floor = Floor.One, Position = new Vector3(196.8505, 36.49997, 204.6926) } } } },
            { GridExit.Clondyke, new Dictionary<GridSide, GridExitInfo> { { GridSide.None, new GridExitInfo { Floor = Floor.One, Position = new Vector3(206.7348, 36.50002, 226.3772) } } } },
            { GridExit.Galway, new Dictionary<GridSide, GridExitInfo> { { GridSide.None, new GridExitInfo { Floor = Floor.One, Position = new Vector3(215.4685, 36.5, 229.9121) } } } },
            { GridExit.Athen, new Dictionary<GridSide, GridExitInfo> { { GridSide.None, new GridExitInfo { Floor = Floor.One, Position = new Vector3(195.9248, 36.5, 182.0787) } } } },
            { GridExit.Meetmedere, new Dictionary<GridSide, GridExitInfo> { { GridSide.None, new GridExitInfo { Floor = Floor.One, Position = new Vector3(171.6873, 36.5, 168.0772) } } } },
            { GridExit.Harrys, new Dictionary<GridSide, GridExitInfo> { { GridSide.None, new GridExitInfo { Floor = Floor.One, Position = new Vector3(181.2249, 36.49998, 226.8944) } } } },
            { GridExit.OmniEnt, new Dictionary<GridSide, GridExitInfo> { { GridSide.None, new GridExitInfo { Floor = Floor.One, Position = new Vector3(180.561, 36.6, 244.2538) } } } },
            { GridExit.Rome, new Dictionary<GridSide, GridExitInfo> { { GridSide.None, new GridExitInfo { Floor = Floor.One, Position = new Vector3(174.8858, 36.5, 245.576) } } } },
            { GridExit.LushHills, new Dictionary<GridSide, GridExitInfo> { { GridSide.None, new GridExitInfo { Floor = Floor.One, Position = new Vector3(189.3578, 36.5, 239.6848) } } } },

            { GridExit.OmniHq, new Dictionary<GridSide, GridExitInfo> { { GridSide.None, new GridExitInfo { Floor = Floor.Two, Position = new Vector3(229.5762, 42.79994, 231.8396) } } } },
            { GridExit.Camelot, new Dictionary<GridSide, GridExitInfo> { { GridSide.None, new GridExitInfo { Floor = Floor.Two, Position = new Vector3(211.8841, 42.80011, 151.4814) } } } },
            { GridExit.Sentinels, new Dictionary<GridSide, GridExitInfo> { { GridSide.None, new GridExitInfo { Floor = Floor.Two, Position = new Vector3(179.5188, 43.2, 150.7599) } } } },
        };
        public static Dictionary<Floor, Dictionary<ElevatorType, List<Vector3>>> GridElevators = new Dictionary<Floor, Dictionary<ElevatorType, List<Vector3>>>()
        {
            {
                Floor.Zero,
                new Dictionary<ElevatorType, List<Vector3>>
                {
                    {
                        ElevatorType.Up,
                        new List<Vector3>
                        {
                            new Vector3(234.7277, 3.900518, 272.2799),
                            new Vector3(171.086, 3.900405, 279.2088),
                            new Vector3(135.653, 3.8, 167.5426),
                            new Vector3(136.5473, 3.901233, 228.9515),
                            new Vector3(171.8443, 3.90069, 136.2363),
                            new Vector3(231.2291, 3.900515, 132.8629),
                            new Vector3(268.5862, 3.900621, 169.2916),
                            new Vector3(272.2536, 3.900552, 228.3615),
                        }
                    }
                }
            },
            {
                Floor.One,
                new Dictionary<ElevatorType, List<Vector3>>
                {
                    {
                        ElevatorType.Up,
                        new List<Vector3>
                        {
                            new Vector3(229.6694, 36.5, 272.6177),
                            new Vector3(174.6143, 36.49997, 272.7457),
                            new Vector3(275.0575, 36.5, 171.6306),
                            new Vector3(274.6978, 36.5, 226.9639),
                            new Vector3(230.5555, 36.5, 127.1759),
                            new Vector3(174.9824, 36.5, 126.6896),
                            new Vector3(129.6331, 36.5, 171.4566),
                            new Vector3(129.6246, 36.5, 226.8833)
                        }
                    },
                    {
                        ElevatorType.Down,
                        new List<Vector3>
                        {
                            new Vector3(229.7757, 36.5, 266.3273),
                            new Vector3(174.7004, 36.5, 266.1311),
                            new Vector3(268.1686, 36.49997, 226.7834),
                            new Vector3(268.4781, 36.5, 171.6642),
                            new Vector3(230.605, 36.5, 133.4632),
                            new Vector3(175.0488, 36.5, 133.2783),
                            new Vector3(135.9213, 36.5, 171.4692),
                            new Vector3(135.9465, 36.5, 227.0124),
                        }
                    },
                }
            },
            {
                Floor.Two,
                new Dictionary<ElevatorType, List<Vector3>>
                {
                    {
                        ElevatorType.Down,
                        new List<Vector3>
                        {
                            new Vector3(183.886, 44.50042, 278.5094),
                            new Vector3(124.6571, 44.5006, 216.2093),
                            new Vector3(282.9151, 44.5006, 179.7437),
                            new Vector3(184.2254, 44.50077, 119.4548),
                            new Vector3(220.6486, 44.50056, 120.5631),
                            new Vector3(281.5297, 44.50056, 217.4783),
                            new Vector3(220.6667, 44.50008, 279.8162),
                            new Vector3(123.6355, 44.50086, 181.3813)
                        }
                    }
                }
            }
        };
        public static Dictionary<GridExit, Dictionary<GridSide, GridExitInfo>> FixerGridExits = new Dictionary<GridExit, Dictionary<GridSide, GridExitInfo>>()
        {
            { GridExit.NewlandCity, new Dictionary<GridSide, GridExitInfo> { { GridSide.Middle,new GridExitInfo { Floor = Floor.One, Position = new Vector3(323.165, 11.91377, 66.87515) } } } },
            { GridExit.OmniTrade, new Dictionary<GridSide, GridExitInfo> { { GridSide.Middle, new GridExitInfo { Floor = Floor.One, Position = new Vector3(318.6, 11.91377, 78.30001) } } } },
            { GridExit.OmniHq, new Dictionary<GridSide, GridExitInfo> { { GridSide.Middle, new GridExitInfo { Floor = Floor.One, Position = new Vector3(307.2, 11.91377, 83.1) } } } },
            { GridExit.OmniEnt, new Dictionary<GridSide, GridExitInfo> { { GridSide.Middle, new GridExitInfo { Floor = Floor.One, Position = new Vector3(295.7, 11.91377, 78.5) } } } },
            { GridExit.Borealis, new Dictionary<GridSide, GridExitInfo> { { GridSide.Middle, new GridExitInfo { Floor = Floor.One, Position = new Vector3(290.9, 11.91377, 67) } } } },
            { GridExit.OldAthens, new Dictionary<GridSide, GridExitInfo> { { GridSide.Middle, new GridExitInfo { Floor = Floor.One, Position = new Vector3(295.6, 11.91377, 55.7) } } } },
            { GridExit.WestAthen, new Dictionary<GridSide, GridExitInfo> { { GridSide.Middle, new GridExitInfo { Floor = Floor.One, Position = new Vector3(307, 11.91377, 50.8) } } } },
            { GridExit.TirCity, new Dictionary<GridSide, GridExitInfo> { { GridSide.Middle, new GridExitInfo { Floor = Floor.One, Position = new Vector3(318.4, 11.91377, 55.5) } } } },


            { GridExit.Newland, new Dictionary<GridSide, GridExitInfo> { { GridSide.Middle, new GridExitInfo { Floor = Floor.Two, Position = new Vector3(322.1828, 21.91377, 61.32129) } } } },
            { GridExit.OmniForest, new Dictionary<GridSide, GridExitInfo> { { GridSide.Middle, new GridExitInfo { Floor = Floor.Two, Position = new Vector3(321.7678, 21.91377, 73.65694) } } } },
            { GridExit.RomeBlue, new Dictionary<GridSide, GridExitInfo> { { GridSide.Middle, new GridExitInfo { Floor = Floor.Two, Position = new Vector3(312.6968, 21.91377, 82.06639) } } } },
            { GridExit.RomeRed, new Dictionary<GridSide, GridExitInfo> { { GridSide.Middle, new GridExitInfo { Floor = Floor.Two, Position = new Vector3(300.3171, 21.91377, 81.67691) } } } },
            { GridExit.RomeGreen, new Dictionary<GridSide, GridExitInfo> { { GridSide.Middle, new GridExitInfo { Floor = Floor.Two, Position = new Vector3(291.8734, 21.91377, 72.51205) } } } },
            { GridExit.HolesInTheWall, new Dictionary<GridSide, GridExitInfo> { { GridSide.Middle, new GridExitInfo { Floor = Floor.Two, Position = new Vector3(292.4253, 21.91377, 60.28604) } } } },
            { GridExit.AthenShire, new Dictionary<GridSide, GridExitInfo> { { GridSide.Middle, new GridExitInfo { Floor = Floor.Two, Position = new Vector3(301.462, 21.91377, 51.78261) } } } },
            { GridExit.TirCounty, new Dictionary<GridSide, GridExitInfo> { { GridSide.Middle, new GridExitInfo { Floor = Floor.Two, Position = new Vector3(313.782, 21.91377, 52.30026) } } } },


            { GridExit.NewlandDesert, new Dictionary<GridSide, GridExitInfo> { { GridSide.Middle, new GridExitInfo { Floor = Floor.Three, Position = new Vector3(319.3137, 31.91377, 56.48801) } } } },
            { GridExit.GreaterOmniForest, new Dictionary<GridSide, GridExitInfo> { { GridSide.Middle, new GridExitInfo { Floor = Floor.Three, Position = new Vector3(323.1565, 31.91377, 68.21046) } } } },
            { GridExit.LushFields, new Dictionary<GridSide, GridExitInfo> { { GridSide.Middle, new GridExitInfo { Floor = Floor.Three, Position = new Vector3(317.5087, 31.91377, 79.21513) } } } },
            { GridExit.MutantDomain, new Dictionary<GridSide, GridExitInfo> { { GridSide.Middle, new GridExitInfo { Floor = Floor.Three, Position = new Vector3(305.7423, 31.91377, 83.08315) } } } },
            {
    GridExit.StretWestBank, new Dictionary<GridSide, GridExitInfo>
            {
                { GridSide.Right, new GridExitInfo { Floor = Floor.Three, Position = new Vector3(294.6733, 31.91377, 77.35879) } },
                { GridSide.Left, new GridExitInfo { Floor = Floor.Three, Position = new Vector3(291.0105, 31.91377, 65.68131) } }
            } },
            { GridExit.WailingWastes, new Dictionary<GridSide, GridExitInfo> { { GridSide.Middle, new GridExitInfo { Floor = Floor.Three, Position = new Vector3(296.594, 31.91377, 54.60003) } } } },
            { GridExit.GreaterTirCounty, new Dictionary<GridSide, GridExitInfo> { { GridSide.Middle, new GridExitInfo { Floor = Floor.Three, Position = new Vector3(308.3481, 31.91377, 50.8729) } } } },


            {
    GridExit.VarmitWoods, new Dictionary<GridSide, GridExitInfo>
            {
                { GridSide.Right, new GridExitInfo { Floor = Floor.Four, Position = new Vector3(314.9615, 41.91377, 52.94378) } },
                { GridSide.Left, new GridExitInfo { Floor = Floor.Four, Position = new Vector3(322.5987, 41.91377, 62.61747) } }
            } },
            { GridExit.GalwayCounty, new Dictionary<GridSide, GridExitInfo> { { GridSide.Middle, new GridExitInfo { Floor = Floor.Four, Position = new Vector3(321.0552, 41.91377, 74.89011) } } } },
            { GridExit.GalwayShire, new Dictionary<GridSide, GridExitInfo> { { GridSide.Middle, new GridExitInfo { Floor = Floor.Four, Position = new Vector3(311.3213, 41.91377, 82.54911) } } } },
            {
    GridExit.Clondyke, new Dictionary<GridSide, GridExitInfo>
            {
                { GridSide.Right, new GridExitInfo { Floor = Floor.Four, Position = new Vector3(298.962, 41.91377, 80.95565) } },
                { GridSide.Left, new GridExitInfo { Floor = Floor.Four, Position = new Vector3(291.5262, 41.91377, 71.2351) } }
            } },
            { GridExit.WartornValley, new Dictionary<GridSide, GridExitInfo> { { GridSide.Middle, new GridExitInfo { Floor = Floor.Four, Position = new Vector3(292.9831, 41.91377, 58.91245) } } } },
            { GridExit.Aegean, new Dictionary<GridSide, GridExitInfo> { { GridSide.Middle, new GridExitInfo { Floor = Floor.Four, Position = new Vector3(302.7537, 41.91377, 51.39006) } } } },


            {
    GridExit.FourHoles, new Dictionary<GridSide, GridExitInfo>
            {
                { GridSide.Right, new GridExitInfo { Floor = Floor.Five, Position = new Vector3(297.6735, 51.91377, 53.78938) } },
                { GridSide.Middle, new GridExitInfo { Floor = Floor.Five, Position = new Vector3(309.6569, 51.91377, 51.06101) } },
                { GridSide.Left, new GridExitInfo { Floor = Floor.Five, Position = new Vector3(320.1617, 51.91377, 57.55252) } }
            } },
            {
    GridExit.PleasentMeadows, new Dictionary<GridSide, GridExitInfo>
            {
                { GridSide.Right, new GridExitInfo { Floor = Floor.Five, Position = new Vector3(322.9087, 51.91377, 69.61296) } },
                { GridSide.Left, new GridExitInfo { Floor = Floor.Five, Position = new Vector3(316.3812, 51.91377, 80.1392) } }
            } },
            {
    GridExit.Andromeda, new Dictionary<GridSide, GridExitInfo>
            {
                { GridSide.Right, new GridExitInfo { Floor = Floor.Five, Position = new Vector3(304.2223, 51.91377, 82.86882) } },
                { GridSide.Middle, new GridExitInfo { Floor = Floor.Five, Position = new Vector3(293.9104, 51.91377, 76.27757) } },
                { GridSide.Left, new GridExitInfo { Floor = Floor.Five, Position = new Vector3(291.0649, 51.91377, 64.19978) } }
            } },


            {
    GridExit.TheLongestRoad, new Dictionary<GridSide, GridExitInfo>
            {
                { GridSide.Right, new GridExitInfo { Floor = Floor.Six, Position = new Vector3(293.7203, 61.91377, 57.78148) } },
                { GridSide.Middle, new GridExitInfo { Floor = Floor.Six, Position = new Vector3(304.077, 61.91377, 51.09941) } },
                { GridSide.Left, new GridExitInfo { Floor = Floor.Six, Position = new Vector3(316.1394, 61.91377, 53.62648) } }
            } },
            {
    GridExit.MilkyWay, new Dictionary<GridSide, GridExitInfo>
            {
                { GridSide.Right, new GridExitInfo { Floor = Floor.Six, Position = new Vector3(322.8455, 61.91377, 64.02014) } },
                { GridSide.Left, new GridExitInfo { Floor = Floor.Six, Position = new Vector3(320.3117, 61.91377, 76.14408) } }
            } },
            {
    GridExit.StretEastBank, new Dictionary<GridSide, GridExitInfo>
            {
                { GridSide.Right, new GridExitInfo { Floor = Floor.Six, Position = new Vector3(309.8196, 61.91377, 82.86757) } },
                { GridSide.Middle, new GridExitInfo { Floor = Floor.Six, Position = new Vector3(297.8753, 61.91377, 80.20058) } },
                { GridSide.Left, new GridExitInfo { Floor = Floor.Six, Position = new Vector3(291.0781, 61.91377, 69.85567) } }
            } },

            {
    GridExit.SouthernFoulsHills, new Dictionary<GridSide, GridExitInfo>
            {
                { GridSide.Right, new GridExitInfo { Floor = Floor.Seven, Position = new Vector3(291.3708, 71.91377, 62.88487) } },
                { GridSide.Left, new GridExitInfo { Floor = Floor.Seven, Position = new Vector3(298.8307, 71.91377, 53.07289) } }
            } },
            {
    GridExit.SouthernArteryValley, new Dictionary<GridSide, GridExitInfo>
            {
                { GridSide.Right, new GridExitInfo { Floor = Floor.Seven, Position = new Vector3(311.0169, 71.91377, 51.31287) } },
                { GridSide.Middle, new GridExitInfo { Floor = Floor.Seven, Position = new Vector3(320.8734, 71.91377, 58.78619) } },
                { GridSide.Left, new GridExitInfo { Floor = Floor.Seven, Position = new Vector3(322.6389, 71.91377, 71.04559) } }
            } },
            {
    GridExit.UpperStretEastBank, new Dictionary<GridSide, GridExitInfo>
            {
                { GridSide.Right, new GridExitInfo { Floor = Floor.Seven, Position = new Vector3(315.079, 71.91377, 80.95204) } },
                { GridSide.Middle, new GridExitInfo { Floor = Floor.Seven, Position = new Vector3(302.9428, 71.91377, 82.53097) } },
                { GridSide.Left, new GridExitInfo { Floor = Floor.Seven, Position = new Vector3(292.9998, 71.91377, 75.10767) } }
            } },


            {
    GridExit.Avalon, new Dictionary<GridSide, GridExitInfo>
            {
                { GridSide.Right, new GridExitInfo { Floor = Floor.Eight, Position = new Vector3(290.9084, 81.91377, 68.48405) } },
                { GridSide.Left, new GridExitInfo { Floor = Floor.Eight, Position = new Vector3(294.5486, 81.91377, 56.72783) } }
            } },
            {
    GridExit.BelialForest, new Dictionary<GridSide, GridExitInfo>
            {
                { GridSide.Right, new GridExitInfo { Floor = Floor.Eight, Position = new Vector3(305.412, 81.91377, 50.89072) } },
                { GridSide.Middle, new GridExitInfo { Floor = Floor.Eight, Position = new Vector3(317.2301, 81.91377, 54.54236) } },
                { GridSide.Left, new GridExitInfo { Floor = Floor.Eight, Position = new Vector3(323.082, 81.91377, 65.45864) } }
            } },
            {
    GridExit.CentralArteryValley, new Dictionary<GridSide, GridExitInfo>
            {
                { GridSide.Right, new GridExitInfo { Floor = Floor.Eight, Position = new Vector3(319.3661, 81.91377, 77.35326) } },
                { GridSide.Middle, new GridExitInfo { Floor = Floor.Eight, Position = new Vector3(308.5018, 81.91377, 82.98767) } },
                { GridSide.Left, new GridExitInfo { Floor = Floor.Eight, Position = new Vector3(296.6365, 81.91377, 79.42649) } }
            } },


            {
    GridExit.Mort, new Dictionary<GridSide, GridExitInfo>
            {
                { GridSide.Right, new GridExitInfo { Floor = Floor.Nine, Position = new Vector3(292.3889, 91.91377, 73.90373) } },
                { GridSide.Middle, new GridExitInfo { Floor = Floor.Nine, Position = new Vector3(291.7542, 91.91377, 61.58836) } },
                { GridSide.Left, new GridExitInfo { Floor = Floor.Nine, Position = new Vector3(300.0008, 91.91377, 52.41096) } }
            } },
            {
    GridExit.BrokenShores, new Dictionary<GridSide, GridExitInfo>
            {
                { GridSide.Right, new GridExitInfo { Floor = Floor.Nine, Position = new Vector3(312.3551, 91.91377, 51.8005) } },
                { GridSide.Left, new GridExitInfo { Floor = Floor.Nine, Position = new Vector3(321.5876, 91.91377, 60.05707) } }
            } },
            {
    GridExit.DeepArteryValley, new Dictionary<GridSide, GridExitInfo>
            {
                { GridSide.Right, new GridExitInfo { Floor = Floor.Nine, Position = new Vector3(322.1638, 91.91377, 72.50528) } },
                { GridSide.Middle, new GridExitInfo { Floor = Floor.Nine, Position = new Vector3(313.8817, 91.91377, 81.51563) } },
                { GridSide.Left, new GridExitInfo { Floor = Floor.Nine, Position = new Vector3(301.5187, 91.91377, 82.22952) } }
            } },


            {
    GridExit.PrepetualWastelands, new Dictionary<GridSide, GridExitInfo>
            {
                { GridSide.Right, new GridExitInfo { Floor = Floor.Ten, Position = new Vector3(295.6318, 101.9138, 78.47115) } },
                { GridSide.Middle, new GridExitInfo { Floor = Floor.Ten, Position = new Vector3(290.8146, 101.9138, 67.16725) } },
                { GridSide.Left, new GridExitInfo { Floor = Floor.Ten, Position = new Vector3(295.4624, 101.9138, 55.66339) } }
            } },
            {
    GridExit.EasternFoulsPlains, new Dictionary<GridSide, GridExitInfo>
            {
                { GridSide.Right, new GridExitInfo { Floor = Floor.Ten, Position = new Vector3(323.0843, 101.9141, 66.98514) } },
                { GridSide.Middle, new GridExitInfo { Floor = Floor.Ten, Position = new Vector3(318.4632, 101.9139, 78.33324) } },
                { GridSide.Left, new GridExitInfo { Floor = Floor.Ten, Position = new Vector3(307.0642, 101.9138, 83.13419) } }
            } },
        };
        public static Dictionary<Floor, Dictionary<ElevatorType, List<Vector3>>> FixerGridElevators = new Dictionary<Floor, Dictionary<ElevatorType, List<Vector3>>>()
        {
            {
                Floor.Zero,
                new Dictionary<ElevatorType,List<Vector3>>
                {
                    { ElevatorType.Up, new List<Vector3> {new Vector3 (307, 1.313771, 67) } },
                }
            },
            {
                Floor.One,
                new Dictionary<ElevatorType,List<Vector3>>
                {
                    { ElevatorType.Up, new List<Vector3> {new Vector3(313.8423, 12.31377, 66.95627) } },
                    { ElevatorType.Down,new List<Vector3> {new Vector3(300.2563, 12.31375, 66.94968) } },
                }
            },
            {
                Floor.Two,
                new Dictionary<ElevatorType,List<Vector3>>
                {
                    { ElevatorType.Up, new List<Vector3> {new Vector3(313.4414, 22.31378, 64.6021) } },
                    { ElevatorType.Down, new List<Vector3> {new Vector3(300.612, 22.31378, 69.21096) } },
                }
            },
            {
                Floor.Three,
                new Dictionary<ElevatorType,List<Vector3>>
                {
                    { ElevatorType.Up, new List<Vector3> {new Vector3(312.2277, 32.31377, 62.54662) } },
                    { ElevatorType.Down, new List<Vector3> {new Vector3(301.8001, 32.31377, 71.29952) } },
                }
            },
            {
                Floor.Four,
                new Dictionary<ElevatorType,List<Vector3>>
                {
                    { ElevatorType.Up, new List<Vector3> {new Vector3(310.3886, 42.31377, 61.06009) } },
                    { ElevatorType.Down, new List<Vector3> {new Vector3(303.6136, 42.31374, 72.81461) } },
                }
            },
            {
                Floor.Five,
                new Dictionary<ElevatorType,List<Vector3>>
                {
                    { ElevatorType.Up, new List<Vector3> {new Vector3(308.1369, 52.31377, 60.25761) } },
                    { ElevatorType.Down, new List<Vector3> {new Vector3(305.7891, 52.31377, 73.68073) } },
                }
            },
            {
                Floor.Six,
                new Dictionary<ElevatorType,List<Vector3>>
                {
                    { ElevatorType.Up, new List<Vector3> {new Vector3(305.7449, 62.31377, 60.24995) } },
                    { ElevatorType.Down, new List<Vector3> {new Vector3(308.1276, 62.31377, 73.66639) } },
                }
            },
            {
                Floor.Seven,
                new Dictionary<ElevatorType,List<Vector3>>
                {
                    { ElevatorType.Up,new List<Vector3> {new Vector3(303.5269, 72.31377, 61.13066) } },
                    { ElevatorType.Down, new List<Vector3> {new Vector3(310.3562, 72.31377, 72.86738) } },
                }
            },
            {
                Floor.Eight,
                new Dictionary<ElevatorType,List<Vector3>>
                {
                    { ElevatorType.Up, new List<Vector3> { new Vector3(301.7581, 82.31377, 62.66596) } },
                    { ElevatorType.Down,new List<Vector3> { new Vector3(312.1694, 82.31377, 71.39105)} },
                }
            },
            {
                Floor.Nine,
                new Dictionary<ElevatorType,List<Vector3>>
                {
                    { ElevatorType.Up, new List<Vector3> { new Vector3(300.5934, 92.31377, 64.73015) } },
                    { ElevatorType.Down, new List<Vector3> { new Vector3(313.3604, 92.31377, 69.3892) } },
                }
            },
            {
                Floor.Ten,
                new Dictionary<ElevatorType, List<Vector3>>
                {
                    { ElevatorType.Down, new List<Vector3> { new Vector3(313.7806, 102.3138, 67.06682) } },
                }
            },
        };
    }
}