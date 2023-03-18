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
    public abstract class Navigator : NewNavmeshMovementController
    {
        protected Dictionary<Floor, Dictionary<ElevatorType, List<Vector3>>> Elevators;
        protected Dictionary<GridExit, Dictionary<GridSide, GridExitInfo>> Exits;
        protected GridExitDestination CurrentExit = new GridExitDestination { GridSide = GridSide.None, GridExit = GridExit.Unknown };
        protected float StopDistance = 1.35f;
        protected int PlayfieldId;

        public Navigator(int pfId, Dictionary<GridExit, Dictionary<GridSide, GridExitInfo>> exits, Dictionary<Floor, Dictionary<ElevatorType, List<Vector3>>> elevators) : base(true)
        {
            PlayfieldId = pfId;
            Elevators = elevators;
            Exits = exits;

            Game.TeleportStarted += TeleportStarted;
            Game.TeleportEnded += TeleportEnded;

            InitLoad();
        }

        protected abstract Floor GetCurrentFloor();

        protected abstract void OnUpdate(object sender, float e);

        public new void Halt()
        {
            CurrentExit = new GridExitDestination { GridSide = GridSide.None, GridExit = GridExit.Unknown };
            base.Halt();
        }
    
        protected ElevatorType GetElevation(int currFloor, GridExitInfo currExit) => currFloor < (int)currExit.Floor ? ElevatorType.Up : currFloor > (int)currExit.Floor ? ElevatorType.Down : ElevatorType.Current;
     
        protected void SetGeneratedPath(Vector3 start, Vector3 end) => SetPath(Pathfinder.GeneratePath(start, end));

        protected GridExitInfo GetCurrentGridExitInfo(GridExitDestination currentGridExit) => Exits[currentGridExit.GridExit][currentGridExit.GridSide];

        protected void SetDestination(GridExitInfo currentExitInfo, Floor currentFloor)
        {
            var elevationType = GetElevation((int)currentFloor, currentExitInfo);

            if (elevationType == ElevatorType.Current)
                SetNavMeshDestination(currentExitInfo.Position);
            else
                SetNavMeshDestination(GetClosestElevator(currentFloor, elevationType));
        }

        public void SetCurrentExit(GridExitDestination gridExitObject)
        {
            Halt();
            CurrentExit = gridExitObject;
            Chat.WriteLine($"Destination set to: {gridExitObject.GridExit}");
        }

        private void Load()
        {
            Game.OnUpdate += OnUpdate;   
            LoadNavmesh($"{Main.PluginDir}\\Navmeshes\\{PlayfieldId}.navmesh");
            Main.Window = new GridNavWindow("MalisGridNav", Exits, $"{Main.PluginDir}\\UI\\Windows\\MainWindow.xml");
            Main.Window.Show();
            Set(this);
        }

        private void Unload()
        {
            Game.OnUpdate -= OnUpdate;
            LoadNavmesh(null);
            Main.Window.Dispose();
        }

        private Vector3 GetClosestElevator(Floor currentFloor, ElevatorType elevatorType)
        {
            Vector3 playerPos = DynelManager.LocalPlayer.Position;
            return Elevators[currentFloor][elevatorType].OrderBy(x => Vector3.Distance(x, playerPos)).FirstOrDefault();
        }

        private void InitLoad()
        {
            if (Playfield.ModelIdentity.Instance != PlayfieldId)
                return;

            Load();
        }

        private void TeleportStarted(object sender, EventArgs e)
        {
            if (Playfield.ModelIdentity.Instance != PlayfieldId)
                return;

            Halt();
            Unload();
        }

        private void TeleportEnded(object sender, EventArgs e) => InitLoad();
    }

    public class GridExitDestination
    {
        public GridSide GridSide;
        public GridExit GridExit;
    }

    public class GridExitInfo
    {
        public Floor Floor;
        public Vector3 Position;
    }

    public class ClosestGridExitInfo
    {
        public GridExit GridExit;
        public GridExitInfo GridExitInfo;
    }

    public enum Floor
    {
        Zero,
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        None,
    }

    public enum GridExit
    {
        Unknown,
        HO2,
        Newland,
        NewlandCity,
        Tir,
        OmniTrade,
        FourHoles,
        Clondyke,
        Galway,
        OmniHq,
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
        UniDefHub,

        OldAthens,
        WestAthen,
        TirCity,

        OmniForest,
        RomeBlue,
        RomeRed,
        RomeGreen,
        HolesInTheWall,
        AthenShire,
        TirCounty,

        NewlandDesert,
        GreaterOmniForest,
        LushFields,
        MutantDomain,
        StretWestBank,
        WailingWastes,
        GreaterTirCounty,

        VarmitWoods,
        GalwayCounty,
        GalwayShire,
        WartornValley,
        Aegean,

        PleasentMeadows,
        Andromeda,

        TheLongestRoad,
        MilkyWay,
        StretEastBank,

        SouthernFoulsHills,
        SouthernArteryValley,
        UpperStretEastBank,

        Avalon,
        BelialForest,
        CentralArteryValley,

        Mort,
        DeepArteryValley,

        PrepetualWastelands,
        EasternFoulsPlains,
    }

    public enum ElevatorType
    {
        Up,
        Down,
        Current
    }
}