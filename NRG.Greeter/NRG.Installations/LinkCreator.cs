using IWshRuntimeLibrary;

namespace NRG.Installations;

public class LinkCreator
{
    public void CreateLink(string link, string target, string workingDirectory, bool overwriteOnEixtence = true)
    {
        var validLink = ExtendLnkIfNeeded(link);

        if (System.IO.File.Exists(link))
        {
            //if (overwriteOnEixtence)
            //{
            //    System.IO.File.Delete(link);
            //}
            //else
            //{
                throw new Exception("Link already exists");
            //}
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
