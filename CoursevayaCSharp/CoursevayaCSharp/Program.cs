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


namespace CoursevayaCSharp
{
    internal class Program
    {
        
        //Function to play generated maze game mode
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
            Victory = Maze_Setup(Maze_Ptr);
            if (Victory)
            {
                //Generate LVL 2 maze
                Maze_Ptr = Maze_Generate(Width + 3, Height + 3);
                //Play maze
                Victory = Maze_Setup(Maze_Ptr);
                if (Victory)
                {
                    //Generate LVL 3 maze
                    Maze_Ptr = Maze_Generate(Width + 6, Height + 6);
                    //Play maze
                    Victory = Maze_Setup(Maze_Ptr);
                    if (Victory)
                    {
                        //Output scoreboard + add result to it + (maybe save maze to the file)
                    }
                }
            }
            else
            {
                //Display lose screen, let player get back to the menus
            }
        }

        public static void Play_Prepared(List<List<char>> Maze_Ptr)
        {
            //Get maze from file

            //Play maze
            Victory = Maze_Setup(Maze_Ptr);
            //If player has beaten the bot, display scoreboard + add result
            if (Victory)
            { 
                
            }
        }
        //Function to display menu
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
                            Play_Prepared(Maze_Ptr);
                            break;
                        }
                    //Display game info
                    case ConsoleKey.D3:
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
                                if (Key_Info.Key == ConsoleKey.N)
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
        //Function that starts gameplay
        public static bool Maze_Setup(List<List<char>> Maze_Ptr)
        {            
            //Array to save elapsed time data
            List<long> Elapsed_Time = new();

            //Initialize position of player
            PathFinder Player = new();
            Position2D Player_Pos = new(1, 1);

            //Loading already generated maze
            List<List<char>> Buf_Maze_Ptr = Maze_Ptr.Maze_Load();
            List<List<char>> Prev_Buf_Maze_Ptr = Maze_Ptr.Maze_Load();

            Player.Init_Player(Player_Pos, Buf_Maze_Ptr);

            //Marking the time of maze completion
            var Time = Stopwatch.StartNew();
            //Displaying first frame of maze
            Console.SetWindowSize(Maze_Ptr[0].Count + 2, Maze_Ptr.Count + 1);
            Console.SetBufferSize(Maze_Ptr[0].Count + 3, Maze_Ptr.Count + 1);
            Console.SetCursorPosition(1, 0);
            Maze_Print(Maze_Ptr);
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
            Click_Sound.Play();
            //Loading already generated maze
            Buf_Maze_Ptr = Maze_Ptr.Maze_Load();

            //Return player to the start
            Player.Init_Player(Player_Pos, Buf_Maze_Ptr);
            Maze_Print(Maze_Ptr);
            Console.SetCursorPosition(1, 0);
            //Marking the time of maze completion
            Time = Stopwatch.StartNew();

            Maze_Print(Buf_Maze_Ptr);
            while (!Player.isWin())
            {
                Player.Player_Controller();
                Prev_Buf_Maze_Ptr = Maze_Print(Prev_Buf_Maze_Ptr, Buf_Maze_Ptr);
                Console.SetCursorPosition(1,0);
            }
            //Stopping the timer and saving elapsed time data
            Time.Stop();
            Elapsed_Time.Add(Time.ElapsedMilliseconds / 1000);
            //Clearing the console to hide maze
            Console.Clear();
            Console.ResetColor();
            //Display elapsed time for Player and Bot_Player

            //Console.WriteLine(Elapsed_Time[0] + "     " + Elapsed_Time[1]);

            Elapsed_Time_Screen();

            Console.ReadKey();
            Click_Sound.Play();

            return (Elapsed_Time[0] > Elapsed_Time[1]);
        }

        public static ConsoleKeyInfo Key_Info;
        public static bool Victory; 
        public static SoundPlayer Click_Sound = new SoundPlayer(@"CLICK.wav");

        static void Main(string[] args)
        {
            Game();
        }
    }
}