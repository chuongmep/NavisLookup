namespace AppInfo.Command
{
    
    public class SnoopDocument : BaseCommand
    {
        public override SnoopType SnoopType { get; set; } = SnoopType.ActiveDocument;

    }
}