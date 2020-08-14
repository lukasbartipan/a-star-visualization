using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AStar
{
    public class Spot
    {
        public int x { get; set; }

        public int y { get; set; }

        public double width { get; set; }

        public double height { get; set; }

        public double f { get; set; }

        public double g { get; set; }

        public double h { get; set; }

        public Spot previous;

        public bool wall = false;

        public List<Spot> neighbors = new List<Spot>();

        public Spot(int i, int j) {
            this.x = i;
            this.y = j;
            this.width = Form1.w;
            this.height = Form1.h;

            Random rand = new Random();
            double result = rand.Next(100);
            System.Threading.Thread.Sleep(2);
            Debug.WriteLine(result);
            if (result < 10)
                this.wall = true;  
        }

        public void addNeighbors(Spot[,] array)
        {
            if (this.x < Form1.cols - 1)
            {
                this.neighbors.Add(array[this.x + 1, this.y]);
            }
            if (this.x > 0)
            {
                this.neighbors.Add(array[this.x - 1, this.y]);
            }
            if (this.y < Form1.rows - 1)
            {
                this.neighbors.Add(array[this.x, this.y + 1]);
            }
            if (this.y > 0)
            {
                this.neighbors.Add(array[this.x, this.y - 1]);
            }
        }

        public void Draw(Graphics g)
        {
            g.DrawRectangle(Pens.Black, x * (float)this.width, y * (float)this.height, (float)this.width - 1, (float)this.height - 1);
        }

        public void Fill(Graphics g, Brush b)
        {
            g.FillRectangle(b, x * (float)this.width, y * (float)this.height, (float)this.width - 1, (float)this.height - 1);
        }
    }
}
