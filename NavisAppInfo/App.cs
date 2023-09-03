using AppInfo.Command;
using Autodesk.Navisworks.Api.Plugins;

namespace AppInfo
{
    [Plugin("AppInfo", "ChuongMep", DisplayName = "AppInfo")]
    [RibbonLayout("AppInfoRibbon.xaml")]
    [RibbonTab("ID_AppInfo_TAB",DisplayName = "AppInfo")]
    [Command("ID_ButtonAppInfoApp", DisplayName = "Snoop \n Application", Icon = "Resources\\app-16.png", LargeIcon = "Resources\\app-32.png",ToolTip = "Snoop Application")]
    [Command("ID_ButtonAppInfoDoc", DisplayName = "Snoop \n Active Document", Icon = "Resources\\document-16.png", LargeIcon = "Resources\\document-32.png",ToolTip = "Snoop Active Document")]
    [Command("ID_ButtonAppInfoActiveView", DisplayName = "Snoop \n Active View", Icon = "Resources\\view-16.png", LargeIcon = "Resources\\view-32.png",ToolTip = "Snoop Active View")]
    [Command("ID_ButtonAppInfoActiveSheet", DisplayName = "Snoop \n Active Sheet", Icon = "Resources\\sheet-16.png", LargeIcon = "Resources\\sheet-32.png",ToolTip = "Snoop Active Sheet")]
    [Command("ID_ButtonAppInfoClashTest", DisplayName = "Snoop \n Clash Test", Icon = "Resources\\conflict-16.png", LargeIcon = "Resources\\conflict-32.png",ToolTip = "Snoop Clash Test")]
    [Command("ID_ButtonAppInfoClashResultSearch", DisplayName = "Snoop \n ClashResult Search", Icon = "Resources\\conflict-16.png", LargeIcon = "Resources\\conflict-32.png",ToolTip = "Snoop Clash Result Inside Test")]
    [Command("ID_ButtonAppInfoCurrentSelection", DisplayName = "Snoop \n Current Selection", Icon = "Resources\\cursor-16.png", LargeIcon = "Resources\\cursor-32.png",ToolTip = "Snoop Current Selection")]
    [Command("ID_ButtonAppInfoTest", DisplayName = "Test", Icon = "Resources\\test-16.png", LargeIcon = "Resources\\test-32.png",ToolTip = "Test")]
    public class App  : CommandHandlerPlugin
    {
        public override int ExecuteCommand(string name, params string[] parameters)
        {
            switch (name)
            {
                case "ID_ButtonAppInfoApp":
                    new SnoopApplication().Execute();
                    break;
                case "ID_ButtonAppInfoDoc":
                    new SnoopDocument().Execute();
                    break;
                case "ID_ButtonAppInfoTest":
                    new SnoopTest().Execute();
                    break;
                case "ID_ButtonAppInfoActiveView":
                    new SnoopActiveView().Execute();
                    break;
                case "ID_ButtonAppInfoActiveSheet":
                    new SnoopActiveSheet().Execute();
                    break;
                case "ID_ButtonAppInfoClashTest":
                    new SnoopClashTest().Execute();
                    break;
                case "ID_ButtonAppInfoClashResultSearch":
                    new SnoopSearch().Execute();
                    break;
                case "ID_ButtonAppInfoCurrentSelection":
                    new SnoopCurrentSelection().Execute();
                    break;
            }
            return 0;
        }
    }
}