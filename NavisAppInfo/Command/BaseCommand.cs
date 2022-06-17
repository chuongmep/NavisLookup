using System.Windows;
using System.Windows.Interop;
using AppInfo.View;
using AppInfo.ViewModel;
using Autodesk.Navisworks.Api.Plugins;
using Application = Autodesk.Navisworks.Api.Application;

namespace AppInfo.Command
{
    public abstract class BaseCommand  : AddInPlugin
    {

        public abstract SnoopType SnoopType { get; set; }

        public override int Execute(params string[] parameters)
        {
            FrmAppInfo frmAppInfo = new FrmAppInfo(new AppInfoViewModel(SnoopType));
            frmAppInfo.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            IntPtr handle = Application.Gui.MainWindow.Handle;
            new WindowInteropHelper(frmAppInfo).Owner = handle;
            frmAppInfo.Show();
            return 0;
        }
    }
}