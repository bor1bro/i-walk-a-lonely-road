using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Input;

using static CoursevayaCSharp.MazeGenerator;
using static CoursevayaCSharp.PathFinder;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Linq;

namespace CoursevayaCSharp
{
    internal class Program
    {
        //Function to display title screen
        public static void Title_Screen()
        {    
            Console.SetWindowSize(77, 8);
            Console.SetBufferSize(78, 8);
            Console.SetWindowPosition(0, 0);
            string Game_Title = File.ReadAllText(@"MAZEGAME.txt");
            for (int Index = 0; Index < Game_Title.Length; Index++)
            {
                Symbol_Check(Game_Title[Index]);
                Console.Write(Game_Title[Index]);
            }
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("\n           Press any key to access menu (or press Escape to exit)            ");
            Console.ResetColor();

            ConsoleKeyInfo Key_Info = Console.ReadKey();
            if (Key_Info.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
            Console.Clear();
        }
        //Function to display menu
        public static void Game_Menu()
        {
            //Initialize values for maze generation
            List<List<char>> Maze_Ptr = new List<List<char>>();
            int Height = 14; //LVL1
            int Width = 10;
            //int Height = 10; //LVL2
            //int Width = 14;
            //int Height = 12; //LVL3
            //int Width = 12;

            //Draw menu       
            Console.SetWindowSize(35, 13);
            Console.SetBufferSize(36, 13);
            Console.SetWindowPosition(0, 0);
            string Game_Menu = File.ReadAllText(@"MAZEGAME_MENU.txt");
            for (int Index = 0; Index < Game_Menu.Length; Index++)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                if (Game_Menu[Index] == 'Y')
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                }
                Console.Write(Game_Menu[Index]);
            }
            Console.ResetColor();
            //Wait for input of desired option (1,2,3,4)
            ConsoleKeyInfo Key_Info = Console.ReadKey();
            Console.Clear();
            switch (Key_Info.Key)
            {
                //Play on generated maze
                case ConsoleKey.D1:
                    {
                        //Generate maze
                        Maze_Ptr = Maze_Generate(Width, Height);
                        //Play maze
                        Maze_Setup(Maze_Ptr);

                        break;
                    }
                //Play on prepared maze
                case ConsoleKey.D2:
                    {
                        //Get maze from file

                        //Play maze
                        Maze_Setup(Maze_Ptr);

                        break;
                    }
                //Display game info
                case ConsoleKey.D3:
                    {
                        //Display info from file
                        //Let player leave this screen
                        break;
                    }
                //Exit game
                case ConsoleKey.D4:
                    {
                        //Display warning
                        //Wait for input (Y/N)
                        //If N go to menu
                        break;
                    }
            }
        }
        //Initialy core loop of the game
        public static void Maze_Setup(List<List<char>> Maze_Ptr)
        {
            //Array to save elapsed time data
            List<long> Elapsed_Time = new List<long>();

            //Initialize position of player
            PathFinder Player = new PathFinder();
            Position2D Player_Pos = new Position2D(1, 1);

            //Loading already generated maze
            List<List<char>> Buf_Maze_Ptr = Maze_Ptr.Maze_Load();

            Player.Init_Player(Player_Pos, Buf_Maze_Ptr);

            //Marking the time of maze completion
            var Time = Stopwatch.StartNew();
            //Bot_Player goes first
            while (!Player.isWin())
            {
                Player.Next_Step();
                Console.SetCursorPosition(0, 0);
                Console.SetWindowSize(Maze_Ptr[0].Count + 1, Maze_Ptr.Count + 1);
                Console.SetBufferSize(Maze_Ptr[0].Count + 1, Maze_Ptr.Count + 1);
                Console.SetWindowPosition(0, 0);
                Maze_Print(Buf_Maze_Ptr);
            }
            //Stopping the timer and saving elapsed time data
            Time.Stop();
            Elapsed_Time.Add(Time.ElapsedMilliseconds / 1000);
            //Clearing the console to hide maze
            Console.Clear();

            //Console.BackgroundColor = ConsoleColor.Yellow;
            //Console.ForegroundColor = ConsoleColor.DarkYellow;
            //Console.WriteLine("\t\t\t\t\t\t\t");
            //Console.WriteLine("\t\tREADY? (Press any key)\t\t\t");
            //Console.WriteLine("\t\t\t\t\t\t\t");
            //Console.ResetColor();
            //Console.Clear();
            //Player goes next
            //Loading already generated maze
            Buf_Maze_Ptr = Maze_Ptr.Maze_Load();

            //Return player to the start
            Player.Init_Player(Player_Pos, Buf_Maze_Ptr);
            Maze_Print(Buf_Maze_Ptr);
            //Marking the time of maze completion
            Time = Stopwatch.StartNew();
            while (!Player.isWin())
            {
                Player.Player_Controller();
                Console.SetCursorPosition(0, 0);
                Maze_Print(Buf_Maze_Ptr);
            }
            //Stopping the timer and saving elapsed time data
            Time.Stop();
            Elapsed_Time.Add(Time.ElapsedMilliseconds / 1000);
            //Clearing the console to hide maze
            Console.Clear();
            //Display elapsed time for Player and Bot_Player

        }
        static void Main(string[] args)
        {
            Title_Screen();
            Game_Menu();
        }
    }
}