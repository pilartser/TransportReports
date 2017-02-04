﻿using System;
using System.Windows.Forms;

namespace TransportReports
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length == 3)
            {
                Application.Run(new Main(args[0], args[1], args[2]));
            }
            else if (args.Length == 4)
            {
                Application.Run(new Main(args[0], args[1], args[2], args[3]));
            }
            else
                MessageBox.Show("Неверное количество параметров");
        }
    }
}
