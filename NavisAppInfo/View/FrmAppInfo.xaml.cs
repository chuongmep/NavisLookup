using System.Windows;
using AppInfo.ViewModel;

namespace AppInfo.View
{
    public partial class FrmAppInfo : Window
    {
        public FrmAppInfo( AppInfoViewModel viewModel)
        {
            InitializeComponent();
            // Initialize a Host Control which allows hosting a windows form control on WPF. Ensure that the WindowsFormIntegration Reference is present.
            System.Windows.Forms.Integration.WindowsFormsHost host =
                new System.Windows.Forms.Integration.WindowsFormsHost();

            // Create an object of your User control.
            AppInfoControl ucAppInfo = new AppInfoControl(viewModel);

            // Assign MyWebcam control as the host control's child.
            host.Child = ucAppInfo;

            // Add the interop host control to the Grid control's collection of child controls. Make sure to rename grid1 to appr
            this.Grid.Children.Add(host);
        }
    }
}