using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using WixSharp;
using WixSharp.CommonTasks;
using WixSharp.Controls;

string rootDirectory = Path.GetPathRoot(Environment.SystemDirectory);
const string BundleName = "NavisLookup.bundle";
string installationDir = Path.Combine(rootDirectory,@"ProgramData\\Autodesk\\ApplicationPlugins",BundleName);
const string projectName = "Navisworks Lookup";
const string outputName = "Navis.NavisLookup";
const string outputDir = "output";
const string version = "1.0.5";
const string xmlName = "PackageContents.xml";
var fileName = new StringBuilder().Append(outputName).Append("-").Append(version);
var project = new Project
{
    Name = projectName,
    OutDir = outputDir,
    Platform = Platform.x64,
    Description = "Project Snoop Database In Navisworks",
    UI = WUI.WixUI_InstallDir,
    Version = new Version(version),
    OutFileName = fileName.ToString(),
    InstallScope = InstallScope.perUser,
    MajorUpgrade = MajorUpgrade.Default,
    GUID = new Guid("2720165A-E1F1-4188-835E-5D628935DB12"),
    BackgroundImage = @"Installer\Resources\Icons\BackgroundImage.png",
    BannerImage = @"Installer\Resources\Icons\BannerImage.png",
    ControlPanelInfo =
    {
        Manufacturer = "chuongmep",
        Comments = "Project Snoop Database In Navisworks",
        ProductIcon = @"Installer\Resources\Icons\ShellIcon.ico"
    },
    Dirs = new Dir[]
    {
        new InstallDir(Path.Combine(installationDir,"Contents"), GenerateWixEntities()),
        new(installationDir,GenXmlEntity())
    }
    
};

MajorUpgrade.Default.AllowSameVersionUpgrades = true;
project.RemoveDialogsBetween(NativeDialogs.WelcomeDlg, NativeDialogs.InstallDirDlg);
project.BuildMsi();

WixEntity[] GenerateWixEntities()
{
    Console.WriteLine("Start Create Installer");
    var versionRegex = new Regex(@"\d+");
    var versionStorages = new Dictionary<string, List<WixEntity>>();
    if (args.Length == 0) Console.WriteLine("Have some Problem with args build installer");
    int countEntity = 0;
    foreach (var directory in GetDirectories())
    {
        Console.WriteLine($"Working with Directory: {directory}");
        var directoryInfo = new DirectoryInfo(directory);
        var fileVersion = versionRegex.Match(directoryInfo.Name).Value;
        var files = new Files($@"{directory}\*.*");
        if (versionStorages.ContainsKey(fileVersion))
            versionStorages[fileVersion].Add(files);
        else
            versionStorages.Add(fileVersion, new List<WixEntity> {files});
        var assemblies = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);
        Console.WriteLine($"Adding '{fileVersion}' version files: ");
        foreach (var assembly in assemblies)
        {
            Console.WriteLine($"'{assembly}'");
            countEntity++;
        }
        Console.WriteLine($"Added {countEntity} files to msi");
        if (System.IO.File.Exists(Path.Combine(installationDir, xmlName)))
        {
            Console.WriteLine($"Added {xmlName} to bundle msi");
        }
    }
    return versionStorages.Select(storage => new Dir(storage.Key, storage.Value.ToArray())).Cast<WixEntity>().ToArray();
}

WixEntity[] GenXmlEntity()
{
    var files = new Files($@"{installationDir}\{xmlName}");
    return new List<WixEntity> {files}.ToArray();
}

string[] GetDirectories()
{
    var directories = Directory.GetDirectories(Path.Combine(installationDir,"Contents"));
    return directories;
}


