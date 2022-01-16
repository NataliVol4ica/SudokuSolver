using System;
using System.Drawing;
using Application.Models.SudokuAlgo;

namespace ConsoleUserInterface
{
    public static class BasicSudokuPrinter
    {
        public static void Print(Sudoku sudoku, Point? changedCell = null)
        {
            var cells = sudoku.DeepCopyCells();
            PrintHorizontalDigits();
            for (int i = 0; i < 9; i++)//todo 9
            {
                if (i != 0 && i % 3 == 0)
                {
                    PrintBorder("   ");
                    PrintText(" ------+-------+------");
                    PrintBorder("   ");
                    PrintText(" \n");
                }
                PrintBorder($" {(char) ('A' + i)} ");
                PrintText(" ");
                for (int j = 0; j < 9; j++)//todo 9
                {
                    if (j < 8)
                    {
                        PrintField(cells[i, j].ToString(), IsCellChanged(i, j, changedCell));
                        PrintText(" ");
                        if (j > 0 && j % 3 == 2)
                            PrintText("| ");
                    }
                    else
                    {
                        PrintField(cells[i, j].ToString(), IsCellChanged(i, j, changedCell));
                    }
                }
                PrintBorder($" {(char)('A' + i)} ");
                PrintText(" \n");
            }
            PrintHorizontalDigits();
            PrintText(" \n");
        }

        private static void PrintHorizontalDigits()
        {

            PrintBorder("    ");
            for (int i = 0; i < 9; i++) //todo 9
            {
                PrintBorder($"{i + 1} ");
                if (i != 0 && i % 3 == 2)
                    PrintBorder("  ");
            }

            PrintText( "\n");
        }

        private static bool IsCellChanged(int x, int y, Point? changedCell)
        {
            if (!changedCell.HasValue)
                return false;
            if (changedCell.Value.X == x && changedCell.Value.Y == y)
                return true;
            return false;
        }

        private static void PrintBorder(string text)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(text);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        private static void PrintField(string text, bool isChangedCell)
        {
            if (!isChangedCell)
            {
               Console.Write(text);
               return;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static void PrintText(string text)
        {
            Console.Write(text);
        }
    }
}
