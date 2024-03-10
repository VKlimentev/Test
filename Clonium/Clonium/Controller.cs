using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using System;
using System.Windows.Forms;

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
        public static int PlayersCount { get; set; }


        public static void Initialize(string field = "Field1", int numberOfPlayers = 1, bool isFirstGame = true)
        {
            int size = field == "Field1" || field == "Field3" ? 6 : 8;
            FieldName = field;
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
            switch (FieldName)
            {
                case "Field3":
                    Field.Cells[0, 0].Activate = false;
                    Field.Cells[0, Field.Dimension - 1].Activate = false;
                    Field.Cells[Field.Dimension - 1, 0].Activate = false;
                    Field.Cells[Field.Dimension - 1, Field.Dimension - 1].Activate = false;
                    break;
                case "Field4":
                    Field.Cells[0, 3].Activate = false;
                    Field.Cells[0, 4].Activate = false;
                    Field.Cells[3, 0].Activate = false;
                    Field.Cells[4, 0].Activate = false;
                    Field.Cells[3, 7].Activate = false;
                    Field.Cells[4, 7].Activate = false;
                    Field.Cells[7, 3].Activate = false;
                    Field.Cells[7, 4].Activate = false;
                    break;
                case "Field5":
                    Field.Cells[3, 3].Activate = false;
                    Field.Cells[3, 4].Activate = false;
                    Field.Cells[4, 3].Activate = false;
                    Field.Cells[4, 4].Activate = false;
                    goto case "Field3";
            }
        }
        private static void InitializeChips()
        {
            Field.Cells[1, 1].Chip.Value = 3;
            Field.Cells[1, 1].Chip.Color = Color.Blue;

            Field.Cells[Field.Dimension - 2, Field.Dimension - 2].Chip.Value = 3;
            Field.Cells[Field.Dimension - 2, Field.Dimension - 2].Chip.Color = Color.Red;

            if (PlayersCount < 3)
                return;

            Field.Cells[Field.Dimension - 2, 1].Chip.Value = 3;
            Field.Cells[Field.Dimension - 2, 1].Chip.Color = Color.Green;

            if (PlayersCount < 4)
                return;

            Field.Cells[1, Field.Dimension - 2].Chip.Value = 3;
            Field.Cells[1, Field.Dimension - 2].Chip.Color = Color.Purple;
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

            Field.AddValueChip(colorChip, row, col);

            CheckSeparation(colorChip);

            _currentPlayer = (_currentPlayer + 1) % PlayersCount;
            Painter.RefreshField();
        }
        private static void AutoMove()
        {
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

                Field.AddValueChip(colorChip, row - 1, col);
                Field.AddValueChip(colorChip, row + 1, col);
                Field.AddValueChip(colorChip, row, col - 1);
                Field.AddValueChip(colorChip, row, col + 1);
            }
        }
        private static void CheckGameOver()
        {
            if (Field.GetDistinctColors().Count() == 1)
            {
                Color winningColor = Field.GetDistinctColors().First();
                MessageBox.Show($"Игра окончена! Победил {winningColor}.");
                Initialize(FieldName, PlayersCount, false);
            }
        }
    }
}
