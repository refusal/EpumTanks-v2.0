using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EpumTanks_v2._0
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            GameManager gameManager = new GameManager();
            gameManager.CreateField(60, 60);
            gameManager.CreateTanks();
            gameManager.AfterTickEvent += Form1.RemoveView;
            gameManager.AfterTickEvent += Form1.View;
            gameManager.AfterEndEvent += Form1.ShowWinner;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(gameManager));
            
        }
       
    }
}
