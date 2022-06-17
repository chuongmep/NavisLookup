namespace AppInfo.Command
{
    public class SnoopSearch :  BaseCommand
    {
        public override SnoopType SnoopType { get; set; } = SnoopType.Search;

    }
}