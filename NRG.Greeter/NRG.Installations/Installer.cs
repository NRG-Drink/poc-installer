using System.Diagnostics;
using System.IO.Compression;

namespace NRG.Installations;

public partial class Installer
{
    public partial class Builder { }

    private readonly LinkCreator _linkCreator = new();
    public string ZipPath { get; private set; }
    public string AppLocation { get; private set; }
    public string AppExePathAbs { get; private set; }
    public bool OpenExplorerAfterInstallation { get; private set; } = false;
    public List<string> LinkLocations { get; private set; } = [];

    private Installer() { }

    public async Task Install()
    {
        if (IsFolderContainingDataAndExisting(AppLocation))
        {
            OpenFolderIfNeeded();
            throw new Exception("Target folder already contains data.");
        }

        Directory.CreateDirectory(AppLocation);
        var appStream = new FileStream(ZipPath, FileMode.Open, FileAccess.Read, FileShare.Read);
        ZipFile.ExtractToDirectory(appStream, AppLocation);

        foreach (var link in LinkLocations)
        {
            _linkCreator.CreateLink(link, AppExePathAbs, AppLocation);
        }

        OpenFolderIfNeeded();
    }

    private void OpenFolderIfNeeded()
    {
        if (OpenExplorerAfterInstallation)
        {
            Process.Start("explorer.exe", AppLocation);
        }
    }

    private static bool IsFolderContainingDataAndExisting(string folder)
        => Directory.Exists(folder) 
            && Directory.GetFiles(folder).Concat(Directory.GetDirectories(folder)).Any();
}
