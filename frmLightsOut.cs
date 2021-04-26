using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace allpay
{
    public partial class frmLightsOut : Form
    {
        private const int _cells = 5; // Number of cells in grid 
        private bool[,] _lightsOutGrid; // Stores on/off state of cells in grid 5

        private const int _lightsOutGridOffset = 25; // Distance from upper-left side of window 
        private const int _lightsOutGridLength = 300; // Size in pixels of grid 
             
        private Random _rand; // Used to generate random number
        public frmLightsOut()
        {
            InitializeComponent();
            _rand = new Random(); // Initialises random number generator 
            _lightsOutGrid = new bool[_cells, _cells];

            // Turn an element within the grid on or off depending
            // on Random Number that has been generated
            for (int row = 0; row < _cells; row++)
            {
                for (int col = 0; col < _cells; col++)
                {
                    _lightsOutGrid[row, col] = _rand.Next(2) == 1 ? true : false;
                }
            }

        }

        private void frmLightsOut_Paint(object sender, PaintEventArgs e)
        {
 
            Graphics g = e.Graphics;
            for (int row = 0; row < _cells; row++)
            {
                for (int col = 0; col < _cells; col++)
                {
                    // Get proper pen and brush for on/off 
                    // grid section 
                    Brush brush;
                    Pen pen;

                    if (_lightsOutGrid[row, col])
                    {
                        pen = Pens.Black;
                        brush = Brushes.Green; // On 
                    }
                    else
                    {
                        pen = Pens.Black;
                        brush = Brushes.LightGreen; // Off 
                    }

                    // Determine (x,y) coord of row and col to draw rectangle 
                    int RectSize = _lightsOutGridLength / _cells;
                    int x = col * RectSize + _lightsOutGridOffset;
                    int y = row * RectSize + _lightsOutGridOffset;

                    g.DrawRectangle(pen, x, y, RectSize, RectSize); // Rectangle outline 
                    g.FillRectangle(brush, x + 1, y + 1, RectSize - 1, RectSize - 1); // Solid rectangle 
                }
            }

        }

        private void frmLightsOut_MouseDown(object sender, MouseEventArgs e)
        {
            // Find row, col of mouse press 
            int rectSize = _lightsOutGridLength / _cells;
            int r = (e.Y - _lightsOutGridOffset) / rectSize;
            int c = (e.X - _lightsOutGridOffset) / rectSize;
            // Invert selected box and all surrounding boxes 4
            for (int i = r - 1; i <= r + 1; i++)
            {
                for (int j = c - 1; j <= c + 1; j++)
                {
                    if (i >= 0 && i < _cells && j >= 0 && j < _cells)
                    {
                        _lightsOutGrid[i, j] = !_lightsOutGrid[i, j];
                    }
                }
            }
            // Redraw grid 
            this.Invalidate();
            // Check to see if puzzle has been solved
            if (PlayerWon())
            {
                // Display winner dialogue box just inside window 
                MessageBox.Show(this, "Congratulations! You've won!", "Lights Out!",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool PlayerWon()
        {
            for (int row = 0; row < _cells; row++)
            {
                for (int col = 0; col < _cells; col++)
                {
                    if (_lightsOutGrid[row, col])
                    {
                        return false;
                    }
                    
                }

            }

            return true;

        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            // Fill grid with either Green (On) or LightGreen (Off)
            for (int row = 0; row < _cells; row++)
            {
                for (int col = 0; col < _cells; col++)
                {
                    _lightsOutGrid[row, col] = _rand.Next(2) == 1 ? true : false;
                }
            }
            // Redraw grid 
            this.Invalidate();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
