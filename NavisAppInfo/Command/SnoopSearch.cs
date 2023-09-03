using System.Windows;
using System.Windows.Interop;
using AppInfo.View;
using AppInfo.ViewModel;
using Application = System.Windows.Forms.Application;

namespace AppInfo.Command
{
    public class SnoopSearch :  BaseCommand
    {
        public override SnoopType SnoopType { get; set; } = SnoopType.Search;
        public override int Execute(params string[] parameters)
        {
            AppInfoViewModel viewModel = new AppInfoViewModel(SnoopType);
            SearchByContains searchByContains = new SearchByContains(viewModel);
            searchByContains.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            IntPtr handle = Autodesk.Navisworks.Api.Application.Gui.MainWindow.Handle;
            new WindowInteropHelper(searchByContains).Owner = handle;
            searchByContains.ShowDialog();
            return 0;
        }
    }
}