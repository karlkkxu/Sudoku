using System;

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

        private bool checkGridSpace(int[] gridSpace)
        {
            
        }

        static void Main()
            {
                int[,] state = new int[9, 9]
                {
                {5, 3, 0, 0, 7, 0, 0, 0, 0},
                {6, 0, 0, 1, 9, 5, 0, 0, 0},
                {0, 9, 8, 0, 0, 0, 0, 6, 0},
                {8, 0, 0, 0, 6, 0, 0, 0, 3},
                {4, 0, 0, 8, 0, 3, 0, 0, 1},
                {7, 0, 0, 0, 2, 0, 0, 0, 6},
                {0, 6, 0, 0, 0, 0, 2, 8, 0},
                {0, 0, 0, 4, 1, 9, 0, 0, 5},
                {0, 0, 0, 0, 8, 0, 0, 7, 9}
                };
                Board board = new Board(state);
                board.print();
                solve(board);
            }
    }
}
