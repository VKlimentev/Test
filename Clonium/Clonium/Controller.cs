using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using System;
using System.Windows.Forms;
using System.Threading;

namespace Clonium
{
    internal static class Controller
    {
        private const int MaxValueChip = 4;

        private static List<Color> _playerColorsList;
        private static int _currentPlayer;
        private static bool _autoPlay;
        private static Random random = new Random();

        public static Field Field { get; set; }
        public static string FieldName { get; set; }
        public static int FieldSize { get; set; }
        public static int PlayersCount { get; set; }
        public static Color CurrentColor
        {
            get { return _playerColorsList[_currentPlayer]; }
        }


        public static void Initialize(string field, int size, int numberOfPlayers, bool isFirstGame = true)
        {
            FieldName = field;
            FieldSize = size;
            PlayersCount = numberOfPlayers;

            Field = new Field(size);
            InitializePlayers();
            InitializeCells();
            InitializeChips();

            if (isFirstGame)
            {
                Painter.Initialize();
            }
            else
            {
                Painter.DrawField();
            }
        }


        private static void InitializePlayers()
        {
            _currentPlayer = 0;

            _playerColorsList = new List<Color>()
            {
                Color.Blue,
                Color.Red,
                Color.Green,
                Color.Purple
            };

            _autoPlay = PlayersCount == 1 ? true : false;
        }
        private static void InitializeCells()
        {
            int centerX = Field.Dimension / 2;
            int centerY = Field.Dimension / 2 - 1;
            int endField = Field.Dimension - 1;

            switch (FieldName)
            {
                case "Field2":
                    Field.Cells[0, 0].Activate = false;
                    Field.Cells[0, endField].Activate = false;
                    Field.Cells[endField, 0].Activate = false;
                    Field.Cells[endField, endField].Activate = false;

                    if (FieldSize >= 12)
                    {
                        Field.Cells[0, 1].Activate = false;
                        Field.Cells[1, 0].Activate = false;
                        Field.Cells[endField - 1, 0].Activate = false;
                        Field.Cells[0, endField - 1].Activate = false;
                        Field.Cells[endField, 1].Activate = false;
                        Field.Cells[1, endField].Activate = false;
                        Field.Cells[endField, endField - 1].Activate = false;
                        Field.Cells[endField - 1, endField].Activate = false;
                    }

                    break;
                case "Field3":
                    Field.Cells[0, centerX].Activate = false;
                    Field.Cells[0, centerY].Activate = false;
                    Field.Cells[centerX, 0].Activate = false;
                    Field.Cells[centerY, 0].Activate = false;
                    Field.Cells[centerX, endField].Activate = false;
                    Field.Cells[centerY, endField].Activate = false;
                    Field.Cells[endField, centerX].Activate = false;
                    Field.Cells[endField, centerY].Activate = false;

                    if (FieldSize >= 12)
                    {
                        Field.Cells[0, centerX + 1].Activate = false;
                        Field.Cells[0, centerY - 1].Activate = false;
                        Field.Cells[centerX + 1, 0].Activate = false;
                        Field.Cells[centerY - 1, 0].Activate = false;
                        Field.Cells[centerX + 1, endField].Activate = false;
                        Field.Cells[centerY - 1, endField].Activate = false;
                        Field.Cells[endField, centerX + 1].Activate = false;
                        Field.Cells[endField, centerY - 1].Activate = false;


                        Field.Cells[1, centerX].Activate = false;
                        Field.Cells[1, centerY].Activate = false;
                        Field.Cells[centerX, 1].Activate = false;
                        Field.Cells[centerY, 1].Activate = false;
                        Field.Cells[centerX, endField - 1].Activate = false;
                        Field.Cells[centerY, endField - 1].Activate = false;
                        Field.Cells[endField - 1, centerX].Activate = false;
                        Field.Cells[endField - 1, centerY].Activate = false;
                    }

                    break;
                case "Field4":
                    Field.Cells[centerX, centerX].Activate = false;
                    Field.Cells[centerX, centerY].Activate = false;
                    Field.Cells[centerY, centerX].Activate = false;
                    Field.Cells[centerY, centerY].Activate = false;

                    if (FieldSize >= 12)
                    {
                        Field.Cells[centerY, centerY - 1].Activate = false;
                        Field.Cells[centerX, centerY - 1].Activate = false;
                        Field.Cells[centerY, centerX + 1].Activate = false;
                        Field.Cells[centerX, centerX + 1].Activate = false;
                        Field.Cells[centerY - 1, centerX].Activate = false;
                        Field.Cells[centerY - 1, centerY].Activate = false;
                        Field.Cells[centerX + 1, centerX].Activate = false;
                        Field.Cells[centerX + 1, centerY].Activate = false;
                        Field.Cells[centerY - 1, centerY - 1].Activate = false;
                        Field.Cells[centerX + 1, centerY - 1].Activate = false;
                        Field.Cells[centerY - 1, centerX + 1].Activate = false;
                        Field.Cells[centerX + 1, centerX + 1].Activate = false;
                    }

                    goto case "Field2";
            }
        }
        private static void InitializeChips()
        {
            if (FieldSize >= 12 && FieldName != "Field1" && FieldName != "Field3")
            {
                Field.ActivateChip(2, 2, Color.Blue);
                Field.ActivateChip(Field.Dimension - 3, Field.Dimension - 3, Color.Red);

                if (PlayersCount >= 3)
                {
                    Field.ActivateChip(Field.Dimension - 3, 2, Color.Green);
                }

                if (PlayersCount >= 4)
                {
                    Field.ActivateChip(2, Field.Dimension - 3, Color.Purple);
                }
            }
            else
            {
                Field.ActivateChip(1, 1, Color.Blue);
                Field.ActivateChip(Field.Dimension - 2, Field.Dimension - 2, Color.Red);

                if (PlayersCount >= 3)
                {
                    Field.ActivateChip(Field.Dimension - 2, 1, Color.Green);
                }

                if (PlayersCount >= 4)
                {
                    Field.ActivateChip(1, Field.Dimension - 2, Color.Purple);
                }
            }
        }


        public static void HandleCellClick(Point point)
        {
            int row, col;

            if (!Field.FindCellByCoordinates(point, out row, out col) ||
                 Field.GetColorChip(row, col) != _playerColorsList[_currentPlayer] ||
                 Field.GetValueChip(row, col) == 0)
                return;

            Move(point);

            if (_autoPlay)
                AutoMove();

            CheckGameOver();
        }
        private static void Move(Point point)
        {
            int row, col;

            if (!Field.FindCellByCoordinates(point, out row, out col))
                return;

            Color colorChip = Field.GetColorChip(row, col);

            Field.AddValueChip(row, col, colorChip);

            CheckSeparation(colorChip);

            _currentPlayer = (_currentPlayer + 1) % PlayersCount;
            Painter.RefreshField();
        }
        private static void AutoMove()
        {
            Thread.Sleep(100);
            var redCells = Field.GetRedCells();
            if (redCells.Count() == 0)
                return;
            int randomIndex = random.Next(0, redCells.Count());
            Point point = redCells.ElementAt(randomIndex).Coordinates;
            Move(point);
        }
        private static void CheckSeparation(Color colorChip)
        {
            int row, col;

            while (Field.FindCellByValue(MaxValueChip, out row, out col))
            {
                Field.Cells[row, col].Chip.Value -= MaxValueChip;

                Field.AddValueChip(row - 1, col, colorChip);
                Field.AddValueChip(row + 1, col, colorChip);
                Field.AddValueChip(row, col - 1, colorChip);
                Field.AddValueChip(row, col + 1, colorChip);
            }
        }
        private static void CheckGameOver()
        {
            if (Field.GetDistinctColors().Count() == 1)
            {
                Color winningColor = Field.GetDistinctColors().First();
                MessageBox.Show($"Игра окончена! Победил {winningColor}.");
                Initialize(FieldName, FieldSize, PlayersCount, false);
            }
        }
    }
}
