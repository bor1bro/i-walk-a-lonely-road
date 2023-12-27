using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CoursevayaCSharp
{
    //Initializing directions enum
    enum Direction2D
    {
        RIGHT,
        DOWN,
        LEFT,
        UP,
    };
    // Initialize position struct
    struct Position2D
    {
        public int X;
        public int Y;
        //Position2D constructor to initialize starting position
        public Position2D(int X_Pos, int Y_Pos)
        {
            X = X_Pos;
            Y = Y_Pos;
        }
    }

    internal class PathFinder
    {
        /// <summary>
        /// Function to initialize the player
        /// </summary>
        /// <param name="Start_Position"></param>
        /// <param name="Maze_Ptr"></param>
        /// <returns>Maze with player at start position</returns>
        public List<List<char>> Init_Player(Position2D Start_Position, List<List<char>> Maze_Ptr)
        {
            //Maze data to check every step
            Maze_Ptr_ = Maze_Ptr;
            //At the start we are not at the exit
            Is_Win_ = false;
            //1. Initializing player's start position and direction
            Current_Position = Start_Position;
            Current_Direction = Direction2D.RIGHT;

            //Draw player on the screen
            Maze_Ptr_[Current_Position.Y][Current_Position.X] = 'P';

            return Maze_Ptr_;
        }

        /// <summary>
        /// Function that determines bot movement in the maze
        /// </summary>
        public void Next_Step()
        {
            //If at the exit, don't do more steps
            if (Is_Win_)
            {
                return;
            }

            //2. Player turns 90 degrees, counter-clockwise
            Current_Direction = Current_Direction == Direction2D.UP ? Direction2D.LEFT :
                            Current_Direction == Direction2D.LEFT ? Direction2D.DOWN :
                            Current_Direction == Direction2D.DOWN ? Direction2D.RIGHT :
                            Direction2D.UP;
            //Initialize temporary variable to check forward cell
            Position2D Forward_Cell;
            do
            {
                //3. Check cell that the player is facing
                Forward_Cell = Current_Position;
                switch (Current_Direction)
                {
                    case Direction2D.RIGHT:
                        {
                            Forward_Cell.X++;
                            break;
                        }
                    case Direction2D.DOWN:
                        {
                            Forward_Cell.Y++;
                            break;
                        }
                    case Direction2D.LEFT:
                        {
                            Forward_Cell.X--;
                            break;
                        }
                    case Direction2D.UP:
                        {
                            Forward_Cell.Y--;
                            break;
                        }
                }
                //3.1. If wall:
                //Player turns 90 degrees, clockwise
                Current_Direction = Current_Direction == Direction2D.UP ? Direction2D.RIGHT :
                                Current_Direction == Direction2D.LEFT ? Direction2D.UP :
                                Current_Direction == Direction2D.DOWN ? Direction2D.LEFT :
                                Direction2D.DOWN;
                //Do step 3
            } while (Maze_Ptr_[Forward_Cell.Y][Forward_Cell.X] == '#');
            //3.2. If empty:
            //Do step 4
            //Restore correct dirrection
            Current_Direction = Current_Direction == Direction2D.UP ? Direction2D.LEFT :
                        Current_Direction == Direction2D.LEFT ? Direction2D.DOWN :
                        Current_Direction == Direction2D.DOWN ? Direction2D.RIGHT :
                        Direction2D.UP;

            //4. Player steps on next empty cell

            Position2D Previous_Position = Current_Position; //Saving previous position to mark player's trail
            Current_Position = Forward_Cell;
            //5. Check if current cell is the exit:
            //5.1. If it isn't do step 2 etc
            //5.2. If it is the exit - end
            Is_Win_ = Maze_Ptr_[Current_Position.Y][Current_Position.X] == 'X';
            //Marking player position
            Maze_Ptr_[Current_Position.Y][Current_Position.X] = 'B';
            //Marking cells investigated by player
            Maze_Ptr_[Previous_Position.Y][Previous_Position.X] = 'a';
        }

        /// <summary>
        /// Function to control player's movement across the maze
        /// </summary>
        public void Player_Controller()
        {
            //If at the exit, don't do more steps
            if (Is_Win_)
            {
                return;
            }
            //Saving previous position to mark player's trail
            Position2D Previous_Position = Current_Position;
            Position2D Forward_Cell;
            //Wait for input
            ConsoleKeyInfo Key_Info = Console.ReadKey();
            switch (Key_Info.Key)
            {
                case ConsoleKey.UpArrow:
                    {
                        Forward_Cell = Current_Position;
                        Forward_Cell.Y--;
                        if (Maze_Ptr_[Forward_Cell.Y][Forward_Cell.X] != '#')
                        {
                            Current_Position = Forward_Cell;
                        }
                        break;
                    }
                case ConsoleKey.DownArrow:
                    {
                        Forward_Cell = Current_Position;
                        Forward_Cell.Y++;
                        if (Maze_Ptr_[Forward_Cell.Y][Forward_Cell.X] != '#')
                        {
                            Current_Position = Forward_Cell;
                        }
                        break;
                    }
                case ConsoleKey.LeftArrow:
                    {
                        Forward_Cell = Current_Position;
                        Forward_Cell.X--;
                        if (Maze_Ptr_[Forward_Cell.Y][Forward_Cell.X] != '#')
                        {
                            Current_Position = Forward_Cell;
                        }
                        break;
                    }
                case ConsoleKey.RightArrow:
                    {
                        Forward_Cell = Current_Position;
                        Forward_Cell.X++;
                        if (Maze_Ptr_[Forward_Cell.Y][Forward_Cell.X] != '#')
                        {
                            Current_Position = Forward_Cell;
                        }
                        break;
                    }
            }

            Is_Win_ = Maze_Ptr_[Current_Position.Y][Current_Position.X] == 'X';
            //Marking cells investigated by player
            Maze_Ptr_[Previous_Position.Y][Previous_Position.X] = 'o';
            //Marking player position
            Maze_Ptr_[Current_Position.Y][Current_Position.X] = 'P';
        }

        /// <summary>
        /// Function to check for victory
        /// </summary>
        /// <returns>Bool value</returns>
        public bool isWin()
        {
            return Is_Win_;
        }

        //Protected fields
        protected bool Is_Win_;

        protected Position2D Current_Position;
        protected Direction2D Current_Direction;

        //Ptr on maze where we go
        protected List<List<char>> Maze_Ptr_;
    }
}