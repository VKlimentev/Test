using System.Drawing;

namespace Clonium
{
    internal class Cell
    {
        public Chip Chip { get; set; } = new Chip();
        public Point Coordinates { get; set; } = new Point();
        public bool Activate { get; set; } = true;

    }
}