using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{

    public partial class Form1 : Form
    {
        Bitmap off;
        List<Column> LC = new List<Column>();
        List<Block> LB = new List<Block>();
        bool iDrag = false;
        int myIndex = -1;
        int prevX, prevY;
        bool isHit = false;
        int myColumn = 0;
        Color[] clr = { Color.Green, Color.Red };
        bool clrS = false;
        public Form1()
        {
            InitializeComponent();
            this.MouseDown += new MouseEventHandler(Form1_MouseDown);
            this.MouseMove += new MouseEventHandler(Form1_MouseMove);
            this.MouseUp += new MouseEventHandler(Form1_MouseUp);
            this.WindowState = FormWindowState.Maximized;
            this.Paint += Form1_Paint;
            this.Text = " Assignment 8";
            this.KeyDown += Form1_KeyDown;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDouble(this.CreateGraphics());
        }

        void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            iDrag = false;

            if (isHit)
            {
                for (int c = 0; c < 3; c++)
                {

                    if (e.X > (LC[c].X - LC[c].W - ClientSize.Width / 50.0f) &&
                        e.X < (LC[c].X + LC[c].W + ClientSize.Width / 50.0f) &&
                        e.Y > LC[c].Y)
                    {

                        if (LC[c].myBlocks.Count > 0)
                        {

                            if (LC[myColumn].myBlocks[LC[myColumn].myBlocks.Count - 1].index >= LC[c].myBlocks[LC[c].myBlocks.Count - 1].index /* + 1 */ && c != myColumn)
                            {
                                Block pnn = new Block();
                                pnn.X = LC[c].myBlocks[LC[c].myBlocks.Count - 1].X + ((ClientSize.Width / 168f) * (LC[myColumn].myBlocks[LC[myColumn].myBlocks.Count - 1].index - LC[c].myBlocks[LC[c].myBlocks.Count - 1].index /* + 1*/));
                                pnn.Y = LC[c].myBlocks[LC[c].myBlocks.Count - 1].Y - ClientSize.Height / 38;
                                pnn.W = LC[myColumn].myBlocks[LC[myColumn].myBlocks.Count - 1].W;
                                pnn.H = ClientSize.Height / 39.5f;
                                pnn.index = LC[myColumn].myBlocks[LC[myColumn].myBlocks.Count - 1].index;
                                LC[myColumn].myBlocks.RemoveAt(LC[myColumn].myBlocks.Count - 1);
                                LC[c].myBlocks.Add(pnn);
                                break;
                            }


                        }
                        else
                        {
                            Block pnn = new Block();
                            pnn.X = LC[c].X - LC[myColumn].myBlocks[LC[myColumn].myBlocks.Count - 1].W / 2 + ClientSize.Width / 336f;
                            pnn.Y = LC[c].Y + ClientSize.Height / 3.62f;
                            pnn.W = LC[myColumn].myBlocks[LC[myColumn].myBlocks.Count - 1].W;
                            pnn.H = ClientSize.Height / 39.5f;
                            pnn.index = LC[myColumn].myBlocks[LC[myColumn].myBlocks.Count - 1].index;
                            LC[myColumn].myBlocks.RemoveAt(LC[myColumn].myBlocks.Count - 1);
                            LC[c].myBlocks.Add(pnn);
                            break;
                        }

                    }
                    else
                    {
                        if (LC[myColumn].myBlocks.Count > 1)
                        {
                            LC[myColumn].myBlocks[LC[myColumn].myBlocks.Count - 1].X = LC[myColumn].myBlocks[LC[myColumn].myBlocks.Count - 2].X + (ClientSize.Width / 168f * (LC[myColumn].myBlocks[LC[myColumn].myBlocks.Count - 1].index - LC[myColumn].myBlocks[LC[myColumn].myBlocks.Count - 2].index /* + 1*/));
                            LC[myColumn].myBlocks[LC[myColumn].myBlocks.Count - 1].Y = LC[myColumn].myBlocks[LC[myColumn].myBlocks.Count - 2].Y - ClientSize.Height / 38;
                            //break;
                        }
                        else
                        {
                            LC[myColumn].myBlocks[LC[myColumn].myBlocks.Count - 1].X = LC[myColumn].X - LC[myColumn].myBlocks[LC[myColumn].myBlocks.Count - 1].W / 2 + ClientSize.Width / 336f;
                            LC[myColumn].myBlocks[LC[myColumn].myBlocks.Count - 1].Y = LC[myColumn].Y + ClientSize.Height / 3.62f;
                            //break;
                        }
                    }
                }

            }
            DrawDouble(this.CreateGraphics());
            isHit = false;
        }

        void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                for (int c = 0; c < 3; c++)
                {
                    for (int i = 0; i < LC[c].myBlocks.Count; i++)
                    {
                        if (e.X > LC[c].myBlocks[i].X && e.Y > LC[c].myBlocks[i].Y &&
                            e.X < (LC[c].myBlocks[i].X + LC[c].myBlocks[i].W) && e.Y < (LC[c].myBlocks[i].H + LC[c].myBlocks[i].Y) /*&& (CurrentMove1 == LC[c].myBlocks[i].index)*/)////
                        {
                            if (LC[c].myBlocks.Count > 1)
                            {
                                if (LC[c].myBlocks[i].index >= LC[c].myBlocks[LC[c].myBlocks.Count - 2].index + 1)
                                {
                                    clrS = true;
                                    myIndex = LC[c].myBlocks[i].index;
                                    iDrag = true;
                                    prevX = e.X;
                                    prevY = e.Y;
                                    isHit = true;
                                    myColumn = c;
                                    //MessageBox.Show("" + myColumn + "    " +  myIndex);
                                    //break;
                                }
                                else
                                {
                                    clrS = false;
                                }
                            }
                            else if (LC[c].myBlocks.Count == 1)
                            {
                                clrS = true;
                                myIndex = LC[c].myBlocks[i].index;
                                iDrag = true;
                                prevX = e.X;
                                prevY = e.Y;
                                isHit = true;
                                myColumn = c;
                            }
                        }

                    }

                }
            }
        }

        void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            label4.Text = "X = " + e.X + "  Y = " + e.Y;
            if (iDrag)
            {
                int dx = e.X - prevX;
                int dy = e.Y - prevY;

                if (dx < 0)
                {
                    dx *= -1;
                }
                if (dy < 0)
                {
                    dy *= -1;
                }
                if (e.X > prevX)
                {
                    LC[myColumn].myBlocks[LC[myColumn].myBlocks.Count - 1].X += dx;
                    prevX = e.X;
                }
                if (e.X < prevX)
                {
                    LC[myColumn].myBlocks[LC[myColumn].myBlocks.Count - 1].X -= dx;
                    prevX = e.X;
                }
                if (e.Y > prevY)
                {
                    LC[myColumn].myBlocks[LC[myColumn].myBlocks.Count - 1].Y += dy;
                    prevY = e.Y;
                }
                if (e.Y < prevY)
                {
                    LC[myColumn].myBlocks[LC[myColumn].myBlocks.Count - 1].Y -= dy;
                    prevY = e.Y;
                }

                DrawDouble(this.CreateGraphics());
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            CreateActors();
        }

        void CreateActors()
        {
            float ax = ClientSize.Width / 3.0f;
            for (int i = 0; i < 3; i++)
            {
                Column pnn = new Column();
                pnn.X = ax;
                pnn.Y = ClientSize.Height / 1.522f;
                pnn.H = ClientSize.Height - pnn.Y - ClientSize.Height / 25.0f;
                pnn.W = ClientSize.Width / 168f;
                pnn.index = i;
                LC.Add(pnn);
                ax += ClientSize.Width / 6.0f;
            }

            for (int i = 0; i < 3; i++)
            {
                Column pnn = new Column();
                pnn.X = LC[i].X - ClientSize.Width / 16.8f;
                pnn.Y = LC[i].Y + LC[i].H;
                pnn.W = ClientSize.Width / 8.45f + LC[i].W;
                pnn.H = ClientSize.Height - pnn.Y;
                LC.Add(pnn);

            }

            float bx = ClientSize.Width / 17.62f;
            float by = ClientSize.Height / 3.62f;

            for (int i = 0; i < 10; i++)
            {
                Block pnn = new Block();
                pnn.X = LC[0].X - bx;
                pnn.Y = LC[0].Y + by;
                pnn.W = bx * 2 + LC[0].W;
                pnn.H = ClientSize.Height / 39.5f;
                pnn.index = i;
                //LC[0].blocks++;
                //LB.Add(pnn);
                LC[0].myBlocks.Add(pnn);
                bx -= ClientSize.Width / 168f;
                by -= ClientSize.Height / 38;
            }
        }

        void DrawScene(Graphics g)
        {
            g.Clear(Color.White);

            if (LC[1].myBlocks.Count == 10 || LC[2].myBlocks.Count == 10)
            {
                label6.Text = "WINNER WINNER CHICKEN DINNER";
                label6.ForeColor = Color.Green;
            }
            else
            {
                label6.Text = "If you win, I will change :D";
                label6.ForeColor = Color.Black;
            }

            if (clrS)
            {
                SolidBrush br0 = new SolidBrush(clr[0]);
                g.FillEllipse(br0, ClientSize.Width / 2, ClientSize.Height / 7, ClientSize.Width / 34, ClientSize.Height / 20);
            }
            else
            {
                SolidBrush br0 = new SolidBrush(clr[1]);
                g.FillEllipse(br0, ClientSize.Width / 2, ClientSize.Height / 7, ClientSize.Width / 34, ClientSize.Height / 20);
            }

            SolidBrush br1 = new SolidBrush(Color.LightGray);
            SolidBrush br2 = new SolidBrush(Color.Yellow);
            SolidBrush br3 = new SolidBrush(Color.Black);
            Pen p1 = new Pen(Color.Black, 4);

            for (int i = 0; i < LC.Count; i++)
            {
                g.FillRectangle(br1, LC[i].X, LC[i].Y, LC[i].W, LC[i].H);
                g.DrawRectangle(p1, LC[i].X, LC[i].Y, LC[i].W, LC[i].H);
            }

            for (int i = 0; i < LC.Count; i++)
            {
                for (int j = 0; j < LC[i].myBlocks.Count; j++)
                {
                    g.FillRectangle(br2, LC[i].myBlocks[j].X, LC[i].myBlocks[j].Y, LC[i].myBlocks[j].W, LC[i].myBlocks[j].H);
                    g.DrawRectangle(p1, LC[i].myBlocks[j].X, LC[i].myBlocks[j].Y, LC[i].myBlocks[j].W, LC[i].myBlocks[j].H);
                    //g.DrawString("" + LC[i].myBlocks[j].index, this.Font, Brushes.Black, LC[i].myBlocks[j].X + (LC[i].myBlocks[j].W / 2 - 8), LC[i].myBlocks[j].Y + (LC[i].myBlocks[j].H / 4));
                }
            }



        }

        void DrawDouble(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }

        class Column
        {
            public float X, Y, W, H;
            public int index;
            public List<Block> myBlocks = new List<Block>();
        }
        class Block
        {
            public float X, Y, W, H;
            public int index;

        }
    }
}
