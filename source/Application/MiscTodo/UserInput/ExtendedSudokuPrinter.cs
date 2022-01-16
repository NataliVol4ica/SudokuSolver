using System;
using System.Drawing;
using Application.Models.SudokuAlgo;

namespace Application.MiscTodo.UserInput
{
    public static class ExtendedSudokuPrinter
    {
        public static void Print(Sudoku sudoku, Point? changedCell = null)
        {
            PrintHorizontalDigits();
            var cells = sudoku.DeepCopyCells();
            for (int blockRowId = 0; blockRowId < 3; blockRowId++)
            {
                for (int blockCellRowId = 0; blockCellRowId < 3; blockCellRowId++)
                {
                    int rowId = blockRowId * 3 + blockCellRowId;
                    for (int candidateRowId = 0; candidateRowId < 3; candidateRowId++)
                    {
                        PrintVerticalRowId(rowId, candidateRowId);
                        for (int columnId = 0; columnId < 9; columnId++)
                        {
                            PrintSudokuCell(cells[rowId, columnId], candidateRowId, IsCellChanged(rowId, columnId, changedCell));

                            if (columnId != 8)
                            {
                                if (columnId % 3 == 2)
                                    PrintBlockVerticalDelimiter();
                                else
                                    PrintCellVerticalDelimiter();
                            }
                        }
                        PrintVerticalRowId(rowId, candidateRowId);
                        PrintEndl();
                    }
                    if (blockCellRowId != 2)
                        PrintCellHorizontalDelimiter();
                }

                if (blockRowId != 2)
                    PrintBlockHorizontalDelimiter();
            }
            PrintHorizontalDigits();
            PrintEndl();
        }

        private static void PrintSudokuCell(SudokuCell cell, int consoleRowId, bool isChangedCell)
        {
            if (cell.HasValue)
            {
                if (consoleRowId == 0 || consoleRowId == 2)
                    PrintText("   ");
                else if (consoleRowId == 1)
                {
                    PrintCellValue($" {cell} ", isChangedCell);
                    SetDefaultColors();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                for (int i = 0; i < 3; i++)
                {
                    var digit = consoleRowId * 3 + i;
                    if (cell.Candidates[digit])
                        PrintText($"{digit + 1}");
                    else
                        PrintText(".");
                }
                SetDefaultColors();
            }
        }

        private static void PrintBlockHorizontalDelimiter()
        {
            PrintVerticalBorderInARow("===");
            Console.BackgroundColor = ConsoleColor.DarkGray;
            PrintText("===========++===========++===========");
            SetDefaultColors();
            PrintVerticalBorderInARow("===");
            PrintEndl();
        }

        private static void PrintCellHorizontalDelimiter()
        {
            PrintVerticalBorderInARow("---");
            PrintText("---+---+---");
            Console.BackgroundColor = ConsoleColor.DarkGray;
            PrintText("++");
            SetDefaultColors();
            PrintText("---+---+---");
            Console.BackgroundColor = ConsoleColor.DarkGray;
            PrintText("++");
            SetDefaultColors();
            PrintText("---+---+---");
            PrintVerticalBorderInARow("---");
            PrintEndl();
        }

        private static void PrintBlockVerticalDelimiter()
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            PrintText("||");
            SetDefaultColors();
        }
        private static void PrintCellVerticalDelimiter()
        {
            PrintText("|");
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
            SetDefaultColors();
        }

        private static void PrintCellValue(string text, bool isChangedCell)
        {
            if (!isChangedCell)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(text);
                return;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(text);
            SetDefaultColors();
        }

        private static void SetDefaultColors()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        //hack to fix console colors
        private static void PrintEndl()
        {
            SetDefaultColors();
            Console.Write(" ");
            Console.WriteLine();
        }
        private static void PrintText(string text)
        {
            Console.Write(text);
        }

        #region Print A..I1..9 borders

        private static void PrintHorizontalDigits()
        {
            PrintBorder("   ");
            for (int i = 0; i < 9; i++) //todo 9
            {
                PrintBorder($" {i + 1}  ");
                if (i != 0 && i % 3 == 2)
                    PrintBorder(" ");
            }

            PrintBorder(" ");
            PrintEndl();
        }

        private static void PrintVerticalRowId(int digit, int rowId)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            if (rowId == 0 || rowId == 2)
                PrintVerticalBorderInARow();
            else if (rowId == 1)
                PrintBorder($" {(char)('A' + digit)} ");

            SetDefaultColors();
        }

        private static void PrintVerticalBorderInARow(string text = null)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            if (!String.IsNullOrEmpty(text))
                PrintBorder(text);
            else
                PrintBorder("   ");
            SetDefaultColors();
        }

        #endregion Print A..I1..9 borders
    }
}
