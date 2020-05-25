using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Sudoku
{
    class Board
    {
        private int[,] grid;

        public Board(int[,] state)
        {
            this.grid = state;
        }

        private void print()
        {
            for (int i = 0; i < this.grid.GetLength(0); i++)
            {
                for (int k = 0; k < this.grid.GetLength(1); k++)
                {
                    Console.Write(this.grid[i, k].ToString() + ' ');
                }
                Console.WriteLine();
            }
        }

        private static int[] findNextZero(Board board)
        {
            int[] gridSpace = new int[2];
            //iterating through the spaces on the board until we find a zero
            for (int i = 0; i < board.grid.GetLength(0); i++)
                for (int k = 0; k < board.grid.GetLength(1); k++)
                    if (board.grid[i, k] == 0)
                    {
                        gridSpace[0] = i;
                        gridSpace[1] = k;
                        return gridSpace;
                    }
            return null;
        }

        private static bool solve(Board board)
        {
            int[] gridSpace;
            if ((gridSpace = findNextZero(board)) == null) return true;

            for (int i = 1; i <=9; i++)
            {
                board.grid[gridSpace[0], gridSpace[1]] = i;
                if (board.checkGridSpace(gridSpace) == true)
                    if (solve(board) == true) return true;
            }

            board.grid[gridSpace[0], gridSpace[1]] = 0;

            return false;
        }

        private static bool solveWithStyle(Board board)
        {
            board.print();
            Console.Out.WriteLine();
            int[] gridSpace;
            if ((gridSpace = findNextZero(board)) == null) return true;

            for (int i = 1; i <= 9; i++)
            {
                board.grid[gridSpace[0], gridSpace[1]] = i;
                if (board.checkGridSpace(gridSpace) == true)
                    if (solveWithStyle(board) == true) return true;
            }

            board.grid[gridSpace[0], gridSpace[1]] = 0;

            return false;
        }

        private bool checkGridSpace(int[] gridSpace)
        {
            //Checking the horizontal line
            HashSet<int> values = new HashSet<int>();
            for (int i = 0; i < 9; i++)
            {
                int currentValue = this.grid[gridSpace[0], i];
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
                int currentValue = this.grid[i, gridSpace[1]];
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
                    int currentValue = this.grid[i, j];
                    //Zeroes only mark a space that has not yet been filled, no need to account them since we're checking for
                    //duplicates of values on the row
                    if (currentValue != 0) 
                        //HashSet.Add(x) returns false if x is a duplicate of another value in the set
                        if (values.Add(currentValue) == false)
                            //If there's a duplicate value on the board row, it can't be a valid solution
                            return false;
                }

            return true;
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
                {1, 0, 0, 0, 0, 7, 0, 9, 0},
                {0, 3, 0, 0, 2, 0, 0, 0, 8},
                {0, 0, 9, 6, 0, 0, 5, 0, 0},
                {0, 0, 5, 3, 0, 0, 9, 0, 0},
                {0, 1, 0, 0, 8, 0, 0, 0, 2},
                {6, 0, 0, 0, 0, 4, 0, 0, 0},
                {3, 0, 0, 0, 0, 0, 0, 1, 0},
                {0, 4, 1, 0, 0, 0, 0, 0, 7},
                {0, 0, 7, 0, 0, 0, 3, 0, 0}
};
            Board board = new Board(state);
            board.print();
            solve(board);
            Console.Out.WriteLine();
            board.print();

            //solveWithStyle(board);

        }
    }
}
