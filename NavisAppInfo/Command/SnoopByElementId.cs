namespace AppInfo.Command
{
    public class SnoopByElementId : BaseCommand
    {
        public override SnoopType SnoopType { get; set; } = SnoopType.ElementId;

    }
}