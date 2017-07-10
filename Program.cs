using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AllegroFinder
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm form = new MainForm();
            
            if (form.IsInitialized)
            {
                Application.Run(form);
            }
            else
            {
                form.Close();
            }
        }
    }
}
