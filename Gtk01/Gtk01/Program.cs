using System;
using Gtk;

namespace Gtk01
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Application.Init();
            MainWindow win = new MainWindow();
            win.Show();
            // ("Hello World\n");
            Application.Run();
        }
    }
}
