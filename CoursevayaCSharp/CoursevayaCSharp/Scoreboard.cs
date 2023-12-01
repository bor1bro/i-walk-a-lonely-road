using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursevayaCSharp
{
    struct PlayerInfo
    {
        public char[] Name = new char[7];
        public long Time = new();
        public char[] Maze_Name = new char[8];

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
            }
            else
            {
                StreamWriter Scoreboard_File = new StreamWriter(@"Scoreboard.txt", true);
                Scoreboard_File.Write(Player_Info.Name + " ");
                Scoreboard_File.Write(Player_Info.Time + " ");
                Scoreboard_File.Write(Player_Info.Maze_Name + " ");
            }  
        }

        public static string[] Scoreboard_Load()
        {
            if (!File.Exists(@"Scoreboard.txt"))
            {
                File.Create(@"Scoreboard.txt");
            }
            string All_Scoreboard_Info = File.ReadAllText(@"Scoreboard.txt");
            string[] Splitted_Scoreboard_Info = All_Scoreboard_Info.Split(' ');
            return Splitted_Scoreboard_Info;
        }
    }
}
