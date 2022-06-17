namespace AppInfo.Command
{
    public class SnoopActiveSheet : BaseCommand
    {
        public override SnoopType SnoopType { get; set; } = SnoopType.ActiveSheet;

    }
}