namespace AppInfo.Command
{
    public class SnoopCurrentSelection : BaseCommand
    {
        public override SnoopType SnoopType { get; set; } = SnoopType.CurrentSelection;
    }
}