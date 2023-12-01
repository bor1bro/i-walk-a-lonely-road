using System;
using System.Media;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Input;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Linq;
using static CoursevayaCSharp.MazeGenerator;
using static CoursevayaCSharp.PathFinder;
using static CoursevayaCSharp.ScreenVisual;
using static CoursevayaCSharp.MazeFileManagement;
using static CoursevayaCSharp.Scoreboard;


namespace CoursevayaCSharp
{
    internal class Program
    {
        /// <summary>
        /// Function to play generated maze game mode
        /// </summary>
        /// <param name="Maze_Ptr"></param>
        public static void Play_Generated(List<List<char>> Maze_Ptr)
        {
            int Height = 10; //LVL1
            int Width = 15;
            //int Height = 13; //LVL2
            //int Width = 18;
            //int Height = 16; //LVL3
            //int Width = 21;

            //Generate LVL 1 maze 
            Maze_Ptr = Maze_Generate(Width, Height);
            //Play maze
            Victory = Maze_Play(Maze_Ptr);
            if (Victory)
            {
                //Generate LVL 2 maze
                Maze_Ptr = Maze_Generate(Width + 3, Height + 3);
                //Play maze
                Victory = Maze_Play(Maze_Ptr);
                if (Victory)
                {
                    //Generate LVL 3 maze
                    Maze_Ptr = Maze_Generate(Width + 6, Height + 6);
                    //Play maze
                    Victory = Maze_Play(Maze_Ptr);
                    if (Victory)
                    {
                        //TODO: let player enter his name -> save it to structure
                        //      -> save all data of player to file (name[7], time, maze)
                        //      -> output scoreboard with all victorious attempts
                        Maze_File_Save(Maze_Ptr);
                        return;
                    }
                }
            }
            //Display lose screen, let player try again or go back to the menu
            Lose_Screen();
            Key_Info = Console.ReadKey();
        }
        /// <summary>
        /// Function to play prepared maze game mode
        /// </summary>
        public static void Play_Prepared(List<List<char>> Maze_Ptr, string File_Name)
        {
            Console.ResetColor();
            Console.Clear();
            //Get maze from file
            Maze_Ptr = Maze_File_Load(Maze_Ptr, File_Name);
            //Play maze
            Victory = Maze_Play(Maze_Ptr);
            //If player has beaten the bot, display scoreboard + add result
            if (Victory)
            {
                //TODO: let player enter his name -> save it to structure
                //      -> save all data of player to file (name[7], time, maze)
                //      -> output scoreboard with all victorious attempts
            }
        }
        /// <summary>
        /// Function that enables player to choose from saved mazes and play them
        /// </summary>
        public static void Choose_Maze(List<List<char>> Maze_Ptr)
        {
            bool Exit = false;
            int File_Quad = 0;
            string[] File_Names = Load_Maze_File_Names();
            string[] File_Quad_Names = Get_Maze_Quad(File_Quad, File_Names);

            while (!Exit)
            {
                //Display prepared maze screen with quad of file names
                Maze_Choose_Screen(File_Quad_Names);

                //Wait for input
                Key_Info = Console.ReadKey();
                switch (Key_Info.Key)
                {
                    //if -> show next quad (if remaining files don't make a quad, display only the remainings)
                    case ConsoleKey.RightArrow:
                        {
                            File_Quad = File_Quad + 4;
                            //Check if quad goes beyond name array's range, if does fit it in this range  
                            if (File_Quad > File_Names.Length)
                            {
                                File_Quad = File_Quad - 4;
                            }
                            File_Quad_Names = Get_Maze_Quad(File_Quad, File_Names);
                            break;
                        }
                    //if <- show prev quad (if it's going negative, show first quad)
                    case ConsoleKey.LeftArrow:
                        {
                            File_Quad = File_Quad - 4;
                            //Check if quad goes beyond name array's range, if does fit it in this range  
                            if (File_Quad < 0)
                            {
                                File_Quad = File_Quad + 4;
                            }
                            File_Quad_Names = Get_Maze_Quad(File_Quad, File_Names);
                            break;
                        }
                    //If player inputs "1" - "4", show warning
                    //                                  //if proceed - start game,
                    //                                  //if not - let player choose different maze
                    //                                  //TODO: Add screen to validate player's decision
                    case ConsoleKey.D1:
                        {
                            //Wait for input
                            Key_Info = Console.ReadKey();
                            Click_Sound.Play();
                            if (Key_Info.Key == ConsoleKey.Enter)
                            {                                
                                Play_Prepared(Maze_Ptr, File_Quad_Names[0]);
                            }
                            if (Key_Info.Key == ConsoleKey.Escape)
                            {
                                break;
                            }
                            break;
                        }
                    case ConsoleKey.D2:
                        {
                            //Check if this quad member has any name value, if not - go back
                            if (File_Quad_Names[1] == null)
                            {
                                break;
                            }
                            //Wait for input
                            Key_Info = Console.ReadKey();
                            Click_Sound.Play();
                            if (Key_Info.Key == ConsoleKey.Enter)
                            {
                                Play_Prepared(Maze_Ptr, File_Quad_Names[1]);
                            }
                            if (Key_Info.Key == ConsoleKey.Escape)
                            {
                                break;
                            }
                            break;
                        }
                    case ConsoleKey.D3:
                        {
                            //Check if this quad member has any name value, if not - go back
                            if (File_Quad_Names[2] == null)
                            {
                                break;
                            }
                            //Wait for input
                            Key_Info = Console.ReadKey();
                            Click_Sound.Play();
                            if (Key_Info.Key == ConsoleKey.Enter)
                            {
                                Play_Prepared(Maze_Ptr, File_Quad_Names[2]);
                            }
                            if (Key_Info.Key == ConsoleKey.Escape)
                            {
                                break;
                            }
                            break;
                        }
                    case ConsoleKey.D4:
                        {
                            //Check if this quad member has any name value, if not - go back
                            if (File_Quad_Names[3] == null)
                            {
                                break;
                            }
                            //Wait for input:              
                            Key_Info = Console.ReadKey();
                            Click_Sound.Play();
                            if (Key_Info.Key == ConsoleKey.Enter)
                            {
                                Play_Prepared(Maze_Ptr, File_Quad_Names[3]);
                            }
                            if (Key_Info.Key == ConsoleKey.Escape)
                            {
                                break;
                            }
                            break;
                        }
                    //If player pressed Esc - go back to main menu
                    case ConsoleKey.Escape:
                        {
                            Click_Sound.Play();
                            Exit = true;
                            break;
                        }
                }
            }
        }
        /// <summary>
        /// Function to iterate through names of maze files
        /// </summary>
        /// <returns>Quad of names</returns>
        public static string[] Get_Maze_Quad(int Quad, string[] Names)
        {
            int Counter = 0;
            string[] Quad_Names = new string[4];
            do
            {
                Quad_Names[Counter] = Names[Quad];
                Quad++;
                Counter++;
            } while (Counter != 4 && Quad < Names.Length);
            return Quad_Names;
        }
        /// <summary>
        /// Function to display menu
        /// </summary>
        public static void Game()
        {
            //Initialize values for maze storage
            List<List<char>> Maze_Ptr = new();
            //Boolean variable for game loop condition
            bool Exit = false;

            //Display title screen
            Title_Screen();
            Click_Sound.Play();

            //In fact this is the core loop of the game
            while (!Exit)
            {
                //Draw menu
                Game_Menu_Screen();
                //Wait for input of desired option (1,2,3,4)
                Key_Info = Console.ReadKey();
                Click_Sound.Play();
                Console.Clear();
                switch (Key_Info.Key)
                {
                    //Play on generated maze
                    case ConsoleKey.D1:
                        {
                            Play_Generated(Maze_Ptr);
                            break;
                        }
                    //Play on prepared maze
                    case ConsoleKey.D2:
                        {
                            Choose_Maze(Maze_Ptr);
                            break;
                        }
                    //Show scoreboard
                    case ConsoleKey.D3:
                        {
                            //Display scoreboard
                            Scoreboard_Screen(Scoreboard_Load());
                            //Wait for player's input
                            //TODO: Add sorting:
                            //              -by player name
                            //              -by time
                            //              -by maze
                            //              -no sorting (if chosen - reverts to in-file data)
                            //      If player presses S open sorting options screen
                            //          4 options (+ Esc if don't want to choose)
                            //      When the option is chosen - go back to sorted scoreboard
                            //      Every sorting will combine with previous
                            Key_Info = Console.ReadKey();
                            //Let player leave this screen
                            if (Key_Info.Key == ConsoleKey.Escape)
                            {
                                Click_Sound.Play();
                                break;
                            }
                            break;
                        }
                    //Display game info
                    case ConsoleKey.D4:
                        {
                            while (true)
                            {
                                //Display info from file
                                Game_Info_Screen();
                                //Wait for player's input
                                Key_Info = Console.ReadKey();
                                //Let player leave this screen
                                if (Key_Info.Key == ConsoleKey.Escape)
                                {
                                    Click_Sound.Play();
                                    break;
                                }
                            }
                            break;
                        }
                    //Exit game
                    case ConsoleKey.Escape:
                        {
                            while (true)
                            {
                                //Display warning
                                Exit_Warning_Screen();
                                //Wait for input (Y/N)
                                Key_Info = Console.ReadKey();
                                //If Y exit
                                if (Key_Info.Key == ConsoleKey.Y)
                                {
                                    Exit = true;
                                    break;
                                }
                                //If N go to menu
                                if (Key_Info.Key == ConsoleKey.N || Key_Info.Key == ConsoleKey.Escape)
                                {
                                    Click_Sound.Play();
                                    break;
                                }
                            }
                            break;
                        }
                }
            }
        }
        /// <summary>
        /// Function that starts gameplay
        /// </summary>
        public static bool Maze_Play(List<List<char>> Maze_Ptr)
        {
            //Array to save elapsed time data
            List<long> Elapsed_Time = new();

            //Initialize position of player
            PathFinder Player = new();
            Position2D Player_Pos = new(1, 1);

            //Loading already generated maze
            List<List<char>> Buf_Maze_Ptr = Maze_Ptr.Maze_Copy();
            List<List<char>> Prev_Buf_Maze_Ptr = Maze_Ptr.Maze_Copy();

            Player.Init_Player(Player_Pos, Buf_Maze_Ptr);

            //Marking the time of maze completion
            var Time = Stopwatch.StartNew();
                //Displaying first frame of maze
                Console.SetWindowSize(Maze_Ptr[0].Count + 2, Maze_Ptr.Count + 1);
                Console.SetBufferSize(Maze_Ptr[0].Count + 3, Maze_Ptr.Count + 1);
                Console.SetCursorPosition(1, 0);
            Maze_Print(Maze_Ptr, 1);
            Console.SetCursorPosition(1, 0);
            //Bot_Player goes first
            while (!Player.isWin())
            {
                Player.Next_Step();
                Prev_Buf_Maze_Ptr = Maze_Print(Prev_Buf_Maze_Ptr, Buf_Maze_Ptr);
                Console.SetCursorPosition(1, 0);
                //Add delay in bots movement, because it's moving every tick
                Thread.Sleep(75);
            }
            //Stopping the timer and saving elapsed time data
            Time.Stop();
            Elapsed_Time.Add(Time.ElapsedMilliseconds / 1000);
            //Clearing the console to hide maze
            Console.Clear();

            //Player goes next
            //Ready screen
            Ready_Screen(Maze_Ptr);
                Key_Info = Console.ReadKey();
                if (Key_Info.Key == ConsoleKey.Escape)
                {
                    return false;
                }
                Console.ResetColor();
                Console.Clear();
                Console.SetWindowPosition(0, 0);
                //Returning console window to maze size 
                Console.SetWindowSize(Buf_Maze_Ptr[0].Count + 2, Buf_Maze_Ptr.Count + 1);
                Console.SetBufferSize(Buf_Maze_Ptr[0].Count + 3, Buf_Maze_Ptr.Count + 1);
            Click_Sound.Play();
            //Loading already generated maze
            Buf_Maze_Ptr = Maze_Ptr.Maze_Copy();

            //Return player to the start
            Player.Init_Player(Player_Pos, Buf_Maze_Ptr);
            Maze_Print(Maze_Ptr, 1);
            Console.SetCursorPosition(1, 0);
            //Marking the time of maze completion
            Time = Stopwatch.StartNew();

            Maze_Print(Maze_Ptr, 1);
            while (!Player.isWin())
            {
                Player.Player_Controller();
                Prev_Buf_Maze_Ptr = Maze_Print(Prev_Buf_Maze_Ptr, Buf_Maze_Ptr);
                Console.SetCursorPosition(1, 0);
            }
            //Stopping the timer and saving elapsed time data
            Time.Stop();
            Elapsed_Time.Add(Time.ElapsedMilliseconds / 1000);
                //Clearing the console to hide maze
                Console.ResetColor();
                Console.Clear();
            //Display elapsed time for Player and Bot_Player
            Elapsed_Time_Screen(Elapsed_Time);

            Console.ReadKey();
            Click_Sound.Play();

            Console.ResetColor();
            Console.Clear();

            if (Elapsed_Time[0] == Elapsed_Time[1])
            {
                //Maybe add tie screen
                return false;
            }

            return (Elapsed_Time[0] > Elapsed_Time[1]);
        }

        public static PlayerInfo Player_Info;
        public static ConsoleKeyInfo Key_Info;
        public static bool Victory; 
        public static SoundPlayer Click_Sound = new(@"Sounds/CLICK.wav");

        static void Main(string[] args)
        {
            Game();
            //int c = 0;
            //while (c != 14)
            //{
            //    Maze_File_Save(Maze_Generate(21, 16));
            //    c++;
            //}
        }
    }
}