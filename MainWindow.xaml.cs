using System;
using System.Reflection;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using Microsoft.Win32;
using System.Threading;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using System.Collections;
using System.IO;
using System.Windows.Forms;

namespace wg_confGen
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string wg_exe = "C:\\Program Files\\WireGuard\\wg.exe";

        public MainWindow()
        {
            if (!File.Exists(wg_exe))
            {
                System.Windows.Forms.MessageBox.Show(
                "wgcg",
                "wg.exe not found, Wireguard not installed?\n\nPath = \"" + wg_exe + "\"",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);

                Environment.Exit(1);
            }

            InitializeComponent();
        }

        //# # # # # # # # # # # # # # # # #

        private static UInt32 Clients = 0;

        //# # # # # # # # # # # # # # # # #

        private void Debug(object sender, RoutedEventArgs e)
        {

            

        }

        //# # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # #

        ///<summary>Launches executables.</summary>
        ///<exception cref="FileLoadException"></exception>
        private static (String, Int32) EXE(String Path, String Args = null, Boolean RunAs = false, Boolean InternalOutputRedirect = false, Boolean HiddenExecute = false, Boolean WaitForExit = false, String WorkingDirectory = null, Boolean RedirectErrors = false)
        {
            Process process = new();

            process.StartInfo.FileName = Path;

            if (RunAs)
            {
                process.StartInfo.Verb = "runas";
            }

            if (HiddenExecute)
            {
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.CreateNoWindow = true;
            }

            if (InternalOutputRedirect)
            {
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;

                if (RedirectErrors)
                {
                    process.StartInfo.RedirectStandardError = true;
                }
                else
                {
                    process.StartInfo.RedirectStandardError = false;
                }
            }
            else
            {
                process.StartInfo.RedirectStandardOutput = false;
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.RedirectStandardError = false;
            }

            if (WorkingDirectory != null)
            {
                process.StartInfo.WorkingDirectory = WorkingDirectory;
            }

            if (Args != null)
            {
                process.StartInfo.Arguments = Args;
            }

            process.Start();

            if (InternalOutputRedirect)
            {
                String Output = process.StandardOutput.ReadToEnd();

                process.WaitForExit();

                if (RedirectErrors)
                {
                    Output += process.StandardError.ReadToEnd();
                }

                return (Output, process.ExitCode);
            }
            else if (WaitForExit)
            {
                process.WaitForExit();

                return (null, process.ExitCode);
            }

            return (null, Int32.MaxValue - 1);
        }
    }
}
