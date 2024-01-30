using System.Collections.Generic;

namespace BattleshipLibrary.Models
{
    public class PlayerInfo
    {
        public string UserName { get; set; }
        public List<GridSpot> ShipLocation { get; set; } = new List<GridSpot>();
        public List<GridSpot> ShotGrid { get; set; } = new List<GridSpot>();
    }
}
