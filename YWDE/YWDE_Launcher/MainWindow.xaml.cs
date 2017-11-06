using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace YWDE_Launcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow ()
        {
            InitializeComponent ();
            ProcessStartInfo startInfo = new ProcessStartInfo ();
            startInfo.UseShellExecute = true;
            startInfo.Verb = "runas";
            Patching.Update ();
            Patching.DeleteFiles ("Temp");
            LauncherVersion.Content = Launcher.Version;
            ClientVersion.Content = Launcher.ClientVersion;
            char[] Key = new char[] { 'C', 'O', 'L', ':' };
            closeOnLaunch.IsChecked = Launcher.LoadSettings (Key);
        }

        private void LaunchButton_Click (object sender, RoutedEventArgs e)
        {
            Launcher.LaunchGame (closeOnLaunch, "YWDE");
        }

        private void LaunchWebsiteButton_Click (object sender, RoutedEventArgs e)
        {
            Launcher.LaunchWebsite ("https://zhakalendk.github.io/");
        }
    }
}
