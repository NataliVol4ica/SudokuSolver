using System;
using System.Drawing;
using Application.Models.SudokuAlgo;

namespace Application.MiscTodo.UserInput
{
    public static class ExtendedSudokuPrinter
    {
        public static void Print(Sudoku sudoku, Point? changedCell = null)
        {
            var cells = sudoku.DeepCopyCells();
            for (int blockRowId = 0; blockRowId < 3; blockRowId++)
            {
                //single block row printer
                for (int blockCellRowId = 0; blockCellRowId < 3; blockCellRowId++)
                {
                    int rowId = blockRowId * 3 + blockCellRowId;
                    //print 9 cells in form of 3 rows
                    for (int candidateRowId = 0; candidateRowId < 3; candidateRowId++)
                    {
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
                        PrintEndl();
                    }
                    if (blockCellRowId != 2)
                        PrintCellHorizontalDelimiter();
                }

                if (blockRowId != 2)
                    PrintBlockHorizontalDelimiter();
            }

            //PrintHorizontalDigits();

            //PrintHorizontalDigits();
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
            Console.BackgroundColor = ConsoleColor.DarkGray;
            PrintText("===========++===========++===========");
            SetDefaultColors();
            PrintEndl();
        }

        private static void PrintCellHorizontalDelimiter()
        {
            PrintText("---+---+---");
            Console.BackgroundColor = ConsoleColor.DarkGray;
            PrintText("++");
            SetDefaultColors();
            PrintText("---+---+---");
            Console.BackgroundColor = ConsoleColor.DarkGray;
            PrintText("++");
            SetDefaultColors();
            PrintText("---+---+---");
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

        private static void PrintCellBorder()
        {
            PrintBorder("   ");
            PrintText(" ------+-------+------");
            PrintBorder("   ");
            PrintEndl();
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
    }
}
