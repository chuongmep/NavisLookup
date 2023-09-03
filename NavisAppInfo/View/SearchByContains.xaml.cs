using System.Windows;
using AppInfo.ViewModel;

namespace AppInfo.View
{
    public partial class SearchByContains : Window
    {
        private AppInfoViewModel _viewModel { get; }
        public SearchByContains(AppInfoViewModel viewModel)
        {
            InitializeComponent();
            this._viewModel = viewModel;
            this.DataContext = viewModel;
        }
    }
}