﻿using BattleshipLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleshipLibrary
{
    public static class GameLogic
    {
        public static object GetShotCount(PlayerInfo player)
        {
            int shotCount = 0;
            foreach (var shot in player.ShotGrid)
            {
                if (shot.Status != GridSpotStatus.Empty)
                {
                    shotCount++;
                }

            }
            return shotCount;
        }

        public static bool IdentifyShotResult(PlayerInfo opponent, string row, int column)
        {
            bool isAHit = false;
            foreach (var ship in opponent.ShipLocation)
            {
                if (ship.SpotLetter == row.ToUpper() && ship.SpotNumber == column)
                {
                    isAHit = true;
                }
            }
            return isAHit;
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
                        Status = GridSpotStatus.Empty,

                    };
                    model.ShotGrid.Add(spot);
                }
            }
        }

        public static void MarkShotResult(PlayerInfo activePlayer, string row, int column, bool isAHit)
        {
            foreach (var gridSpot in activePlayer.ShotGrid)
            {
                if (gridSpot.SpotLetter == row.ToUpper() && gridSpot.SpotNumber == column)
                {
                    if (isAHit)
                    {
                        gridSpot.Status = GridSpotStatus.Hit;
                    }
                    else
                    {
                        gridSpot.Status = GridSpotStatus.Miss;
                    }
                }
            }
        }

        public static bool PlaceShip(PlayerInfo model, string location)
        {
            bool output = false;
            (string row, int column) = SplitShotIntoRowAndColumn(location);

            bool isValidLocation = ValidateShipLocation(model, row, column);
            bool isSpotOpen = ValidateGridLocation(model, row, column);

            if (isValidLocation && isSpotOpen)
            {
                model.ShipLocation.Add(new GridSpot
                {
                    SpotLetter = row.ToUpper(),
                    SpotNumber = column,
                    Status = GridSpotStatus.Ship
                });
                output = true;
            }
            return output;
        }

        private static bool ValidateShipLocation(PlayerInfo model, string row, int column)
        {
            bool isValidLocation = true;
            foreach (var ship in model.ShipLocation)
            {
                if (ship.SpotLetter == row.ToUpper() && ship.SpotNumber == column)
                {
                    isValidLocation = false;
                }
            }
            return isValidLocation;
        }

        private static bool ValidateGridLocation(PlayerInfo model, string row, int column)
        {
            bool isValidShot = false;
            foreach (var shot in model.ShotGrid)
            {
                if (shot.SpotLetter == row.ToUpper() && shot.SpotNumber == column)
                {
                    isValidShot = true;
                }
            }
            return isValidShot;
        }

        public static bool PlayerStillActive(PlayerInfo player)
        {
            bool isActive = false;
            foreach (var ship in player.ShipLocation)
            {
                if (ship.Status != GridSpotStatus.Sunk)
                {
                    isActive = true;
                }
            }
            return isActive;
        }

        public static (string row, int column) SplitShotIntoRowAndColumn(string shot)
        {
            string row = "";
            int column = 0;
            if (shot.Length != 2)
            {
                throw new ArgumentException("This was an invalid shot", "shot");
            }
            char[] shotArray = shot.ToArray();

            row = shotArray[0].ToString();
            column = int.Parse(shotArray[1].ToString());

            return (row, column);
        }

        public static bool ValidateShot(PlayerInfo player, string row, int column)
        {
            bool isValidShot = false;
            foreach (var gridSpot in player.ShotGrid)
            {
                if (gridSpot.SpotLetter == row.ToUpper() && gridSpot.SpotNumber == column)
                {
                    if (gridSpot.Status == GridSpotStatus.Empty)
                    {
                        isValidShot = true;
                    }
                }
            }
            return isValidShot;
        }
    }
}
