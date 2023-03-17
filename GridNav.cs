using AOSharp.Common.GameData;
using AOSharp.Common.Unmanaged.Interfaces;
using AOSharp.Core;
using AOSharp.Core.UI;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AOSharp.Pathfinding;
using AOSharp.Core.Movement;
using AOSharp.Core.Misc;
using System;

namespace MalisGridNavigator
{
    public class GridNav
    {
        private readonly Dictionary<GridExit, ExitInfo> _exits = new Dictionary<GridExit, ExitInfo>()
        {
            { GridExit.Newland, new ExitInfo { Floor = Floor.Floor0, Position = new Vector3(177.8126, 3.700028, 173.0097) } },
            { GridExit.Tir, new ExitInfo { Floor = Floor.Floor0, Position = new Vector3(150.8562, 3.60001, 184.7127) } },
            { GridExit.OmniTrade, new ExitInfo { Floor = Floor.Floor0, Position = new Vector3(170.01, 3.6, 240.8323) } },
            { GridExit.Borealis, new ExitInfo { Floor = Floor.Floor0, Position = new Vector3(242.2967, 3.1, 199.4019) } },
            { GridExit.OldAthen, new ExitInfo { Floor = Floor.Floor0, Position = new Vector3(214.5266, 3.200002, 173.0108) } },
            { GridExit.ICC, new ExitInfo { Floor = Floor.Floor0, Position = new Vector3(237.5, 3.1, 217.4) } },
            { GridExit.CoP, new ExitInfo { Floor = Floor.Floor0, Position = new Vector3(201.7671, 3.099974, 253.0732) } },
            { GridExit.CoT, new ExitInfo { Floor = Floor.Floor0, Position = new Vector3(219.2315, 3.138839, 252.2457) } },
            { GridExit.UniDefHub, new ExitInfo { Floor = Floor.Floor0, Position = new Vector3(208.2991, 3.17329, 217.6294) } },

            { GridExit.HO2, new ExitInfo { Floor = Floor.Floor1, Position = new Vector3(189.1437, 36.5, 207.9386) } },
            { GridExit.BrokenShores, new ExitInfo { Floor = Floor.Floor1, Position = new Vector3(243.4243, 36.50009, 221.5573) } },
            { GridExit.FourHoles, new ExitInfo { Floor = Floor.Floor1, Position = new Vector3(196.8505, 36.49997, 204.6926) } },
            { GridExit.Clondyke, new ExitInfo { Floor = Floor.Floor1, Position = new Vector3(206.7348, 36.50002, 226.3772) } },
            { GridExit.Galway, new ExitInfo { Floor = Floor.Floor1, Position = new Vector3(215.4685, 36.5, 229.9121) } },
            { GridExit.Athen, new ExitInfo { Floor = Floor.Floor1, Position = new Vector3(195.9248, 36.5, 182.0787)  } },
            { GridExit.Meetmedere, new ExitInfo { Floor = Floor.Floor1, Position = new Vector3(171.6873, 36.5, 168.0772) } },
            { GridExit.Harrys, new ExitInfo { Floor = Floor.Floor1, Position = new Vector3(181.2249, 36.49998, 226.8944) } },
            { GridExit.OmniEnt, new ExitInfo { Floor = Floor.Floor1, Position = new Vector3(180.561, 36.6, 244.2538) } },
            { GridExit.Rome, new ExitInfo { Floor = Floor.Floor1, Position = new Vector3(174.8858, 36.5, 245.576) } },
            { GridExit.LushHills, new ExitInfo { Floor = Floor.Floor1, Position = new Vector3(189.3578, 36.5, 239.6848) } },

            { GridExit.OmniHQ, new ExitInfo { Floor = Floor.Floor2, Position = new Vector3(229.5762, 42.79994, 231.8396) } },
            { GridExit.Camelot, new ExitInfo { Floor = Floor.Floor2, Position = new Vector3(211.8841, 42.80011, 151.4814) } },
            { GridExit.Sentinels, new ExitInfo { Floor = Floor.Floor2, Position = new Vector3(179.5188, 43.2, 150.7599) } },
        };  
        private readonly Dictionary<Floor, Dictionary<ElevatorType, List<Vector3>>> _elevators = new Dictionary<Floor, Dictionary<ElevatorType, List<Vector3>>>()
        {
            {
                Floor.Floor0,
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
                Floor.Floor1,
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
                Floor.Floor2,
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
        private NewNavmeshMovementController _navController = new NewNavmeshMovementController(true);
        private GridExit _currentPath = GridExit.Unknown;
        
        private int _distanceCheck = 5;
        private float _stopDistance = 1.35f;

        public GridNav()
        {
            Load();
            MovementController.Set(_navController);
        }

        public void SetExit(GridExit gridExitName)
        {
            _navController.Halt();
            _currentPath = gridExitName;
        }

        public void OnUpdate(object sender, float e) => UpdatePath();

        public void TeleportStarted(object sender, EventArgs e) => Unload();

        public void TeleportEnded(object sender, EventArgs e) => Load();

        private Floor GetCurrentFloor()
        {
            float playerY = DynelManager.LocalPlayer.Position.Y;
            return playerY < 34f ? Floor.Floor0 : playerY < 42f ? Floor.Floor1 : Floor.Floor2;
        }

        private Vector3 GetClosestElevator(ElevatorType type)
        {
            Vector3 playerPos = DynelManager.LocalPlayer.Position;
            return _elevators[GetCurrentFloor()][type].OrderBy(x => Vector3.Distance(x, playerPos)).FirstOrDefault();
        }

        private KeyValuePair<GridExit, ExitInfo> GetClosestExit()
        {
            Vector3 playerPos = DynelManager.LocalPlayer.Position;
            return _exits.OrderBy(x => Vector3.Distance(x.Value.Position, playerPos)).FirstOrDefault();
        }

        private void SetElevatorPath(ElevatorType type) => _navController.SetNavMeshDestination(GetClosestElevator(type));

        private void Load()
        {
            if (Playfield.ModelIdentity.Instance != PlayfieldIds.Grid)
                return;

            _navController.LoadNavmesh($"{Main.PluginDir}\\Navmeshes\\152.navmesh");

            Main.Window = new GridNavWindow("MalisGridNav", _exits, $"{Main.PluginDir}\\UI\\Windows\\MainWindow.xml");
            Main.Window.Show();
        }

        private void Unload()
        {
            if (Playfield.ModelIdentity.Instance != PlayfieldIds.Grid)
                return;

            _currentPath = GridExit.Unknown;
            _navController.Halt();
            _navController.LoadNavmesh(null);

            Main.Window.Dispose();
        }

        private void UpdatePath()
        {
            if (_currentPath == GridExit.Unknown)
                return;

            var playerPos = DynelManager.LocalPlayer.Position;
            KeyValuePair<GridExit, ExitInfo> closestExit = GetClosestExit();
            ExitInfo currExit = _exits[_currentPath];

            if (closestExit.Key != _currentPath && Vector3.Distance(playerPos, closestExit.Value.Position) < _distanceCheck)
            {
                LocalAvoidance(playerPos, closestExit, currExit);
            }
            else if (DynelManager.LocalPlayer.Velocity == 0)
            {
                SetPath((int)GetCurrentFloor(), currExit);
            }
            else if (Vector3.Distance(currExit.Position, playerPos) < _stopDistance)
            {
                _navController.Halt();
            }
        }

        private void SetPath(int currFloor, ExitInfo currExit)
        {
            if (currFloor < (int)currExit.Floor)
                SetElevatorPath(ElevatorType.Up);
            else if (currFloor > (int)currExit.Floor)
                SetElevatorPath(ElevatorType.Down);
            else
                _navController.SetNavMeshDestination(currExit.Position);
        }

        private void LocalAvoidance(Vector3 playerPos, KeyValuePair<GridExit, ExitInfo> closestExit, ExitInfo currExit)
        {
            var playerRot = DynelManager.LocalPlayer.Rotation;

            float angle = Vector3.Angle(playerRot.Forward, closestExit.Value.Position - playerPos);

            if (angle < 15)
            {
                float side = Vector3.Cross(closestExit.Value.Position - playerPos, playerRot.Forward).Y;

                Vector3 gridPathAvoid = side > 0 ? playerPos + playerRot * new Vector3(1, 0, 1) * 3f : playerPos + playerRot * new Vector3(-1, 0, 1) * 3f;

                _navController.SetPath(_navController.Pathfinder.GeneratePath(gridPathAvoid, currExit.Position));
            }
        }
    }

    public class ExitInfo
    {
        public Floor Floor;
        public Vector3 Position;
    }

    public enum Floor
    {
        Floor0,
        Floor1,
        Floor2,
        Floor3,
        Floor4,
        Floor5,
        Floor6,
        Floor7,
        Floor8,
        Floor9,
        Floor10,
    }

    public enum GridExit
    {
        Unknown,
        HO2,
        Newland,
        Tir,
        OmniTrade,
        FourHoles,
        Clondyke,
        Galway,
        OmniHQ,
        BrokenShores,
        Athen,
        Meetmedere,
        Sentinels,
        Camelot,
        Harrys,
        OmniEnt,
        Rome,
        LushHills,
        Borealis,
        OldAthen,
        ICC,
        CoP,
        CoT,
        UniDefHub
    }

    public enum ElevatorType
    {
        Up,
        Down
    }
}