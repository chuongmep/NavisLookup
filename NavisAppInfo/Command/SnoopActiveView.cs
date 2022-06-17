
namespace AppInfo.Command
{
    public class SnoopActiveView : BaseCommand
    {
        public override SnoopType SnoopType { get; set; } = SnoopType.ActiveView;
    }
}