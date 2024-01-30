using BattleshipLibrary.Models;
using System;
using System.Collections.Generic;

namespace BattleshipLibrary
{
    public static class GameLogic
    {
        public static object GetShotCount(PlayerInfo winner)
        {
            throw new NotImplementedException();
        }

        public static void InitializeGrid(PlayerInfo model)
        {
            List<string> letter = new List<string>()
            {
                "A",
                "B",
                "C",
                "D",
                "E"
            };
            List<int> number = new List<int>()
            {
                1,
                2,
                3,
                4,
                5,
            };
            for (int i = 0; i < letter.Count; i++)
            {
                for (int j = 0; j < number.Count; j++)
                {
                    GridSpot spot = new GridSpot()
                    {
                        SpotLetter = letter[i],
                        SpotNumber = number[j],
                        Status = GridSpotStatus.Empty
                    };
                    model.ShotGrid.Add(spot);
                }
            }
        }

        public static bool PlaceShip(PlayerInfo player, string location)
        {
            throw new NotImplementedException();
        }

        public static bool PlayerStillActive(PlayerInfo opponent)
        {
            throw new NotImplementedException();
        }
    }
}
