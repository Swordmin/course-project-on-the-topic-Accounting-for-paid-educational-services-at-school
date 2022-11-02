using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Word = Microsoft.Office.Interop.Word;
//using Excel = Microsoft.Office.Interop.Excel;

using System.Windows.Forms;
using System.Data;

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
