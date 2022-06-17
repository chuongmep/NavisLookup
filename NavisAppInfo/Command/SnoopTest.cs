using System.Windows;
using AppInfo.View;
using Autodesk.Navisworks.Api.Plugins;

namespace AppInfo.Command
{
    public class SnoopTest : AddInPlugin
    {
        public override int Execute(params string[] parameters)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mainWindow.ShowDialog();
            return 0;
        }
    }
}