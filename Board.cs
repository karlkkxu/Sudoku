using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Sudoku
{
    class Board
    {
        private Space[,] grid;

        public Board(int[,] state)
        {
            this.grid = new Space[state.GetLength(0), state.GetLength(1)];
            for (int i = 0; i < state.GetLength(0); i++)
            {
                for (int k = 0; k < state.GetLength(1); k++)
                {
                    this.grid[i, k] = new Space(state[i, k]);
                }
            }
        }

        private void Print()
        {
            for (int i = 0; i < this.grid.GetLength(0); i++)
            {
                for (int k = 0; k < this.grid.GetLength(1); k++)
                {
                    Console.Write(this.grid[i, k].getValue());
                    Console.Write(' ');
                }
                Console.WriteLine();
            }
        }

        private static int[] FindNextZero(Board board)
        {
            int[] gridSpace = new int[2];
            //iterating through the spaces on the board until we find a zero
            for (int i = 0; i < board.grid.GetLength(0); i++)
                for (int k = 0; k < board.grid.GetLength(1); k++)
                    if (board.grid[i, k].getValue() == 0)
                    {
                        gridSpace[0] = i;
                        gridSpace[1] = k;
                        return gridSpace;
                    }
            return null;
        }

        private static bool Solve(Board board)
        {
            int[] gridSpace;
            if ((gridSpace = FindNextZero(board)) == null) return true;

            for (int i = 1; i <=9; i++)
            {
                board.grid[gridSpace[0], gridSpace[1]].setValue(i);
                if (board.CheckGridSpace(gridSpace) == true)
                    if (Solve(board) == true) return true;
            }

            board.grid[gridSpace[0], gridSpace[1]].setValue(0);

            return false;
        }

        private static bool SolveWithStyle(Board board)
        {
            board.Print();
            Console.Out.WriteLine();
            int[] gridSpace;
            if ((gridSpace = FindNextZero(board)) == null) return true;

            for (int i = 1; i <= 9; i++)
            {
                board.grid[gridSpace[0], gridSpace[1]].setValue(i);
                if (board.CheckGridSpace(gridSpace) == true)
                    if (SolveWithStyle(board) == true) return true;
            }

            board.grid[gridSpace[0], gridSpace[1]].setValue(0);

            return false;
        }

        private bool CheckGridSpace(int[] gridSpace)
        {
            //Checking the horizontal line
            HashSet<int> values = new HashSet<int>();
            for (int i = 0; i < 9; i++)
            {
                int currentValue = this.grid[gridSpace[0], i].getValue();
                //Zeroes only mark a space that has not yet been filled, no need to account them since we're checking for
                //duplicates of values on the row
                if (currentValue != 0)
                    //HashSet.Add(x) returns false if x is a duplicate of another value in the set
                    if (values.Add(currentValue) == false)
                        //If there's a duplicate value on the board row, it can't be a valid solution
                        return false;
            }

            //Checking the vertical line
            values.Clear();
            for (int i = 0; i < 9; i++)
            {
                int currentValue = this.grid[i, gridSpace[1]].getValue();
                //Zeroes only mark a space that has not yet been filled, no need to account them since we're checking for
                //duplicates of values on the row
                if (currentValue != 0)
                    //HashSet.Add(x) returns false if x is a duplicate of another value in the set
                    if (values.Add(currentValue) == false)
                        //If there's a duplicate value on the board row, it can't be a valid solution
                        return false;
            }

            //Checking the 3x3 of the space

            //The purpose here is to divide the 9x9 board to a set of 9 3x3 boards
            //By checking the gridSpace like this we can set the specific coordinates on the board
            //we must look through for duplicates
            int startX = 3;
            int endX = 5;
            if (gridSpace[0] > 5)
            {
                startX = 6;
                endX = 8;
            }
            if (gridSpace[0] < 3)
            {
                startX = 0;
                endX = 2;
            }

            int startY = 3;
            int endY = 5;
            if (gridSpace[1] > 5)
            {
                startY = 6;
                endY = 8;
            }
            if (gridSpace[1] < 3)
            {
                startY = 0;
                endY = 2;
            }

            //Now that we know the 3x3 area the gridspace is in, we can iterate through it and check for duplicates like before
            values.Clear();
            for (int i = startX; i <= endX; i++)
                for (int j = startY; j <= endY; j++)
                {
                    int currentValue = this.grid[i, j].getValue();
                    //Zeroes only mark a space that has not yet been filled, no need to account them since we're checking for
                    //duplicates of values on the row
                    if (currentValue != 0) 
                        //HashSet.Add(x) returns false if x is a duplicate of another value in the set
                        if (values.Add(currentValue) == false)
                            //If there's a duplicate value on the board row, it can't be a valid solution
                            return false;
                }

            //Checking the dependencies of the space
            if (this.grid[gridSpace[0], gridSpace[1]].getDependency().Count != 0)
                foreach (Dependency d in this.grid[gridSpace[0], gridSpace[1]].getDependency())
                    if (d.doesHold() == false) return false;

            return true;
        }

        public void SetSpace(int x, int y, Space space)
        {
            this.grid[x, y] = space;
        }

        public Space getSpace(int x, int y)
        {
            return this.grid[x, y];
        }

        static void Main()
        {
            //int[,] state = new int[9, 9]
            //{
            //    {5, 3, 0, 0, 7, 0, 0, 0, 0},
            //    {6, 0, 0, 1, 9, 5, 0, 0, 0},
            //    {0, 9, 8, 0, 0, 0, 0, 6, 0},
            //    {8, 0, 0, 0, 6, 0, 0, 0, 3},
            //    {4, 0, 0, 8, 0, 3, 0, 0, 1},
            //    {7, 0, 0, 0, 2, 0, 0, 0, 6},
            //    {0, 6, 0, 0, 0, 0, 2, 8, 0},
            //    {0, 0, 0, 4, 1, 9, 0, 0, 5},
            //    {0, 0, 0, 0, 8, 0, 0, 7, 9}
            //};
            int[,] state = new int[9, 9]
{
                {0, 0, 0,    0, 0, 0,    0, 0, 0},
                {0, 1, 0,    0, 2, 0,    0, 3, 0},
                {0, 0, 0,    0, 0, 0,    0, 0, 0},

                {0, 0, 0,    0, 0, 0,    0, 0, 0},
                {0, 4, 0,    0, 5, 0,    0, 6, 0},
                {0, 0, 0,    0, 0, 0,    0, 0, 0},

                {0, 0, 0,    0, 0, 0,    0, 0, 0},
                {0, 7, 0,    0, 8, 0,    0, 9, 0},
                {0, 0, 0,    0, 0, 0,    0, 0, 0}
};
            Board board = new Board(state);
            board.Print();

            //board.getSpace(x, x).addDependency(board.getSpace(x, x), x);

            board.getSpace(0, 0).addDependency(board.getSpace(1, 0), 1);
            board.getSpace(1, 0).addDependency(board.getSpace(0, 0), 1);

            board.getSpace(0, 2).addDependency(board.getSpace(0, 3), 1);
            board.getSpace(0, 3).addDependency(board.getSpace(0, 2), 1);

            board.getSpace(0, 7).addDependency(board.getSpace(1, 7), 1);
            board.getSpace(1, 7).addDependency(board.getSpace(0, 7), 1);

            board.getSpace(3, 0).addDependency(board.getSpace(4, 0), 1);
            board.getSpace(4, 0).addDependency(board.getSpace(3, 0), 1);

            board.getSpace(4, 3).addDependency(board.getSpace(5, 3), 1);
            board.getSpace(5, 3).addDependency(board.getSpace(4, 3), 1);

            board.getSpace(5, 6).addDependency(board.getSpace(5, 7), 1);
            board.getSpace(5, 7).addDependency(board.getSpace(5, 6), 1);

            board.getSpace(6, 0).addDependency(board.getSpace(7, 0), 2);
            board.getSpace(7, 0).addDependency(board.getSpace(6, 0), 2);

            board.getSpace(6, 8).addDependency(board.getSpace(7, 8), 2);
            board.getSpace(7, 8).addDependency(board.getSpace(6, 8), 2);
            //Ekat

            board.getSpace(0, 6).addDependency(board.getSpace(1, 6), 1);
            board.getSpace(1, 6).addDependency(board.getSpace(0, 6), 1);
            board.getSpace(1, 6).addDependency(board.getSpace(1, 5), 1);
            board.getSpace(1, 5).addDependency(board.getSpace(1, 6), 1);
            board.getSpace(1, 5).addDependency(board.getSpace(2, 5), 1);
            board.getSpace(2, 5).addDependency(board.getSpace(1, 5), 1);

            board.getSpace(1, 2).addDependency(board.getSpace(1, 3), 1);
            board.getSpace(1, 3).addDependency(board.getSpace(1, 2), 1);
            board.getSpace(1, 3).addDependency(board.getSpace(2, 3), 2);
            board.getSpace(2, 3).addDependency(board.getSpace(1, 3), 2);

            board.getSpace(2, 1).addDependency(board.getSpace(2, 2), 2);
            board.getSpace(2, 2).addDependency(board.getSpace(2, 1), 2);
            board.getSpace(2, 2).addDependency(board.getSpace(3, 2), 2);
            board.getSpace(3, 2).addDependency(board.getSpace(2, 2), 2);

            board.getSpace(5, 1).addDependency(board.getSpace(5, 2), 1);
            board.getSpace(5, 2).addDependency(board.getSpace(5, 1), 1);
            board.getSpace(5, 2).addDependency(board.getSpace(6, 2), 1);
            board.getSpace(6, 2).addDependency(board.getSpace(5, 2), 1);
            board.getSpace(6, 2).addDependency(board.getSpace(6, 1), 2);
            board.getSpace(6, 1).addDependency(board.getSpace(6, 2), 2);

            board.getSpace(7, 3).addDependency(board.getSpace(8, 3), 1);
            board.getSpace(8, 3).addDependency(board.getSpace(7, 3), 1);
            board.getSpace(8, 3).addDependency(board.getSpace(8, 4), 2);
            board.getSpace(8, 4).addDependency(board.getSpace(8, 3), 2);
            board.getSpace(8, 4).addDependency(board.getSpace(8, 5), 1);
            board.getSpace(8, 5).addDependency(board.getSpace(8, 4), 1);
            board.getSpace(8, 5).addDependency(board.getSpace(7, 5), 2);
            board.getSpace(7, 5).addDependency(board.getSpace(8, 5), 2);
            board.getSpace(7, 5).addDependency(board.getSpace(7, 6), 2);
            board.getSpace(7, 6).addDependency(board.getSpace(7, 5), 2);
            board.getSpace(7, 6).addDependency(board.getSpace(6, 6), 2);
            board.getSpace(6, 6).addDependency(board.getSpace(7, 6), 2);
            board.getSpace(6, 6).addDependency(board.getSpace(6, 5), 1);
            board.getSpace(6, 5).addDependency(board.getSpace(6, 6), 1);

            board.getSpace(3, 3).addDependency(board.getSpace(3, 4), 1);
            board.getSpace(3, 4).addDependency(board.getSpace(3, 3), 1);
            board.getSpace(3, 4).addDependency(board.getSpace(4, 4), 1);
            board.getSpace(4, 4).addDependency(board.getSpace(3, 4), 1);
            board.getSpace(4, 4).addDependency(board.getSpace(5, 4), 1);
            board.getSpace(5, 4).addDependency(board.getSpace(4, 4), 1);

            board.getSpace(3, 8).addDependency(board.getSpace(4, 8), 2);
            board.getSpace(4, 8).addDependency(board.getSpace(3, 8), 2);
            board.getSpace(4, 8).addDependency(board.getSpace(5, 8), 1);
            board.getSpace(5, 8).addDependency(board.getSpace(4, 8), 1);

            //TODO testaa matriisin suunnat, hardkoodaa dependencies, pitäisi toimia?
            //board.getSpace(1, 0).setValue(10);
            //board.getSpace(0, 1).setValue(01);
            // 0 1
            // 10 0

            Solve(board);
            Console.Out.WriteLine();
            board.Print();

            //solveWithStyle(board);

        }
    }
}
