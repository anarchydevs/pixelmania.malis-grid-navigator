using AOSharp.Common.GameData;
using AOSharp.Core;
using AOSharp.Core.Movement;
using AOSharp.Core.UI;
using AOSharp.Pathfinding;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MalisGridNavigator
{
    public class GridNav : Navigator
    {
        private int _distanceCheck = 5;

        public GridNav(int pfId, Dictionary<GridExit, Dictionary<GridSide, GridExitInfo>> exits, Dictionary<Floor, Dictionary<ElevatorType, List<Vector3>>> elevators) : base(pfId, exits, elevators) { }
       
        protected override void OnUpdate(object sender, float e)
        {
            if (CurrentExit.GridExit == GridExit.Unknown)
                return;

            var playerPos = DynelManager.LocalPlayer.Position;
            ClosestGridExitInfo closestExit = GetClosestExit();
            GridExitInfo currExitInfo = GetCurrentGridExitInfo(CurrentExit);

            if (closestExit.GridExit != CurrentExit.GridExit && Vector3.Distance(playerPos, closestExit.GridExitInfo.Position) < _distanceCheck)
            {
                LocalAvoidance(playerPos, closestExit, currExitInfo);
            }
            else if (DynelManager.LocalPlayer.Velocity == 0)
            {
                SetDestination(currExitInfo, GetCurrentFloor());
            }
            else if (Vector3.Distance(currExitInfo.Position, playerPos) < StopDistance)
            {
                Halt();
            }
        }

        protected override Floor GetCurrentFloor()
        {
            var playerY = DynelManager.LocalPlayer.Position.Y;
            return playerY < 34f ? Floor.Zero : playerY < 42f ? Floor.One : Floor.Two;
        }

        private ClosestGridExitInfo GetClosestExit()
        {
            Vector3 playerPos = DynelManager.LocalPlayer.Position;
            var closestExit = Exits.OrderBy(x => Vector3.Distance(x.Value[GridSide.None].Position, playerPos)).FirstOrDefault();

            return new ClosestGridExitInfo { GridExit = closestExit.Key, GridExitInfo = closestExit.Value[GridSide.None] };
        }

        private void LocalAvoidance(Vector3 playerPos, ClosestGridExitInfo closestExit, GridExitInfo currExit)
        {
            var playerRot = DynelManager.LocalPlayer.Rotation;
            float angle = Vector3.Angle(playerRot.Forward, closestExit.GridExitInfo.Position - playerPos);

            if (angle < 15)
            {
                float side = Vector3.Cross(closestExit.GridExitInfo.Position - playerPos, playerRot.Forward).Y;
                Vector3 gridPathAvoid = side > 0 ? playerPos + playerRot * new Vector3(1, 0, 1) * 3f : playerPos + playerRot * new Vector3(-1, 0, 1) * 3f;

                SetGeneratedPath(gridPathAvoid, currExit.Position);
            }
        }
    }
}