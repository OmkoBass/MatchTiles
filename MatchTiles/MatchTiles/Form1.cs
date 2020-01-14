using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchTiles
{
    public partial class Form1 : Form
    {
        static readonly int SIZE = 12;
        readonly int difference = 150;
        int X = 50;
        int Y = 50;

        Button[] tiles = new Button[SIZE];
        int[] numbers = new int[SIZE];

        int first = 0;
        int index = -1;
        public Form1()
        {
            InitializeComponent();


            Random rand = new Random();
            
            for(int i = 0; i < SIZE / 2; i++)
            {
                numbers[i] = rand.Next(1, 101);

                numbers[i + 6] = numbers[i];
            }

            //Schuffle
            for(int i = 0; i < SIZE / 2; i++)
            {
                int schuffle = rand.Next(0, 11);
                int temp = numbers[i];
                numbers[i] = numbers[schuffle];
                numbers[schuffle] = temp;
            }

            for(int i = 0; i < SIZE; i++)
            {
                tiles[i] = new Button();
                tiles[i].Size = new Size(120, 120);
                tiles[i].Location = new Point(X, Y);
                X += SIZE + difference;
                if (i == 3) { Y += SIZE + difference; X = 50; }
                if (i == 7) { Y += SIZE + difference; X = 50; }

                this.Controls.Add(tiles[i]);

                int index = i;

                tiles[index].Click += (sender, args) => TileClick(tiles[index], index);
            }
        }

        private void TileClick(Button tile, int i)
        {
            if(first == 0)
            {
                tile.Text = numbers[i].ToString();
                first = numbers[i];
                index = i;
            }
            else
            {
                if(first == numbers[i])
                {
                    tile.Text = first.ToString();
                    first = 0;
                    tile.Enabled = false;
                    tiles[index].Enabled = false;
                }
                else
                {
                    first = 0;
                    tiles[index].Text = "";
                }
            }

            Victory();
        }

        private void Victory()
        {
            for(int i = 0; i < SIZE; i++)
            {
                if (tiles[i].Enabled == true)
                    return;
            }

            MessageBox.Show("Victory!");
            System.Environment.Exit(0);
        }
    }
}
