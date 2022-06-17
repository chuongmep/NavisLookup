namespace AppInfo.Model
{
    public class FormIcons
    {
        public ImageList formIcons = new ImageList();

        public FormIcons()
        {
            formIcons.Images.Add(Properties.Resources.pubclass);
            formIcons.Images.Add(Properties.Resources.pubproperty);
            formIcons.Images.Add(Properties.Resources.pubevent);
            formIcons.Images.Add(Properties.Resources.pubmethod);
            formIcons.Images.Add(Properties.Resources.staticclass);
            formIcons.Images.Add(Properties.Resources.staticproperty);
            formIcons.Images.Add(Properties.Resources.staticmethod);
        }
    }
}