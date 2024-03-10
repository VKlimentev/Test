using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using Svg;
using System.IO;
using System.Resources;

namespace Clonium
{
    internal static class Painter
    {
        private static MainForm _mainForm;
        private static int _cellSize;
        private static int _marginLeft;
        private static int _marginTop;

        private static Image[,] _enableChipImages;
        private static Image[,] _disenableChipImages;
        private static Dictionary<Color, int> _colors;

        public static Field Field { get; set; }


        public static void Initialize()
        {
            _mainForm = new MainForm();
            _mainForm.ClientSizeChanged += new EventHandler(MainForm_ClientSizeChanged);
            _mainForm.FormClosing += new FormClosingEventHandler(MainForm_FormClosing);

            InitializeResource();
            InitializeColors();
            DrawField();

            _mainForm.ShowDialog();
        }


        private static void InitializeResource()
        {
            ResourceManager resourceManager = new ResourceManager("Clonium.Resource", typeof(Resource).Assembly);
            _enableChipImages = new Image[4, 4];
            _disenableChipImages = new Image[4, 4];

            for (int i = 0, k = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++, k++)
                {
                    string enableChipName = "enableChip" + k;
                    string disenableChipName = "disenableChip" + k;

                    _enableChipImages[j, i] = LoadSvgImage(resourceManager, enableChipName);
                    _disenableChipImages[j, i] = LoadSvgImage(resourceManager, disenableChipName);
                }
            }
        }
        private static Image LoadSvgImage(ResourceManager resourceManager, string resourceName)
        {
            using (Stream svgStream = new MemoryStream((byte[])resourceManager.GetObject(resourceName)))
            {
                using (StreamReader reader = new StreamReader(svgStream))
                {
                    string svgContent = reader.ReadToEnd();
                    return SvgDocument.FromSvg<SvgDocument>(svgContent).Draw();
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
                    if (Field.GetColorChip(i, j) == Controller.CurrentColor)
                        button.BackgroundImage = _enableChipImages[Field.GetValueChip(i, j), _colors[Field.GetColorChip(i, j)]];
                    else
                        button.BackgroundImage = _disenableChipImages[Field.GetValueChip(i, j), _colors[Field.GetColorChip(i, j)]];
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
                        if (Field.GetColorChip(i, j) == Controller.CurrentColor)
                            button.BackgroundImage = _enableChipImages[Field.GetValueChip(i, j), _colors[Field.GetColorChip(i, j)]];
                        else
                            button.BackgroundImage = _disenableChipImages[Field.GetValueChip(i, j), _colors[Field.GetColorChip(i, j)]];
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
            Controller.Initialize(Controller.FieldName, Controller.Field.Dimension, Controller.PlayersCount, false);
        }
        private static void Settings_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings(Controller.FieldName, Controller.FieldSize, Controller.PlayersCount);
            settings.Show();
        }
        private static void MainForm_FormClosing(object sender, EventArgs e)
        {
            GameConfigManager.SaveGameConfig(Controller.FieldName, Controller.Field.Dimension, Controller.PlayersCount);
        }

        private static void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameConfigManager.SaveGameConfig(Controller.FieldName, Controller.Field.Dimension, Controller.PlayersCount);
            Application.Exit();
        }
    }
}
