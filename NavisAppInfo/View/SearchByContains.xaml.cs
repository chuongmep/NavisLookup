using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using AppInfo.Model;
using AppInfo.ViewModel;
using Application = Autodesk.Navisworks.Api.Application;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.MessageBox;

namespace AppInfo.View
{
    public partial class SearchByContains : Window
    {
        private AppInfoViewModel _viewModel { get; set; }
        public SearchByContains(AppInfoViewModel viewModel)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            _viewModel = viewModel;
            this.DataContext = viewModel;
            KeyDown += Window_KeyDown;
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SnoopSearchClick(sender, e);
            }
        }


        [STAThread]
        private void SnoopSearchClick(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (cbbSnoopType.Text)
                {
                    case "ClashResult Name":
                        _viewModel.SearchType = NodeSearch.SearchType.ClashResultName;
                        break;
                    case "ClashResult Guid":
                        _viewModel.SearchType = NodeSearch.SearchType.ClashResultGuid;
                        break;
                }

                if (string.IsNullOrEmpty(txtSearchValue.Text))
                {
                    MessageBox.Show("Please input search value");
                    return;
                }
                _viewModel.SearchValue = txtSearchValue.Text.ToLower();
                Close();
                FrmAppInfo frmAppInfo = new FrmAppInfo(_viewModel);
                frmAppInfo.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                IntPtr handle = Application.Gui.MainWindow.Handle;
                new WindowInteropHelper(frmAppInfo).Owner = handle;
                frmAppInfo.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            
        }
    }
}