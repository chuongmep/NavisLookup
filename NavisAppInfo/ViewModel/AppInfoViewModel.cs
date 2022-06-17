using AppInfo.Command;

namespace AppInfo.ViewModel
{
    public class AppInfoViewModel
    {
        public SnoopType SnoopType { get; set; }
        public AppInfoViewModel(SnoopType snoopType)
        {
            SnoopType = snoopType;
        }
    }
}