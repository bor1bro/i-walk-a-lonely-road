using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using static CoursevayaCSharp.ScreenVisual;

namespace CoursevayaCSharp
{
    public struct PlayerInfo
    {
        public string Name;
        public long Time;
        public string Maze_Name;

        public PlayerInfo()
        {
        }
    }

    internal class Scoreboard
    {

        public static void Scoreboard_Save(PlayerInfo Player_Info)
        {
            if (!File.Exists(@"Scoreboard.txt"))
            {
                File.Create(@"Scoreboard.txt");
                StreamWriter Scoreboard_File = new StreamWriter(@"Scoreboard.txt", true);
                Scoreboard_File.Write(Player_Info.Name + " ");
                Scoreboard_File.Write(Player_Info.Time + " ");
                Scoreboard_File.Write(Player_Info.Maze_Name + " ");
                Scoreboard_File.Close();
            }
            else
            {
                StreamWriter Scoreboard_File = new StreamWriter(@"Scoreboard.txt", true);
                Scoreboard_File.Write(Player_Info.Name + " ");
                Scoreboard_File.Write(Player_Info.Time + " ");
                Scoreboard_File.Write(Player_Info.Maze_Name + " ");
                Scoreboard_File.Close();
            }
            
        }

        public static string[] Scoreboard_Load()
        {
            if (!File.Exists(@"Scoreboard.txt"))
            {
                return null;
            }
            string All_Scoreboard_Info = File.ReadAllText(@"Scoreboard.txt");
            string[] Splitted_Scoreboard_Info = All_Scoreboard_Info.Split(' ');
            return Splitted_Scoreboard_Info;
        }

        public static string Scoreboard_Player_Input(string Player_Name)
        {
            string Buf_Name = Console.ReadLine();
            Player_Name = Buf_Name.Substring(0, 8);
            return Player_Name;
        }

        public static string[] Get_Scoreboard_Ten(int Ten, string[] Info)
        {
            int Counter = 0;
            string[] Ten_Info = new string[30];
            do
            {
                if (Ten == Info.Length)
                {
                    return Ten_Info;
                }
                Ten_Info[Counter] = Info[Ten];
                Ten++;
                Counter++;
            } while (Counter != 30 && Ten < Info.Length);
            return Ten_Info;
        }

        public static void Leaf_Through_Scoreboard()
        {
            bool Exit = false;
            int Scoreboard_Ten = 0;
            string[] Scoreboard_Info = Scoreboard_Load();
            string[] Scoreboard_Ten_Info = Get_Scoreboard_Ten(Scoreboard_Ten, Scoreboard_Info);

            while (!Exit)
            {
                //Display prepared maze screen with ten of player info's
                Scoreboard_Screen(Scoreboard_Ten_Info);

                //Wait for input
                Key_Info = Console.ReadKey();
                switch (Key_Info.Key)
                {
                    //if -> show next quad (if remaining files don't make a quad, display only the remainings)
                    case ConsoleKey.RightArrow:
                        {
                            Scoreboard_Ten = Scoreboard_Ten + 30;
                            //Check if quad goes beyond name array's range, if does fit it in this range  
                            if (Scoreboard_Ten > Scoreboard_Info.Length)
                            {
                                Scoreboard_Ten = Scoreboard_Ten - 30;
                            }
                            Scoreboard_Ten_Info = Get_Scoreboard_Ten(Scoreboard_Ten, Scoreboard_Info);
                            break;
                        }
                    //if <- show prev quad (if it's going negative, show first quad)
                    case ConsoleKey.LeftArrow:
                        {
                            Scoreboard_Ten = Scoreboard_Ten - 30;
                            //Check if quad goes beyond name array's range, if does fit it in this range  
                            if (Scoreboard_Ten < 0)
                            {
                                Scoreboard_Ten = Scoreboard_Ten + 30;
                            }
                            Scoreboard_Ten_Info = Get_Scoreboard_Ten(Scoreboard_Ten, Scoreboard_Info);
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

        public static SoundPlayer Click_Sound = new(@"Sounds/CLICK.wav");
    }
}
