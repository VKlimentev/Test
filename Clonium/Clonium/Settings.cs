using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Clonium
{
    public partial class Settings : Form
    {
        private static Bitmap[,] _mapImages;
        private static string _fieldName;
        private static int _fieldSize;
        private static int _playersCount;

        public Settings(string fieldName, int fieldSize, int playersCount)
        {
            _fieldName = fieldName;
            _fieldSize = fieldSize;
            _playersCount = playersCount;

            InitializeComponent();
            InitializeResourse();
            InitializeImages();

            foreach (Control control in numberOfPlayers.Controls)
            {
                if (((RadioButton)control).Text == playersCount.ToString())
                {
                    ((RadioButton)control).Checked = true;
                }
            }

            FieldSizes.Text = fieldSize + "x" + fieldSize;

            foreach (Control control in Fields.Controls)
            {
                if (((RadioButton)control).Name == fieldName)
                {
                    ((RadioButton)control).Checked = true;
                }
            }
            FieldRadioButtons_CheckedChanged(new object(), new EventArgs());
        }
        public Bitmap CropImage(Bitmap sourceImage, int x, int y, int width, int height)
        {
            Bitmap croppedImage = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(croppedImage))
            {
                g.DrawImage(sourceImage, new Rectangle(0, 0, width, height), new Rectangle(x, y, width, height), GraphicsUnit.Pixel);
            }

            return croppedImage;
        }
        private void InitializeResourse()
        {
            _mapImages = new Bitmap[4, 2];
            int size = 128;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    _mapImages[i, j] = CropImage(Resource.Maps, j * size, i * size, size, size);
                }
            }
        }
        private void InitializeImages()
        {
            Fields.Controls[0].BackgroundImage = _mapImages[0, 1];
            for (int i = 1; i < 4; i++)
            {
                Fields.Controls[i].BackgroundImage = _mapImages[i, 0];
            }
        }

        private void FieldRadioButtons_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                Fields.Controls[i].BackgroundImage = ((RadioButton)Fields.Controls[i]).Checked ? _mapImages[i, 1] : _mapImages[i, 0];
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            int playersCount = 1;
            string fieldName = "fieldName1";
            int fieldSize = 6;

            foreach (Control control in numberOfPlayers.Controls)
            {
                if (((RadioButton)control).Checked)
                    playersCount = Convert.ToInt32(((RadioButton)control).Text);
            }

            foreach (Control control in Fields.Controls)
            {
                if (((RadioButton)control).Checked)
                    fieldName = ((RadioButton)control).Name;
            }

            fieldSize = int.Parse(FieldSizes.Text.Split('x')[0]);

            this.Close();

            if (_fieldName != fieldName || _fieldSize != fieldSize || _playersCount != playersCount)
                Controller.Initialize(fieldName, fieldSize, playersCount, false);
        }
    }
}
