using System;
using System.Drawing;
using System.Windows.Forms;

namespace MouseMover
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 myForm = new Form1();
            Button cancelBTN = new Button();
            cancelBTN.Size = new Size(0, 0);
            cancelBTN.TabStop = false;
            myForm.Controls.Add(cancelBTN);
            myForm.CancelButton = cancelBTN;
            Application.Run(myForm);
        }
    }
}
