using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheWumpus
{
    class Program
    {
        static int[,] adjacentRooms; // adjacentRooms rectangular array. See CreateCave() for initialization.
        static int numRooms, currentRoom; // currentRoom is an integer variable that stores the room the player is currently in (between 0-20)
        static int wumpusRoom, batRoom1, batRoom2, pitRoom1, pitRoom2; // Stores the room numbers of the respective hazards
        static bool playerAlive, wumpusAlive; // Are the player and wumpus still alive? True or false.
        static Random random = new Random(); // Our random number generator 

        // This method creates or builds the cave network, mainly initalizing the adjacentRooms array.
        // This is where you will add support for new types of caves.
        static void CreateCave()
        {
            // The adjacentRooms rectangular array is the core of the Dodecahedron cave. 
            // It is a 2D array that lists each room and which rooms are adjacent to that room.
            // It essentially encodes the design of the cave into an array.
            // For example, the first element of adjacentRooms states that room 0 is adjacent to rooms 1, 4, 7. Next room 1 is adjacent to rooms 0, 2, and 9.
            // If you look at the dodecahedron cave design picture in the instructions you will see how those numbers map to that design.
            adjacentRooms = new int[,]         
            {
               {1, 4, 7},   {0, 2, 9},   {1, 3, 11},   {2, 4, 13},    {0, 3, 5},
              {4, 6, 14},  {5, 7, 16},    {0, 6, 8},   {7, 9, 17},   {1, 8, 10},
             {9, 11, 18}, {2, 10, 12}, {11, 13, 19},  {3, 12, 14},  {5, 13, 15},
            {14, 16, 19}, {6, 15, 17},  {8, 16, 18}, {10, 17, 19}, {12, 15, 18}
            };

            // Save the total number of rooms in a more user-friendly variable name
            numRooms = adjacentRooms.GetLength(0);  
        }

        // This method places the wumpus in the cave
        // It randomly picks a room (except room 0) and sets wumpusRoom to that value.
        static void PlaceWumpus()
        {
            wumpusRoom = random.Next(1, numRooms);
            Console.WriteLine("DEBUG: Wumpus detected in room {0}", wumpusRoom);
        }

        // This method returns true if roomB is adjacent to roomA, otherwise returns false.
        // It is a helper method that loops through the adjacentRooms array to check. 
        // It will be used throughout the app to check if we are next to the wumpus, bats, or pits
        // as well as check if we can make a valid move.
        static bool IsRoomAdjacent(int roomA, int roomB)
        {
            for (int j = 0; j < adjacentRooms.GetLength(1); j++)
            {
                if (adjacentRooms[roomA, j] == roomB) return true;
            }
            return false;
        }

        // This is a  method that checks if the user inputted a valid room to move to or not.
        // The room number has to be between 0 and 20, but also must be adjacent to the current room.
        static bool IsValidMove(int roomID)
        {
            if (roomID < 0) return false;
            if (roomID > numRooms) return false;
            if (!IsRoomAdjacent(currentRoom, roomID)) return false;

            return true;     
        }

        // This method moves the player to a new room and returns the new room. It performs no checks on its own.
        static int Move(int newRoom)
        {
            return newRoom;
        }

        // Inspects the current room. 
        // This method should check for Hazards such as being in the same room as the wumpus, bats, or pits
        // It must also check if you are adjacent to a hazard and handle those cases
        // Finally it will just print out the room description
        static void InspectCurrentRoom()
        {
            if (currentRoom == wumpusRoom)
            {
                Console.WriteLine("The Wumpus ate you!!!");
                playerAlive = false;
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("You are in room {0}", currentRoom);
                if (IsRoomAdjacent(currentRoom, wumpusRoom)) Console.WriteLine("You smell a horrid stench...");

                Console.Write("Tunnels lead to rooms ");
                for (int j = 0; j < adjacentRooms.GetLength(1); j++)
                {
                    Console.Write("{0} ", adjacentRooms[currentRoom, j]);
                }
                Console.WriteLine();
            }
        }

        // Method accepts a text string which is the command the user inputted.
        // This method performs the action of the command or prints out an error.
        static void PerformAction(string cmd)
        {
            int newRoom;
            switch (cmd.ToLower())
            {
                case "move":
                    Console.Write("Which room? ");
                    try
                    {
                        newRoom = Convert.ToInt32(Console.ReadLine());
                        // Check if the user inputted a valid room id, then simply tell the player to move there.
                        if (IsValidMove(newRoom))
                        {
                            currentRoom = Move(newRoom);
                            InspectCurrentRoom();
                        }
                        else
                        {
                            Console.WriteLine("You cannot move there.");
                        }
                    }
                    catch (FormatException) // Try...Catch block will catch if the user inputs text instead of a number.
                    {
                        Console.WriteLine("You cannot move there.");
                    }
                    break;
                default:
                    Console.WriteLine("You cannot do that. You can move, shoot, or quit.");
                    break;
            }
        }

        // PlayGame() method starts up the game.
        // It houses the main game loop and when PlayGame() quits the game has ended.
        static void PlayGame()
        {
            // We are about to start the game. 
            string cmd;

            Console.WriteLine("Running the game...");

            // Perform initialization tasks at the beginning of every game
            CreateCave();   // Create the cave network.
            PlaceWumpus();  // Place the wumpus in a room
            // Task 2, Task 3 Hint. Maybe we need PlaceBats() and PlacePits()?

            // Place the player in room 0 and inspect that room to get started.
            playerAlive = true;
            wumpusAlive = true;
            currentRoom = Move(0);
            InspectCurrentRoom();

            // Main game loop.
            // 1) Prompt the user for some input
            // 2) Perform the action the user inputted
            // 3) Check if the game is over or not and keep looping.
            while (playerAlive && wumpusAlive)
            {
                Console.Write(">>> ");
                cmd = Console.ReadLine();
                PerformAction(cmd);
            }
        }

        static void PrintInstructions()
        {
            // TODO: Display some instructions here.
        }

        // ViewHighScores(). This method should read the high score file from disk and display its contents.
        static void ViewHighScores()
        {
            // TODO: Task 8
        }

        static void Main(string[] args)
        {
            int choice;
            bool validChoice;
            bool keepPlaying;

            // The purpose of the outer do...while loop is when the game ends, we will bring the user back
            // to the main menu, so they can start a new game, view scores, view instructions, or really quit.
            do
            {
                keepPlaying = true;
                Console.WriteLine("Welcome to Hunt The Wumpus.");
                Console.WriteLine("1) New Game");
                Console.WriteLine("2) Print Instructions");
                Console.WriteLine("3) View High Scores");
                Console.WriteLine("4) Quit");

                do // inner do...while loop is to keep looping until the user picks a valid menu selection
                {
                    validChoice = true;
                    Console.Write("Please make a selection: ");
                    try
                    {
                        choice = Convert.ToInt32(Console.ReadLine());
                        switch (choice)
                        {
                            case 1: // User selected New Game
                                PlayGame();
                                break;
                            case 2: // User selected Print Instructions
                                PrintInstructions();
                                break;
                            case 3: // User selected View High Score List
                                ViewHighScores();
                                break;
                            case 4: // User selected Quit
                                Console.WriteLine("Quitting.");
                                keepPlaying = false;
                                break;
                            default:
                                validChoice = false;
                                Console.WriteLine("Invalid choice. Please try again.");
                                break;
                        }
                    }
                    catch (FormatException)
                    {
                        // This try...catch block catches the FormatException that Convert.ToInt32 will throw 
                        // if the user inputs text or something that cannot be converted to an integer.
                        validChoice = false;
                        Console.WriteLine("Invalid choice. Please try again.");
                    }
                } while (validChoice == false); // Inner loop ends when validChoice is true
            } while (keepPlaying); // Outer loop ends when the user selects quit.
        }
    }
}