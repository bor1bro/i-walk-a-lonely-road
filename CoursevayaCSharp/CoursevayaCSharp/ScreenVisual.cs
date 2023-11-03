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

namespace CoursevayaCSharp
{
    internal class ScreenVisual
    {
        /// <summary>
        /// Function to display title screen
        /// </summary>
        public static void Title_Screen()
        {
            Console.SetWindowSize(77, 8);
            Console.SetBufferSize(78, 8);
            Console.SetWindowPosition(0, 0);
            string Game_Title = File.ReadAllText(@"Screens/MAZEGAME.txt");
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

            Key_Info = Console.ReadKey();
            if (Key_Info.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
            Console.Clear();
        }
        /// <summary>
        /// Function to display ready screen
        /// </summary>
        public static void Ready_Screen(List<List<char>> Buf_Maze_Ptr)
        {
            Console.SetCursorPosition(0, 0);
            Console.SetWindowSize(34, 10);
            Console.SetBufferSize(35, 10);
            Console.SetWindowPosition(0, 0);
            string Ready = File.ReadAllText(@"Screens/READY.txt");
            for (int Index = 0; Index < Ready.Length; Index++)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                if (Ready[Index] == 'Y')
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                }
                Console.Write(Ready[Index]);
            }
        }
        /// <summary>
        /// Function to draw menu
        /// </summary>
        public static void Game_Menu_Screen()
        {
            Console.SetCursorPosition(0, 0);
            Console.SetWindowSize(35, 15);
            Console.SetBufferSize(36, 15);
            Console.SetWindowPosition(0, 0);
            string Game_Menu = File.ReadAllText(@"Screens/MAZEGAME_MENU.txt");
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
        }
        /// <summary>
        /// Function to display warning when exiting from menu
        /// </summary>
        public static void Exit_Warning_Screen()
        {
            Console.SetCursorPosition(0, 0);
            Console.SetWindowSize(42, 10);
            Console.SetBufferSize(43, 10);
            Console.SetWindowPosition(0, 0);
            string Warning = File.ReadAllText(@"Screens/EXIT_WARNING.txt");
            for (int Index = 0; Index < Warning.Length; Index++)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                if (Warning[Index] == 'T')
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                }
                Console.Write(Warning[Index]);
            }
            Console.ResetColor();
        }
        /// <summary>
        /// Function to display information screen
        /// </summary>
        public static void Game_Info_Screen()
        {
            Console.SetCursorPosition(0, 0);
            Console.SetWindowSize(58, 26);
            Console.SetBufferSize(59, 26);
            Console.SetWindowPosition(0, 0);
            string Game_Info = File.ReadAllText(@"Screens/INFO.txt");
            for (int Index = 0; Index < Game_Info.Length; Index++)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                if (Game_Info[Index] == 'Y')
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                }
                Console.Write(Game_Info[Index]);
            }
            Console.ResetColor();
        }
        /// <summary>
        /// Function to display time elapsed on maze completion
        /// </summary>
        public static void Elapsed_Time_Screen(List<long> Elapsed_Time_Array)
        {
            Console.SetCursorPosition(0, 0);
            Console.SetWindowSize(28, 12);
            Console.SetBufferSize(29, 12);
            Console.SetWindowPosition(0, 0);
            string Elapsed_Time = File.ReadAllText(@"Screens/ELAPSED.txt");
            for (int Index = 0; Index < Elapsed_Time.Length; Index++)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                if (Elapsed_Time[Index] == '.')
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                }
                Console.Write(Elapsed_Time[Index]);
            }
            Console.SetCursorPosition(15, 5);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(Elapsed_Time_Array[1]);
            Console.SetCursorPosition(15, 7);
            Console.Write(Elapsed_Time_Array[0]);
        }

        public static void Lose_Screen()
        {
            Console.SetCursorPosition(0, 0);
            Console.SetWindowSize(28, 17);
            Console.SetBufferSize(29, 17);
            Console.SetWindowPosition(0, 0);
            string Game_Info = File.ReadAllText(@"Screens/LOSE.txt");
            for (int Index = 0; Index < Game_Info.Length; Index++)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                if (Game_Info[Index] == '.')
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                }
                Console.Write(Game_Info[Index]);
            }
            Console.ResetColor();
        }
        
        public static void Tie_Screen()
        {

        }
        /// <summary>
        /// Function to display choose menu
        /// </summary>
        public static void Maze_Choose_Screen(string[] Names)
        {
            Console.SetCursorPosition(0, 0);
            Console.SetWindowSize(24, 17);
            Console.SetBufferSize(25, 17);
            Console.SetWindowPosition(0, 0);
            string Game_Info = File.ReadAllText(@"Screens/CHOOSE.txt");
            for (int Index = 0; Index < Game_Info.Length; Index++)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                if (Game_Info[Index] == ',')
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                }
                Console.Write(Game_Info[Index]);
            }
            Console.SetCursorPosition(7, 5);
            Console.Write(Names[0]);
            Console.SetCursorPosition(7, 7);
            Console.Write(Names[1]);
            Console.SetCursorPosition(7, 9);
            Console.Write(Names[2]);
            Console.SetCursorPosition(7, 11);
            Console.Write(Names[3]);
            Console.ResetColor();
        }

        //public SoundPlayer MusicPlayer = new SoundPlayer(@"MENU.wav");
        //MusicPlayer.PlayLooping();               
        public static ConsoleKeyInfo Key_Info;
    }
}
