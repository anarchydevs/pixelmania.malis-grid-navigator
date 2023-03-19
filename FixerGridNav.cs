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
    public class FixerGridNav : Navigator
    {
        public FixerGridNav(int pfId, Dictionary<GridExit, Dictionary<GridSide, GridExitInfo>> exits, Dictionary<Floor, Dictionary<ElevatorType, List<Vector3>>> elevators) : base(pfId, exits, elevators) 
        {
            StopDistance = 1f;
        }

        protected override void OnUpdateGn(object sender, float e)
        {
            if (CurrentExit.GridExit == GridExit.Unknown)
                return;

            var playerPos = DynelManager.LocalPlayer.Position;
            GridExitInfo currExitInfo = GetCurrentGridExitInfo(CurrentExit);

            if (DynelManager.LocalPlayer.Velocity == 0)
            {
                SetDestination(currExitInfo, GetCurrentFloor());
            }
            else if (Vector3.Distance(currExitInfo.Position, playerPos) < StopDistance)
            {
                Halt();
            }
        }

        protected override Floor GetCurrentFloor() => (Floor)((int)(DynelManager.LocalPlayer.Position.Y / 10));
    }

    public enum GridSide
    {
        None,
        Left,
        Middle,
        Right
    }
}
