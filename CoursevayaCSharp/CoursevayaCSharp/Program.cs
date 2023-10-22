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

namespace CoursevayaCSharp
{
    internal class Program
    {
        //Function to display title screen
        public static void Title_Screen()
        {
            string Game_Title = File.ReadAllText(@"D:\Coding\jopa\allog\CoursevayaCSharp\CoursevayaCSharp\MAZEGAME.txt");
            for (int Index = 0; Index < Game_Title.Length; Index++)
            {
                Symbol_Check(Game_Title[Index]);
                Console.Write(Game_Title[Index]);
            }
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\n           Press any key to access menu (or press Escape to exit)            ");
            Console.ResetColor();

            ConsoleKeyInfo Key_Info = Console.ReadKey();
            if (Key_Info.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
            Console.Clear();          
        }
        //Function to display menu
        public static void Menu()
        {
            string Game_Menu = File.ReadAllText(@"D:\Coding\jopa\allog\CoursevayaCSharp\CoursevayaCSharp\MAZEGAME_MENU.txt");
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(Game_Menu);
            Console.ResetColor();

            ConsoleKeyInfo Key_Info = Console.ReadKey();
            Console.Clear();
            switch (Key_Info.Key)
            {
                //Play on generated maze
                case ConsoleKey.D1:
                    {
                        //Generate maze
                        //Bot_Player goes first
                        //Player goes after Bot_Player completes maze
                        //Display elapsed time for Player and Bot_Player
                        break;
                    }
                //Play on prepared maze
                case ConsoleKey.D2:
                    {
                        //Get maze from file
                        //Bot_Player goes first
                        //Player goes after Bot_Player completes maze
                        //Display elapsed time for Player and Bot_Player
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
        public static async void Maze_Setup(int Width, int Height, List<List<char>> Maze_Ptr)
        {
            Maze_Ptr = Maze_Generate(Width, Height);

            Maze_Ptr[Height * 2 - 1][Width * 2 - 1] = 'X';

            PathFinder Player = new PathFinder();
            Position2D Player_Pos = new Position2D(1, 1);

            Player.Init_Player(Player_Pos, Maze_Ptr);

            //Marking the time of maze completion
            var Time = Stopwatch.StartNew();
            
            //Slowing down game loop
            //
            //
            //
            //        ?????
            //
            //
            //

            while (!Player.isWin())
            {
                Player.Next_Step();
                Console.SetCursorPosition(0, 0);
                Maze_Print(Maze_Ptr);
            }
            //Stopping the timer
            Time.Stop();

            //Clearing the console from the maze
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\t\t\t\t\t\t\t");
            Console.WriteLine("\t\tTime elapsed:\t" + Time.ElapsedMilliseconds / 1000 + " seconds\t\t");
            Console.WriteLine("\t\t\t\t\t\t\t");

            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            int Height = 15;
            int Width = 15;
            List<List<char>> Maze = new List<List<char>>();

            Title_Screen();
            Menu();
            Maze_Setup(Width, Height, Maze);
        }
    }
}
