using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Application.Models.SudokuAlgo;
using Application.Models.SudokuAlgo.History.SolutionHistoryNavigation;

namespace ConsoleUserInterface
{
    public static class ExtendedSudokuPrinter
    {
        public static void Print(Sudoku sudoku, List<ValuePosition> removedCandidates = null, List<ValuePosition> highlightedCandidates = null, Point? changedCell = null)
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
                            var removedCandidatesInCell =
                                removedCandidates?.Where(c => c.Position.X == rowId && c.Position.Y == columnId).ToList();

                            var highlightedCandidatesInCell =
                                highlightedCandidates?.Where(c=> c.Position.X == rowId && c.Position.Y == columnId).ToList();

                            PrintSudokuCell(cells[rowId, columnId], candidateRowId, IsCellChanged(rowId, columnId, changedCell), removedCandidatesInCell, highlightedCandidatesInCell);

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


        #region Cells

        private static void PrintSudokuCell(SudokuCell cell, int consoleRowId, bool isChangedCell, List<ValuePosition> removedCandidates, List<ValuePosition> highlightedCandidates)
        {
            if (cell.HasValue)
            {
                if (consoleRowId == 0 || consoleRowId == 2)
                    PrintText("   ");
                else if (consoleRowId == 1)
                {
                    PrintValue($" {cell} ", isChangedCell);
                    SetDefaultColors();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                for (int i = 0; i < 3; i++)
                {
                    var digit = consoleRowId * 3 + i;
                    if (highlightedCandidates?.Any(e => e.Value == digit + 1) ?? false)
                        PrintHighlightedCandidate(digit + 1);
                    else if (cell.HasCandidate(digit))
                        PrintText($"{digit + 1}");
                    else if (removedCandidates?.Any(e => e.Value == digit + 1) ?? false)
                        PrintRemovedCandidate(digit + 1);
                    else
                        PrintText(".");
                }
                SetDefaultColors();
            }
        }

        private static void PrintRemovedCandidate(int digit)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            PrintText(digit.ToString());
            Console.ForegroundColor = ConsoleColor.DarkGray;
        }

        private static void PrintHighlightedCandidate(int digit)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintText(digit.ToString());
            Console.ForegroundColor = ConsoleColor.DarkGray;
        }

        private static void PrintValue(string text, bool isChangedCell)
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

        private static bool IsCellChanged(int x, int y, Point? changedCell)
        {
            if (!changedCell.HasValue)
                return false;
            if (changedCell.Value.X == x && changedCell.Value.Y == y)
                return true;
            return false;
        }

        #endregion Cells

        private static void SetDefaultColors()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static void PrintEndl()
        {
            //hack to fix console colors
            SetDefaultColors();
            Console.Write(" ");
            Console.WriteLine();
        }

        private static void PrintText(string text)
        {
            Console.Write(text);
        }

        #region Outter Borders

        private static void PrintVerticalBorderInARow(string text = null)
        {
            if (!String.IsNullOrEmpty(text))
                PrintBorderText(text);
            else
                PrintBorderText("   ");
            SetDefaultColors();
        }

        private static void PrintVerticalRowId(int digit, int rowId)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            if (rowId == 0 || rowId == 2)
                PrintVerticalBorderInARow();
            else if (rowId == 1)
                PrintBorderText($" {(char)('A' + digit)} ");

            SetDefaultColors();
        }

        private static void PrintHorizontalDigits()
        {
            PrintBorderText("   ");
            for (int i = 0; i < 9; i++) //todo 9
            {
                PrintBorderText($" {i + 1}  ");
                if (i != 0 && i % 3 == 2)
                    PrintBorderText(" ");
            }

            PrintBorderText(" ");
            PrintEndl();
        }

        private static void PrintBorderText(string text)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(text);
            SetDefaultColors();
        }

        #endregion Outter Borders

        #region Inner Borders

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

        #endregion Inner Borders
    }
}
