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

            //SoundPlayer MusicPlayer = new SoundPlayer(@"MENU.wav");
            //MusicPlayer.Play();

            Key_Info = Console.ReadKey();
            if (Key_Info.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
            Console.Clear();
        }
        //Function to display ready screen
        public static void Ready_Screen(List<List<char>> Buf_Maze_Ptr)
        {
            Console.SetCursorPosition(0, 0);
            Console.SetWindowSize(36, 9);
            Console.SetBufferSize(37, 9);
            Console.SetWindowPosition(0, 0);
            string Game_Menu = File.ReadAllText(@"READY.txt");
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
            Console.ReadKey();
            Console.ResetColor();
            Console.Clear();
            Console.SetWindowPosition(0, 0);
            //Returning console window to maze size 
            Console.SetWindowSize(Buf_Maze_Ptr[0].Count + 2, Buf_Maze_Ptr.Count + 1);
            Console.SetBufferSize(Buf_Maze_Ptr[0].Count + 3, Buf_Maze_Ptr.Count + 1);
        }
        //Function to draw menu
        public static void Game_Menu_Screen()
        {
            Console.SetCursorPosition(0, 0);
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
        }
        //Function to display warning when exiting from menu
        public static void Exit_Warning_Screen()
        {
            Console.SetCursorPosition(0, 0);
            Console.SetWindowSize(42, 10);
            Console.SetBufferSize(43, 10);
            Console.SetWindowPosition(0, 0);
            string Game_Menu = File.ReadAllText(@"EXIT_WARNING.txt");
            for (int Index = 0; Index < Game_Menu.Length; Index++)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                if (Game_Menu[Index] == 'T')
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                }
                Console.Write(Game_Menu[Index]);
            }
            Console.ResetColor();
        }
        //Function to display information screen
        public static void Game_Info_Screen()
        {
            Console.SetCursorPosition(0, 0);
            Console.SetWindowSize(58, 26);
            Console.SetBufferSize(59, 26);
            Console.SetWindowPosition(0, 0);
            string Game_Menu = File.ReadAllText(@"INFO.txt");
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
        //Function to display time elapsed on maze completion
        public static void Elapsed_Time_Screen()
        {

        }

        public static ConsoleKeyInfo Key_Info;
    }
}
