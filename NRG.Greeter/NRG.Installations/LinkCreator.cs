using IWshRuntimeLibrary;

namespace NRG.Installations;

public class LinkCreator
{
    public void CreateLink(string link, string target, string workingDirectory, bool overwriteOnExistence = true)
    {
        var validLink = ExtendLnkIfNeeded(link);

        var fileExistsAndShouldOverwrite = overwriteOnExistence && System.IO.File.Exists(link);
        if (!overwriteOnExistence && System.IO.File.Exists(link))
        {
            throw new LinkOverwriteException(
                $"Link already exists. ({nameof(overwriteOnExistence)} = {overwriteOnExistence})");
        }

        var shell = new WshShell();
        var shortcut = (IWshShortcut)shell.CreateShortcut(validLink);

        shortcut.TargetPath = target;
        shortcut.WorkingDirectory = workingDirectory;

        shortcut.Save();
    }

    private static string ExtendLnkIfNeeded(string link)
        => link.EndsWith(".lnk")
            ? link
            : link += ".lnk";
}

public class LinkOverwriteException(string message) : Exception(message);