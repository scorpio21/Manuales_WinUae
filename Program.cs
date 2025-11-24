using System;
using System.Windows.Forms;

namespace WinFormsManual
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new FormManual());
        }
    }
}
