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
        static int savedWumpusRoom, savedBatRoom1, savedBatRoom2, savedPitRoom1, savedPitRoom2; // Stores the room numbers of the respective hazards
        static int goldBarRoom; //stores the location of the gold bar -- FYI this is different every game, regardless of whether the same map is played
        static bool playerAlive, wumpusAlive; // Are the player and wumpus still alive? True or false.
        static int scoreTotal, turnTotal; // This variable keeps a running total 
        static int caveChoice;
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

        static void CreateCave2() // We are going to create a string of beads
        {
            adjacentRooms = new int[,]
            {
               {1, 2, 19},   {0, 2, 3},   {0, 1, 3},   {1, 2, 4},    {3, 5, 6},
              {4, 6, 7},  {5, 4, 7},    {5, 6, 8},   {7, 9, 10},   {8, 10, 11},
             {8, 9, 11}, {9, 10, 12}, {11, 13, 14},  {12, 14, 15},  {12, 13, 15},
            {13, 14, 16}, {15, 17, 18},  {16, 18, 19}, {16, 17, 19}, {0, 17, 18}
            };

            // Save the total number of rooms in a more user-friendly variable name
            numRooms = adjacentRooms.GetLength(0);
        }

        static void CreateCave3() // We are going to create the mobius strip
        {
            adjacentRooms = new int[,]
            {
               {1, 2, 19},   {0, 3, 18},   {0, 3, 4},   {1, 2, 5},    {2, 5, 6},
              {3, 4, 7},  {4, 7, 8},    {5, 6, 9},   {6, 9, 10},   {7, 8, 11},
             {8, 11, 12}, {9, 10, 13}, {10, 13, 14},  {11, 12, 15},  {12, 15, 16},
            {13, 14, 17}, {14, 17, 18},  {15, 16, 19}, {1, 16, 19}, {0, 17, 18}
            };

            // Save the total number of rooms in a more user-friendly variable name
            numRooms = adjacentRooms.GetLength(0);
        }

        static void CreateCave4() // We are going to create the Toroidal Hex 
        {
            adjacentRooms = new int[,]
            {
               {5, 9, 15},   {5, 6, 16},   {6, 7, 17},   {7, 8, 18},    {8, 9, 19 },
              {0, 1, 14},  {1, 2, 10},    {2, 3, 11},   {3, 4, 12},   {0, 4, 13},
             {6, 15, 19}, {7, 15, 16}, {8, 16, 17},  {9, 17, 18},  {5, 18, 19},
            {0, 10, 11}, {1, 11, 12},  {2, 12, 13}, {3, 13, 14}, {4, 10, 14}
            };

            // Save the total number of rooms in a more user-friendly variable name
            numRooms = adjacentRooms.GetLength(0);
        }

        // This method places the wumpus in the cave
        // It randomly picks a room (except room 0) and sets wumpusRoom to that value.
        static void PlaceWumpus()
        {
            wumpusRoom = random.Next(1, numRooms);
            savedWumpusRoom = wumpusRoom; // 
            //Console.WriteLine("DEBUG: Wumpus detected in room {0}", wumpusRoom);
        }

        static void ViewHighScores()
        {
            //System.IO.File.AppendAllText("{0}\n", scoreTotal);

            string highScore = Convert.ToString(scoreTotal);
            highScore += "\n";
            System.IO.File.AppendAllText(@"WriteText.txt", highScore);
            //Now we will show the high scores

            string readScore = System.IO.File.ReadAllText(@"WriteText.txt");
        // Display the file contents to the console. Variable text is a string.     
        System.Console.WriteLine("High Scores:\n{0}", readScore);

        }
        
        static void PlaySameMap() //THis is task 5 - play again
        {
            wumpusRoom = savedWumpusRoom;
            batRoom1 = savedBatRoom1;
            batRoom2 = savedBatRoom2;
            pitRoom1 = savedPitRoom1;
            pitRoom2 = savedPitRoom2;
            PlaceGoldBar(); // the gold bar gets a new location!
            Console.WriteLine("Launching Saved Wumpus Game");
            //Console.WriteLine("DEBUG: Launching Saved Wumpus Game - Wumpus detected in room {0}", wumpusRoom);
            //Console.WriteLine("DEBUG: Launching Saved Wumpus Game - Bats placed in first bat room {0}", batRoom1);
            //Console.WriteLine("DEBUG: Launching Saved Wumpus Game - Bats placed in second bat room {0}", batRoom2);
            //Console.WriteLine("DEBUG: Launching Saved Wumpus Game -  Bottomless pit detected in first pit room {0}", pitRoom1);
            //Console.WriteLine("DEBUG: Launching Saved Wumpus Game - Bottomless pit detected in second pit room {0}", pitRoom2);
            //Console.WriteLine("DEBUG: Launching Saved Wumpus Game - New Location of the Gold Bar {0}", goldBarRoom );
            /*challenge - I was going through the debugging process on this method and I initially didn't move the player back to room 0. 
            During a subsequent test the wumpus ate me and I was surprised to find that the moment I
            started the game over again that I instantly eaten by the Wumpus! */
            scoreTotal = 0; // here we bring the players score back to zero
            turnTotal = 0; // here we start the counting of the total turns back to zero.
            currentRoom = Move(0);
            InspectCurrentRoom();
            //CheckforGoldBar();
        }


        static void PlaceBats()
        {
            //Random number generator finds rooms for our bats to go in
            batRoom1 = random.Next(1, numRooms);
            batRoom2 = random.Next(1, numRooms);

            //now we make sure that the two bat rooms are not the same room and that neither bat room is a wumpus room
            if (batRoom1 == batRoom2 || wumpusRoom == batRoom1 || wumpusRoom == batRoom2)
            {
                batRoom1 = random.Next(1, numRooms);
                batRoom2 = random.Next(1, numRooms);
                //Console.WriteLine("DEBUG: Bats placed in first bat room {0}", batRoom1);
                //Console.WriteLine("DEBUG: Bats placed in second bat room {0}", batRoom2);
            }
            else
            {
                //trace messages telling us where the bat rooms ended up
                //Console.WriteLine("DEBUG: Bats placed in first bat room {0}", batRoom1);
                //Console.WriteLine("DEBUG: Bats placed in second bat room {0}", batRoom2);
            }
            savedBatRoom1 = batRoom1;
            savedBatRoom2 = batRoom2;
        }

        static void PlacePits()
        {
            pitRoom1 = random.Next(1, numRooms);
            pitRoom2 = random.Next(1, numRooms);

            if (pitRoom1 == pitRoom2)
            {
                //if both pit room random numbers are the same then we will run the random number thingy on pit room 2 until it becomes a different room
                pitRoom2 = random.Next(1, numRooms);
                //Console.WriteLine("DEBUG: Bottomless pit detected in first pit room {0}", pitRoom1);
                //Console.WriteLine("DEBUG: Bottomless pit detected in second pit room {0}", pitRoom2);
            }
            else
            {
                //Console.WriteLine("DEBUG: Bottomless pit detected in first pit room {0}", pitRoom1);
                //Console.WriteLine("DEBUG: Bottomless pit detected in second pit room {0}", pitRoom2);
            }
            savedPitRoom1 = pitRoom1;
            savedPitRoom2 = pitRoom2;
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

            //CheckForGoldBar();
            if (currentRoom == wumpusRoom)
            {
                Console.WriteLine("The Wumpus ate you!!!");
                //playerAlive = false;
                QuitGame();
            }
         

            {
                if (currentRoom == pitRoom1 || currentRoom == pitRoom2)
                {
                    Console.WriteLine("YYYIIIIEEEE…You fell in a pit.");
                    Console.WriteLine("Lose 45 points");
                    scoreTotal = -45;
                    //playerAlive = false;
                    QuitGame();
                }
                else if (currentRoom == batRoom1 || currentRoom == batRoom2)
                {
                    Console.WriteLine("-----------------------------------------");
                    Console.WriteLine("You are in one of the bat rooms");
                    Console.WriteLine("-----------------------------------------");
                    currentRoom = random.Next(1, numRooms);
                    Console.WriteLine("You are being move to this room {0}", currentRoom);
                    InspectCurrentRoom(); 
                }

                else if ((currentRoom == pitRoom1 || currentRoom == pitRoom2) && (currentRoom == batRoom1 || currentRoom == batRoom2))
                {
                    Console.WriteLine("You are in a bottomless pit room with bats (who will move you now to another room");
                    currentRoom = random.Next(1, numRooms);
                    Console.WriteLine("Snatched by superbats! (You are being move to this room {0})", currentRoom);
                    InspectCurrentRoom();
                }
              

                else

                {
                    Console.WriteLine();
                    Console.WriteLine("You are in room {0}", currentRoom);
                    Console.WriteLine("----------------------------------------");
                    if (IsRoomAdjacent(currentRoom, wumpusRoom)) Console.WriteLine("You smell a horrid stench...");
                    if (IsRoomAdjacent(currentRoom, batRoom1)) Console.WriteLine("Bats nearby..."); //(DEBUG:batroom1)
                    if (IsRoomAdjacent(currentRoom, batRoom2)) Console.WriteLine("Bats nearby..."); // (DEBUG:batroom2)
                    if (IsRoomAdjacent(currentRoom, pitRoom1)) Console.WriteLine("You feel a draft…"); //  (DEBUG:pitroom1)
                    if (IsRoomAdjacent(currentRoom, pitRoom2)) Console.WriteLine("You feel a draft…"); // (DEBUG:pitroom2)
                    if (currentRoom == goldBarRoom)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("You found the Luminous Gold Bar!! Lucky you");
                        Console.WriteLine("You get an extra 222 points added to your score");
                        Console.WriteLine();
                        Console.ResetColor();
                        scoreTotal += 222;
                        goldBarRoom = 100;
                    }
                    Console.Write("Tunnels lead to rooms ");
                    for (int j = 0; j < adjacentRooms.GetLength(1); j++)
                    {
                        Console.Write("{0} ", adjacentRooms[currentRoom, j]);
                    }
                    Console.WriteLine();
                    Console.WriteLine("----------------------------------------");
                    turnTotal++;
                    scoreTotal += 13;
                    //Console.WriteLine("DEBUG: This is turn number {0}", turnTotal);
                    //Console.WriteLine("DEBUG: Your current Score is {0}", scoreTotal);
                    Console.WriteLine("What do you want to do (type move, shoot or quit)?");
                    Console.WriteLine();
                }
                
            }
        }
        //This method places the gold bar 
        //gold bar gets a new location regardless of whether its a fresh game or if the same map is being played.
        static void PlaceGoldBar()
        {
            goldBarRoom = random.Next(1, numRooms);


            if (goldBarRoom == pitRoom1 || goldBarRoom == pitRoom1 || goldBarRoom == wumpusRoom)
            {
                //The gold bar may be appear in either of the batrooms, but it won't appear in the Wumpus Room, and it won't be in either of the pitrooms
                goldBarRoom = random.Next(1, numRooms);
                //Console.WriteLine("DEBUG:Location of the Gold Bar {0}", goldBarRoom);
            }
            else
            {
                //Console.WriteLine("DEBUG:Location of the Gold Bar {0}", goldBarRoom);
            }

        }

        static void CheckForGoldBar()
        {
            if (currentRoom == goldBarRoom)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("You found the Luminous Gold Bar!! Lucky you");
                Console.WriteLine("You get an extra 222 points added to your score");
                Console.WriteLine();
                Console.ResetColor();
                scoreTotal += 222;
            }

        }

        static void ShootIntoRoom()
        {
            if (currentRoom == wumpusRoom)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ARGH...Splat");
                Console.WriteLine();
                Console.ResetColor();
                Console.WriteLine("Congratulations. You killed the Wumpus! You Win!");
                scoreTotal += 100;
                Console.WriteLine("You got an extra 100 points for Killing that Wumpus"); //TRACE COMMMENT TO BE REMOVED/commented out
                if (turnTotal <= 10) { scoreTotal += 100; Console.WriteLine("You only took {0} turns to kill the Wumpus! Bonus - add 100 points", turnTotal);}
                if (turnTotal > 10) { scoreTotal += -30; Console.WriteLine("You took {0} turns - too many turns for a skilled warrior to kill the Wumpus - subtract 30 points", turnTotal);}
                //playerAlive = false;
                QuitGame();
            }
            else //We need to change this bit to invoke the move startled wumpus method
            {
                Console.WriteLine();
                Console.WriteLine("Miss! But you startled the Wumpus");
                scoreTotal -= 45;
                Console.WriteLine("You lost 45 points for missing that Wumpus");
                MoveStartledWumpus();
                InspectCurrentRoom();
                //CheckForGoldBar();
            }
        }

        static void MoveStartledWumpus()
        {
            wumpusRoom = random.Next(1, numRooms);
            //Console.WriteLine("DEBUG: Wumpus has been moved to a new room {0}", wumpusRoom);
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
                            //scoreTotal += 12;
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
                case "shoot":
                    Console.Write("Which room? ");
                    try
                    {
                        newRoom = Convert.ToInt32(Console.ReadLine());
                        // Check if the user inputted a valid room id, then simply tell the player to shoot there.
                        if (IsValidMove(newRoom))
                        {
                            currentRoom = Move(newRoom);
                            ShootIntoRoom();
                        }
                        else
                        {
                            Console.WriteLine("You cannot shoot there.");
                        }
                    }
                    catch (FormatException) // Try...Catch block will catch if the user inputs text instead of a number.
                    {
                        Console.WriteLine("You cannot shoot there.");
                    }
                    break;

                case "quit":
                    QuitGame();
                    Console.WriteLine();
                    break;
                default:
                    Console.WriteLine("You cannot do that. You can move, shoot, or quit.");
                    break;



            }
        }
        // QuitGame ends the game and brings up the start menu
        static void QuitGame()
        {
            //We inform the player that the game is quitting
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Quitting...");
            Console.WriteLine("-----------------------------------------");
            //We calculate any additions or subtractions that are evaluated based on the number of turns

            //we tell the user what the final score what
            Console.WriteLine("Your Final Score was {0}", scoreTotal);
            Console.WriteLine("-----------------------------------------");
            //We then evaluate if the score is a high score


            //we will add the "high score" to the text fill by appending it...
            ViewHighScores();


            Console.WriteLine();

            Console.WriteLine("Do you want to:");
            Console.WriteLine("1) Play the same map again");
            Console.WriteLine("2) Start a totally new game");
            Console.WriteLine("3) Quit Playing..."); // This isn't called for in the instructions... but it made sense to me as an option in this menu
            Console.WriteLine("-----------------------------------------");

            int choice;
            bool validChoice;
            bool keepPlaying;

            do // inner do...while loop is to keep looping until the user picks a valid menu selection
            {
                Console.Write("-----------------------------------------");
                validChoice = true;
                Console.Write("\nPlease make a selection: ");
                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1: // User opts to play the same map again
                                //all items will resume the setting at the BEGINNING of the game
                            PlaySameMap();
                            break;
                        case 2: // User selects a totally New Game
                            ChooseGame();
                            PlayGame();
                            break;
                        case 3: // Wants to quit for real
                            NoGameQuit();
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
        }


        static void NoGameQuit()
            //this method runs if no game has been played yet...(or going to be played...) a non-option for reals quit type of thing 
        {
            Environment.Exit(0);
            Console.WriteLine("game is ending");
        }

        static void ChooseGame()
        {
            Console.WriteLine("Which cave to play:\n1) Dodecahedron of Terror \n2) String of Beads of Death \n3) Mobius Strip of Doom  \n4) Hateful Toroidal Hex");

            //bool validCaveChoice;

            int choice;
            bool validChoice;
            bool keepPlaying;

            do // inner do...while loop is to keep looping until the user picks a valid menu selection
            {
                validChoice = true;
                Console.Write("Please make a selection: ");
                try
                {
                    caveChoice = Convert.ToInt32(Console.ReadLine());
                    switch (caveChoice)
                    {
                        /*Originally I had the cave selection store as a variable 
                        (and use an if/then statement to activate... 
                        then once I got to 4 caves this seemed wildly inefficient... 
                        thus the following switch statement was born...
                        */
                        case 1: // 
                            Console.WriteLine("----------------------------------------");
                            Console.WriteLine("Dodecahedron of Terror is being created");
                            Console.WriteLine("----------------------------------------");
                            CreateCave();   // Create the cave network.

                            break;
                        case 2: // 
                            Console.WriteLine("----------------------------------------");
                            Console.WriteLine("String of Beads of Death is being created");
                            Console.WriteLine("----------------------------------------");
                            CreateCave2();   // Create the cave network.

                            break;
                        case 3: // 
                            Console.WriteLine("----------------------------------------");
                            Console.WriteLine("Mobius Strip of Doom is being created");
                            Console.WriteLine("----------------------------------------");
                            CreateCave3();   // Create the cave network.

                            break;
                        case 4: // 
                            Console.WriteLine("----------------------------------------");
                            Console.WriteLine("Hateful Toroidal Hex is being created");
                            Console.WriteLine("----------------------------------------");
                            CreateCave4();   // Create the cave network.

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

            

            PlayGame();
            Console.WriteLine("----------------------------------------");
          
        }

// PlayGame() method starts up the game.
// It houses the main game loop and when PlayGame() quits the game has ended.
        static void PlayGame()
        {
            // We are about to start the game. 
            string cmd;



            turnTotal = 0;
            PlaceWumpus();  // Place the wumpus in a room
                            // Task 2, Task 3 Hint. Maybe we need PlaceBats() and PlacePits()?

            PlaceBats();  // Places the bats in two rooms

            PlacePits();  // Places the bats in two rooms

            PlaceGoldBar();

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
            Console.WriteLine("In Hunt the Wumpus the player hunts a mysterious creature called the Wumpus.\n" +
                "The Wumpus lives deep inside a dark cave network.\n" +
                "There are several different types of cave that you can expore.\n" +
                "Game play will proceed in this way:\n" +
                "You will type: move, shoot or quit. Then the game will ask you: Which room?, after which you will type a room number" +
                "The player can move from room to room or shoot an arrow into a room hoping to hit and kill the Wumpus.\n\n" +
                "Actions such as moving or shooting each count as a turn. Killing the Wumpus in fewer turns yields a bonus.\n\n" + 

                "Other hazards lurk in this cave as well: \n" +
                "Bats can pick up and move the player to another room\n" +
                "or the player can fall into a bottomless pit.\n" +
                "The game gives clues to the location of these hazards.\n" +
                "Gameplay takes the form of exploration and deduction.\n");
            Console.WriteLine("----------------------------------------");


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
                Console.WriteLine("----------------------------------------");

                do // inner do...while loop is to keep looping until the user picks a valid menu selection
                {
                    validChoice = true;
                    Console.WriteLine("\nPlease make a selection: ");
                    Console.WriteLine("\n----------------------------------------");
                    try
                    {
                        choice = Convert.ToInt32(Console.ReadLine());
                        switch (choice)
                        {
                            case 1: // User selected New Game
                                ChooseGame();
                                PlayGame();
                                break;
                            case 2: // User selected Print Instructions
                                PrintInstructions();
                                break;
                            case 3: // User selected View High Score List
                                ViewHighScores();
                                break;
                            case 4: // User selected Quit
                                NoGameQuit();
                               //keepPlaying = false;
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