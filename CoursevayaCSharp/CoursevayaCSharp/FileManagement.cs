using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace CoursevayaCSharp
{
    internal class FileManagement
    {
        public static List<List<char>> Maze_File_Load(List<List<char>> Maze_Ptr)
        {
            string File_Name = Console.ReadLine();
            string Input = File.ReadAllText(@"Saved-Mazes/" + File_Name);
            
            Maze_Ptr.Capacity = 37;
            int K_Index = 0;

            for (int I_Index = 0; I_Index < Maze_Ptr.Capacity; ++I_Index)
            {
                List<char> Row = new();
                Row.Capacity = 43;
                for (int J_Index = 0; J_Index < Row.Capacity; ++J_Index)
                {
                        Row.Add(Input[K_Index]);
                        K_Index++; 
                }
                Maze_Ptr.Add(Row);
            }
            return Maze_Ptr;
        }
        /// <summary>
        /// Function to save maze on which player was victorious
        /// </summary>
        public static void Maze_File_Save(List<List<char>> Maze_Ptr)
        {
            //Generating random file name
            var PathName = Random_File_Name(8);
            int K_Index = 0;
            char[] Maze_Ptr_String = new char[Maze_Ptr.Count*Maze_Ptr[0].Count];

            for (int I_Index = 0; I_Index < Maze_Ptr.Count; ++I_Index)
            {
                for (int J_Index = 0; J_Index < Maze_Ptr.Count; ++J_Index)
                {
                    Maze_Ptr_String[K_Index] = Maze_Ptr[I_Index][J_Index];
                    K_Index++;
                }
            }

            using (FileStream FileStream = File.OpenWrite(@"Saved-Mazes/" + PathName + ".txt"))
            {
                Byte[] Info = new UTF8Encoding(true).GetBytes(Maze_Ptr_String);
                //Writing maze into the file
                FileStream.Write(Info, 0, Info.Length);
            }
        }
        /// <summary>
        /// Function to generate random file numbers (to save mazes)
        /// </summary>
        /// <returns>Random file name(without .txt)</returns>
        public static string Random_File_Name(int Length)
        {
            const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(Chars, Length)
                .Select(String => String[Random_Name.Next(String.Length)]).ToArray());
        }

        static Random Random_Name = new();
    }
}
