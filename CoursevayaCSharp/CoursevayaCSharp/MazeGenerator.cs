﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CoursevayaCSharp
{
    public class MazeGenerator
    {
        //Function to generate maze structure
        public static List<List<char>> Maze_Generate(int Width, int Height)
        {
            //Restriction for parameters by 0
            if ((Width < 1) || (Height < 1))
            {
                return null;
            }
            var Max_Limit = uint.MaxValue;
            //Restriction for parameters by max size
            if (((Max_Limit - 1) / 2 <= Width) || ((Max_Limit - 1) / 2 <= Height))
            {
                return null;
            }
            //Initialize size of output maze matrix
            //Every cell are fragments 2x2, + 1 up and left for walls
            int Output_Height = Height * 2 + 1;
            int Output_Width = Width * 2 + 1;
            //Initialize ptr to maze
            var Maze_Ptr = new List<List<char>>();
            //Reserve size of maze by height
            Maze_Ptr.Capacity = Output_Height;
            //Loop for initializing empty maze row by row
            for (int I_Index = 0; I_Index < Output_Height; ++I_Index)
            { 
                var Row = new List<char>();
                Row.Capacity = Output_Width;
                for (int J_Index = 0; J_Index < Output_Width; ++J_Index)
                {
                    if ((I_Index % 2 == 1) && (J_Index % 2 == 1))
                    {
                        Row.Add(' ');
                    }
                    else
                    {
                        if (((I_Index % 2 == 1) && (J_Index % 2 == 0) && (J_Index != 0) && (J_Index != Output_Width - 1)) ||
                            ((J_Index % 2 == 1) && (I_Index % 2 == 0) && (I_Index != 0) && (I_Index != Output_Height - 1)))
                        {
                            Row.Add(' ');
                        }
                        else 
                        {
                            Row.Add('#');
                        }
                    }
                }
                Maze_Ptr.Add(Row);
            }
            //I. Creating first row of a maze, any cell is not in any set
            //Initialize extra string which will save attachment to any set
            var Row_Set = new List<uint>();
            Row_Set.Capacity = Width;
            //"0" means that cell is not in any set
            for (int Index = 0; Index < Width; ++Index)
            {
                Row_Set.Add(0);
            }
            //Counter for sets
            uint Set = 1;
            //Initialize RNG
            Random Random = new Random();
            //Loop for Eller's algorithm
            for (int I_Index = 0; I_Index < Height; ++I_Index)
            {
                //II. for each cell that is not in a set, create individual set
                for (int J_Index = 0; J_Index < Width; ++J_Index)
                {
                    if (Row_Set[J_Index] == 0)
                    {
                        Row_Set[J_Index] = Set++;
                    }
                }
                //III. Create right walls for cells, moving from left to right
                for (int J_Index = 0; J_Index < Width - 1; ++J_Index)
                {
                    //"0" - no wall, "1" - place wall, choosing randomly
                    var Right_Wall = Random.Next(2);
                    //If current cell and cell to the right are in the same set, place wall between (excludes loops)
                    if ((Right_Wall == 1) || (Row_Set[J_Index] == Row_Set[J_Index + 1]))
                    {
                        Maze_Ptr[I_Index * 2 + 1][J_Index * 2 + 2] = '#';
                    }
                    else 
                    {
                        //If no wall, merge sets of current cell and cell to the right
                        var Changing_Set = Row_Set[J_Index + 1];
                        for (int L_Index = 0; L_Index < Width; ++L_Index)
                        {
                            if (Row_Set[L_Index] == Changing_Set)
                            {
                                Row_Set[L_Index] = Row_Set[J_Index];
                            }
                        }
                    }
                }
                //IV. Create bottom walls, moving from left to right
                for (int J_Index = 0; J_Index < Width; ++J_Index)
                {
                    //"0" - no wall, "1" - place wall, choosing randomly
                    var Bottom_Wall = Random.Next(2);
                    //If cell is single in it's set, don't place wall
                    uint Current_Set_Count = 0;
                    for (int L_Index = 0; L_Index < Width; ++L_Index)
                    {
                        //Current set cell count
                        if (Row_Set[J_Index] == Row_Set[L_Index])
                        {
                            Current_Set_Count++;
                        }
                    }
                    //If cell is single in it's set and don't have bottom wall, don't place wall
                    if ((Bottom_Wall == 1) && (Current_Set_Count != 1))
                    {
                        Maze_Ptr[I_Index * 2 + 2][J_Index * 2 + 1] = '#';
                    }                  
                }
                //V. Decide, whether to add more rows or stop and finish maze
                //Check that every set have atleast one cell without a wall (unless it's last row)
                if (I_Index != Height - 1)
                {
                    for (int J_Index = 0; J_Index < Width; ++J_Index)
                    {
                        uint Passage_Count = 0;
                        for (int L_Index = 0; L_Index < Width; ++L_Index)
                        {
                            if ((Row_Set[L_Index] == Row_Set[J_Index]) && (Maze_Ptr[I_Index * 2 + 2][L_Index * 2 + 1]) == ' ')
                            {
                                Passage_Count++;
                            }
                        }
                        if (Passage_Count == 0)
                        {
                            Maze_Ptr[I_Index * 2 + 2][J_Index * 2 + 1] = ' ';
                        }
                    }
                    for (int J_Index = 0; J_Index < Width; ++J_Index)
                    {
                        //Check if there's wall at the bottom of current row
                        if (Maze_Ptr[I_Index * 2 + 2][J_Index * 2 + 1] == '#')
                        {
                            //If wall, delete cell from set
                            Row_Set[J_Index] = 0;
                        }
                    }
                }
            }
            //If finishing maze, add bottom wall for every cell, from left to right
            for (int J_Index = 0; J_Index < Width - 1; ++J_Index)
            {
                //If current cell and cell to the right are in the different sets, then
                if (Row_Set[J_Index] != Row_Set[J_Index + 1])
                {
                    //Delete right wall
                    Maze_Ptr[Output_Height - 2][J_Index * 2 + 2] = ' ';
                }
            }
            //Output ptr on result maze
            return Maze_Ptr;
        }

        //Function to draw maze in console
        public static void Maze_Print(List<List<char>> Maze_Ptr)
        {
            //Check ptr for null
            if (Maze_Ptr == null)
            {
                return;
            }
            //Scanning maze row by row and outputting to the console
            for (int I_Index = 0; I_Index < Maze_Ptr.Count(); ++I_Index)
            {
                for (int J_Index = 0; J_Index < Maze_Ptr[0].Count(); ++J_Index)
                {
                    Symbol_Check(Maze_Ptr[I_Index][J_Index]);
                    Console.Write(Maze_Ptr[I_Index][J_Index]);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        //Function to visualize game
        public static void Symbol_Check(char Symbol)
        {
            switch (Symbol)
            {
                case '#':
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break; 
                }
                case ' ':
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    break; 
                }
                case 'P':
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    break; 
                }
                case 'X':
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Green;
                    break; 
                }
                case 'o':
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break; 
                }
                case '|':
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break; 
                }
                case 'Y':
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break; 
                }
            }
        }
        private MazeGenerator() { }
    }
}
