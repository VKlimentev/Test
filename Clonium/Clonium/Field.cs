using System.Linq;
using System.Drawing;
using System.Collections.Generic;

namespace Clonium
{
    internal class Field
    {
        private Cell[,] _cells;
        private int _dimension;

        public Field(int dimension)
        {
            _dimension = dimension;
            InitializeCells();
        }
        public Cell[,] Cells
        {
            get { return _cells; }
            set { _cells = value; }
        }
        public int Dimension
        {
            get { return _dimension; }
            set { _dimension = value; }
        }

        private void InitializeCells()
        {
            _cells = new Cell[_dimension, _dimension];

            for (int i = 0; i < _dimension; i++)
            {
                for (int j = 0; j < _dimension; j++)
                {
                    _cells[i, j] = new Cell();
                }
            }
        }

        public int GetValueChip(int row, int col)
        {
            return _cells[row, col].Chip.Value;
        }
        public Color GetColorChip(int row, int col)
        {
            return _cells[row, col].Chip.Color;
        }

        public void AddValueChip(Color color, int row, int col)
        {
            if (
                row < 0 ||
                row >= _dimension ||
                col < 0 ||
                col >= _dimension ||
                !_cells[row, col].Activate
               )
                return;

            _cells[row, col].Chip.Value++;
            _cells[row, col].Chip.Color = color;

        }

        public bool FindCellByValue(int value, out int row, out int col)
        {
            for (int i = 0; i < _dimension; i++)
            {
                for (int j = 0; j < _dimension; j++)
                {
                    if (GetValueChip(i, j) >= value)
                    {
                        row = i;
                        col = j;
                        return true;
                    }
                }
            }

            row = -1;
            col = -1;
            return false;
        }
        public bool FindCellByCoordinates(Point point, out int row, out int col)
        {
            for (int i = 0; i < _dimension; i++)
            {
                for (int j = 0; j < _dimension; j++)
                {
                    if (_cells[i, j].Coordinates == point)
                    {
                        row = i;
                        col = j;
                        return true;
                    }
                }
            }

            row = -1;
            col = -1;
            return false;
        }

        public IEnumerable<Color> GetDistinctColors()
        {
            return _cells
                .OfType<Cell>()
                .Where(cell => cell.Chip.Color != Color.Empty)
                .Select(cell => cell.Chip.Color)
                .Distinct();
        }
        public IEnumerable<Cell> GetRedCells()
        {
            return _cells
                .OfType<Cell>()
                .Where(cell => cell.Chip.Color == Color.Red && cell.Chip.Value > 0);
        }
    }
}
