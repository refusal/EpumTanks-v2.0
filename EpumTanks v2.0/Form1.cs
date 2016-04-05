using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace EpumTanks_v2._0
{
    public partial class Form1 : Form
    {
        public const int WIDTH = 900;
        public const int HEIGHT = 500;
        private static GameManager gameManager;
        private static PictureBox[,] pictureBox;
        private Button button1 = new Button();
        private Thread gameThread;

        public Form1(GameManager _gameManager)
        {
            gameManager = _gameManager;
            InitializeField(gameManager.field.cells.GetLength(0), gameManager.field.cells.GetLength(1));
            InitializeComponent();
            gameThread = new Thread(gameManager.Run);
            
        }
        private  void button1_Click(object sender, EventArgs e)
        {
            gameThread.Start();   
        }
        public static void ShowWinner(string text)
        {
            MessageBox.Show("Winner is " + text);
        }
        private void InitializeField(int width, int height)
        {
            pictureBox = new PictureBox[width, height];

            int wsize = ((WIDTH - 120) / width);
            int hsize = (HEIGHT / height);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    pictureBox[i, j] = new PictureBox();

                    if (gameManager.field.cells[i, j].Tank != null) { pictureBox[i, j].BackColor = Color.Black; }
                    else { pictureBox[i, j].BackColor = Color.White; }

                    pictureBox[i, j].Location = new Point(i * wsize, j * hsize);
                    pictureBox[i, j].Size = new Size(wsize - 1, hsize - 1);
                    Controls.Add(pictureBox[i, j]);
                }
            }
        }


        private void InitializeComponent()
        {
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(WIDTH - 100, HEIGHT - 75);
            this.button1.Size = new System.Drawing.Size(75, 50);
            this.button1.Text = "Run";
            this.button1.Click += new System.EventHandler(this.button1_Click);

            this.FormClosing += Form1_FormClosing;

            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(WIDTH, HEIGHT);
            this.Controls.Add(this.button1);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            gameThread.Abort();
        }

        public  void SetBullet(int i, int j)
        {
           
        }

        public void ClearBullet(int i, int j)
        {
            pictureBox[i, j].BackColor = Color.White;
            UpdatePictureBox(pictureBox[i,j]);
                }

        static void UpdatePictureBox(PictureBox picture )
        {
            if (!picture.IsDisposed)
            {

                if (picture.InvokeRequired)
                {
                    
                        picture.Invoke(new MethodInvoker(
                        delegate ()
                        {
                            picture.Update();
                        }
                        ));
                    
                }
                else
                {
                    picture.Update();
                }
            }
        }

        public static void View( )
        {
            int width = gameManager.field.cells.GetLength(0);
            int height = gameManager.field.cells.GetLength(1);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (gameManager.field.cells[i, j].Bullet != null)
                    {
                        pictureBox[i, j].BackColor = Color.Violet;
                        UpdatePictureBox(pictureBox[i, j]);
                    }

                    if (gameManager.field.cells[i, j].Tank != null)
                    {
                        pictureBox[i, j].BackColor = Color.Black;
                        UpdatePictureBox(pictureBox[i, j]);
                    }
                }
            }
            Thread.Sleep(100);
        }

        public static  void RemoveView()
        {
            int width = gameManager.field.cells.GetLength(0);
            int height = gameManager.field.cells.GetLength(1);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (pictureBox[i, j].BackColor != Color.White)
                    {
                        pictureBox[i, j].BackColor = Color.White;
                        
                    }
                }
            }
        }
    }
}
