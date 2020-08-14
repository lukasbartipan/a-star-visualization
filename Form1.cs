using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AStar
{
    public partial class Form1 : Form
    {
        public static Graphics g;

        public static int rows = 25;
        public static int cols = 25;

        public Spot[,] array = new Spot[rows, cols];

        public List<Spot> openSet = new List<Spot>();
        public List<Spot> closeSet = new List<Spot>();
        public List<Spot> path = new List<Spot>();

        public Spot start;
        public Spot end;

        public static double w;
        public static double h;

        public Form1()
        {
            InitializeComponent();

            g = pictureBox1.CreateGraphics();
            w = pictureBox1.Width / cols;
            h = pictureBox1.Height / rows;

            for (int i = 0; i < rows; i++)            
            {
                for (int j = 0; j < cols; j++)
                {
                    array[i, j] = new Spot(i, j);
                }
            }

            start = array[0, 0];
            end = array[cols - 1, rows - 1];

            end.wall = false;
            start.wall = false;

            openSet.Add(start);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    array[i, j].addNeighbors(this.array);
                }
            }
        }

        public void removeFromArray(List<Spot> arr, Spot s)
        {
            for (int i = arr.Count - 1; i >= 0; i--)
            {
                if (arr[i] == s)
                {
                    arr.Remove(s);
                }
            }
        }

        public double heuristic(Spot a, Spot b)
        {
            //double distance = (Math.Sqrt(Math.Pow(Math.Abs(a.x - b.x), 2) + Math.Pow(Math.Abs(a.y - b.y), 2)));
            double distance = Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
            return distance;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    array[i, j].Draw(e.Graphics);
                    if (array[i, j].wall)
                    {
                        array[i, j].Fill(e.Graphics, Brushes.Black);
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int winner = 0;
            for (int i = 0; i < openSet.Count; i++)
            {
                if (openSet[i].f < openSet[winner].f)
                {
                    winner = i;
                }
            }

            Spot current = openSet[winner];
            Spot temp = current;
            
            if (openSet[winner] == end)
            {
                
                path.Add(temp);
                while (temp.previous != null)
                {
                    path.Add(temp.previous);
                    temp = temp.previous;
                }

                Debug.WriteLine("DONE!");
                timer1.Enabled = false;
            }

            removeFromArray(openSet, current);
            closeSet.Add(current);

            List<Spot> neighbors = current.neighbors;
            for (int i = 0; i < neighbors.Count; i++)
            {
                Spot neighbor = neighbors[i];

                if (!closeSet.Exists(u => u == neighbor) && !neighbor.wall)
                {
                    double tempG = current.g + 1;

                    if (openSet.Exists(u => u == neighbor))
                    {
                        if (tempG < neighbor.g)
                        {
                            neighbor.g = tempG;
                        }
                    }
                    else
                    {
                        neighbor.g = tempG;
                        openSet.Add(neighbor);
                    }

                    neighbor.h = heuristic(neighbor, end);
                    neighbor.f = neighbor.g + neighbor.h;
                    neighbor.previous = current;
                }
            }



            foreach (Spot item in openSet)
            {
                item.Fill(g, Brushes.Green);
            }

            foreach (Spot item in closeSet)
            {
                item.Fill(g, Brushes.Red);
            }

            foreach (Spot item in path)
            {
                item.Fill(g, Brushes.Blue);
            }
        }
    }
}
