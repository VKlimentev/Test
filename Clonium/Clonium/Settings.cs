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
        private static int _playersCount;

        public Settings(string fieldName, int playersCount)
        {
            _fieldName = fieldName;
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

            foreach (Control control in Fields.Controls)
            {
                if (((RadioButton)control).Name == fieldName)
                {
                    ((RadioButton)control).Checked = true;
                }
            }
            FieldRadioButtons_CheckedChanged(new object(), new EventArgs());
        }
        private void InitializeResourse()
        {
            _mapImages = new Bitmap[5, 2];
            int size = 90;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    _mapImages[i, j] = Painter.CropImage(Resource.Maps, j * size, i * size, size, size);
                }
            }
        }
        private void InitializeImages()
        {
            Fields.Controls[0].BackgroundImage = _mapImages[0, 1];
            for (int i = 1; i < 5; i++)
            {
                Fields.Controls[i].BackgroundImage = _mapImages[i, 0];
            }
        }

        private void FieldRadioButtons_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                Fields.Controls[i].BackgroundImage = ((RadioButton)Fields.Controls[i]).Checked ? _mapImages[i, 1] : _mapImages[i, 0];
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            int playersCount = 1;
            string fieldName = "fieldName1";

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

            this.Close();

            if (_fieldName != fieldName || _playersCount != playersCount)
                Controller.Initialize(fieldName, playersCount, false);
        }
    }
}
