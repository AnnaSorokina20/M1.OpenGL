using System;
using System.Windows.Forms;

namespace Task_7
{
    internal static class Program
    {
        static Program() => DesignMode = true;
        public static bool DesignMode { get; set; }

        [STAThread]
        static void Main(string[] args)
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length > 0)
            {
                switch (args[0])
                {
                    case "/c": 
                        Application.Run(new SettingsForm());
                        break;
                    case "/s": 
                        Application.Run(new MainForm(true));
                        break;
                    case "/p": 
                        Application.Run(new MainForm(false));
                        break;
                    default:
                        MessageBox.Show("Unsupported command line option.");
                        break;
                }
            }
            else
            {
                Application.Run(new MainForm(false));
            }
        }
    }
}