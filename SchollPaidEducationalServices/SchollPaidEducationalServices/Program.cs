using System;

using System.Windows.Forms;

namespace SchollPaidEducationalServices
{
    internal  class Program
    {
 
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    

    }
}
