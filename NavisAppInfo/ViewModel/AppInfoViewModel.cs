using AppInfo.Command;
using AppInfo.Model;

namespace AppInfo.ViewModel
{
    public class AppInfoViewModel
    {
        public SnoopType SnoopType { get; set; }
        public NodeSearch.SearchType SearchType { get; set; }
        public string SearchValue { get; set; }
        public AppInfoViewModel(SnoopType snoopType)
        {
            SnoopType = snoopType;
        }
    }
}