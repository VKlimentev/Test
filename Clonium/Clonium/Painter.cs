using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Clonium
{
    internal static class Painter
    {
        private static MainForm _mainForm;
        private static int _cellSize;
        private static int _marginLeft;
        private static int _marginTop;

        private static Bitmap[,] _chipImages;
        private static Dictionary<Color, int> _colors;

        public static Field Field { get; set; }


        public static void Initialize()
        {
            _mainForm = new MainForm();
            _mainForm.ClientSizeChanged += new EventHandler(MainForm_ClientSizeChanged);

            InitializeResource();
            InitializeColors();
            DrawField();

            _mainForm.ShowDialog();
        }


        public static Bitmap CropImage(Bitmap sourceImage, int x, int y, int width, int height)
        {
            Bitmap croppedImage = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(croppedImage))
            {
                g.DrawImage(sourceImage, new Rectangle(0, 0, width, height), new Rectangle(x, y, width, height), GraphicsUnit.Pixel);
            }

            return croppedImage;
        }
        private static void InitializeResource()
        {
            int size = 90;

            _chipImages = new Bitmap[4, 4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    _chipImages[i, j] = CropImage(Resource.Chips, i * size, j * size, size, size);
                }
            }
        }
        private static void InitializeColors()
        {
            _colors = new Dictionary<Color, int>()
            {
                { Color.Empty, 0 },
                { Color.Blue, 0 },
                { Color.Red, 1 },
                { Color.Green, 2 },
                { Color.Purple, 3 }
            };
        }


        private static void CalculateSizeAndIndents()
        {
            int formHeight = _mainForm.ClientSize.Height;
            int formWidth = _mainForm.ClientSize.Width;

            _cellSize = Math.Min(formHeight - 25, formWidth) / Field.Dimension;
            _marginLeft = (formWidth - Field.Dimension * _cellSize) / 2;
            _marginTop = (24 + formHeight - Field.Dimension * _cellSize) / 2;
        }
        public static void DrawField()
        {
            _mainForm.Controls.Clear();
            Field = Controller.Field;
            InitializeMenuStrip();
            CalculateSizeAndIndents();

            for (int i = 0; i < Field.Dimension; i++)
            {
                for (int j = 0; j < Field.Dimension; j++)
                {
                    Button button = new Button();
                    if (!Field.Cells[i, j].Activate)
                    {
                        button.Enabled = false;
                        button.Visible = false;
                    }
                    button.FlatAppearance.BorderSize = 0;
                    button.FlatStyle = FlatStyle.Popup;
                    button.Location = new Point(j * _cellSize + _marginLeft, i * _cellSize + _marginTop);
                    button.Size = new Size(_cellSize, _cellSize);
                    button.BackgroundImageLayout = ImageLayout.Zoom;
                    button.BackgroundImage = _chipImages[Field.GetValueChip(i, j), _colors[Field.GetColorChip(i, j)]];
                    button.Click += Cell_Click;
                    _mainForm.Controls.Add(button);

                    Field.Cells[i, j].Coordinates = button.Location;
                }
            }
        }
        public static void RefreshField()
        {
            CalculateSizeAndIndents();
            for (int i = 0, k = 1; i < Field.Dimension; i++)
            {
                for (int j = 0; j < Field.Dimension; j++, k++)
                {
                    if (_mainForm.Controls[k] is Button button)
                    {
                        button.Size = new Size(_cellSize, _cellSize);
                        button.Location = new Point(j * _cellSize + _marginLeft, i * _cellSize + _marginTop);
                        button.BackgroundImage = _chipImages[Field.GetValueChip(i, j), _colors[Field.GetColorChip(i, j)]];
                        Field.Cells[i, j].Coordinates = button.Location;
                    }
                }
            }
        }


        private static void Cell_Click(object sender, EventArgs e)
        {
            Controller.HandleCellClick(((Button)sender).Location);
        }

        private static void MainForm_ClientSizeChanged(object sender, EventArgs e)
        {
            RefreshField();
        }
        private static void InitializeMenuStrip()
        {
            MenuStrip menuStrip = new MenuStrip();

            ToolStripMenuItem Game = new ToolStripMenuItem();
            ToolStripMenuItem NewGame = new ToolStripMenuItem();
            ToolStripMenuItem Settings = new ToolStripMenuItem();
            ToolStripMenuItem Exit = new ToolStripMenuItem();
            // 
            // menu
            // 
            menuStrip.SuspendLayout();
            menuStrip.BackColor = Color.Transparent;
            menuStrip.Items.AddRange(new ToolStripItem[] {
            Game});
            menuStrip.Location = new Point(0, 0);
            menuStrip.Size = new Size(684, 24);
            // 
            // Game
            // 
            Game.DropDownItems.AddRange(new ToolStripItem[] {
            NewGame,
            Settings,
            Exit});
            Game.ForeColor = SystemColors.Window;
            Game.Size = new Size(46, 20);
            Game.Text = "Игра";
            // 
            // NewGame
            // 
            NewGame.Size = new Size(180, 22);
            NewGame.Text = "Новая игра";
            NewGame.Click += new EventHandler(NewGame_Click);
            // 
            // Settings
            // 
            Settings.Size = new Size(180, 22);
            Settings.Text = "Параметры";
            Settings.Click += new EventHandler(Settings_Click);
            // 
            // Exit
            // 
            Exit.Size = new Size(180, 22);
            Exit.Text = "Выход";
            Exit.Click += new EventHandler(ExitToolStripMenuItem_Click);

            _mainForm.Controls.Add(menuStrip);

            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
        }
        private static void NewGame_Click(object sender, EventArgs e)
        {
            Controller.Initialize(Controller.FieldName, Controller.PlayersCount, false);
        }
        private static void Settings_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings(Controller.FieldName, Controller.PlayersCount);
            settings.Show();
        }
        private static void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
