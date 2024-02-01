using BattleshipLibrary;
using BattleshipLibrary.Models;
using System;

namespace ConsoleUI
{
    public class Program
    {
        static void Main(string[] args)
        {
            GreetMessage();

            PlayerInfo activePlayer = CreatePlayer("Player 1");
            PlayerInfo opponent = CreatePlayer("Player 2");
            PlayerInfo winner = null;

            do
            {
                // Display grid from activePlayer on where they fired
                DisplayShotGrid(activePlayer);

                // Ask the activePlayer for a shot
                // Determine if it is a valid shot
                // Determine shot results
                RecordPlayerShot(activePlayer, opponent);

                // Determine if the game is over
                bool doesGameContinue = GameLogic.PlayerStillActive(opponent);

                // If over, set activePlayer as the winner
                // else, swap positions (activePlayer to opponent) 
                if (!doesGameContinue)
                {
                    winner = activePlayer;
                }
                else
                {
                    (activePlayer, opponent) = (opponent, activePlayer);
                }
            } while (winner == null);

            IdentifyWinner(winner);

            Console.ReadLine();
        }

        private static void IdentifyWinner(PlayerInfo winner)
        {
            Console.WriteLine($"Congratulations to {winner.UserName} for winning");
            Console.WriteLine($"{winner.UserName} took {GameLogic.GetShotCount(winner)} shots");
        }

        private static void RecordPlayerShot(PlayerInfo activePlayer, PlayerInfo opponent)
        {
            bool isValidShot = false;
            string row = "";
            int column = 0;

            do
            {
                // ask for a shot (we ask for "B2")
                string shot = AskForShot(activePlayer);
                try
                {
                    // determine what row and column that is - split it apart
                    (row, column) = GameLogic.SplitShotIntoRowAndColumn(shot);
                    // determine if that is a valid spot
                    // go back to the beginning if not a valid shot
                    isValidShot = GameLogic.ValidateShot(activePlayer, row, column);
                }
                catch (Exception)
                {
                    isValidShot = false;
                }

                // error message if not valid shot
                if (!isValidShot)
                {
                    Console.WriteLine("Invalid shot location. Please try again");
                }
            } while (!isValidShot);

            // determine shot results
            bool isAHit = GameLogic.IdentifyShotResult(opponent, row, column);
            // record  results
            GameLogic.MarkShotResult(activePlayer, row, column, isAHit);

            DisplayResult(row, column, isAHit);
        }


        private static void DisplayResult(string row, int column, bool isAHit)
        {
            if (!isAHit)
            {
                Console.WriteLine($"{row.ToUpper()}{column} is a miss.");
            }
            else
            {
                Console.WriteLine($"{row.ToUpper()}{column} is a Hit!");
            }
            Console.WriteLine();
        }
        private static string AskForShot(PlayerInfo player)
        {
            Console.Write($"{player.UserName}, please enter your shot selection: ");
            string output = Console.ReadLine();
            return output;
        }

        private static void DisplayShotGrid(PlayerInfo activePlayer)
        {
            string currentRow = activePlayer.ShotGrid[0].SpotLetter;

            foreach (var gridSpot in activePlayer.ShotGrid)
            {
                if (gridSpot.SpotLetter != currentRow)
                {
                    Console.WriteLine();
                    currentRow = gridSpot.SpotLetter;
                }

                if (gridSpot.Status == GridSpotStatus.Empty)
                {
                    Console.Write($" {gridSpot.SpotLetter}{gridSpot.SpotNumber} ");
                }
                else if (gridSpot.Status == GridSpotStatus.Hit)
                {
                    Console.Write(" X  ");
                }
                else if (gridSpot.Status == GridSpotStatus.Miss)
                {
                    Console.Write(" O  ");
                }
                else
                {
                    Console.Write(" ?  ");
                }
            }
            Console.WriteLine();
        }

        private static void GreetMessage()
        {
            Console.WriteLine("Welcome to Battleship Lite");
            Console.WriteLine("Created by Luqman Hafizi");
            Console.WriteLine();
        }
        private static PlayerInfo CreatePlayer(string playerNumber)
        {
            PlayerInfo output = new PlayerInfo();
            // Ask for player name
            Console.WriteLine($"{playerNumber}");
            output.UserName = AskPlayerName();
            // Load up shot grid
            GameLogic.InitializeGrid(output);
            // Ask for ship placement
            // Determine whethe it is a valid spot
            // Store the data 
            PlaceShips(output);
            // Clear Screen
            Console.Clear();

            return output;
        }
        private static string AskPlayerName()
        {
            Console.Write($"Insert your name: ");
            string output = Console.ReadLine();
            return output;
        }
        private static void PlaceShips(PlayerInfo player)
        {
            Console.WriteLine("Insert a Grid Number for ship to be placed");
            do
            {
                Console.Write($"Ship number {player.ShipLocation.Count + 1}: ");
                string location = Console.ReadLine();
                bool isValidAnswer = false;
                try
                {
                    isValidAnswer = GameLogic.PlaceShip(player, location);
                }
                catch (Exception)
                {
                }
                if (!isValidAnswer)
                {
                    Console.WriteLine("Please insert valid Grid Number");
                }
            } while (player.ShipLocation.Count < 5);
        }
    }
}
