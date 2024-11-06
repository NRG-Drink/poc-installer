using NRG.Installations;

namespace NRG.Greeter.Installations;

internal class Program
{
    private static readonly string _appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    private static readonly string _desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

    static async Task<int> Main(string[] args)
    {
        var appFolder = Path.Combine(_appData, "NRG-Greeter");

        var installer = new Installer.Builder()
            .FromZipPath("app.zip")
            .ToAppLocation(appFolder)
            .WithAppExePath(Path.Combine(appFolder, "NRG.Greeter.App.exe"))
            .CreateLinkAt(Path.Combine(_desktop, "Greeter"))
            .CreateLinkAt(Path.Combine(appFolder, "Greeter"))
            .OpenExplorerAfterInstallation()
            .Build()
            ;

        try
        {
            await installer.Install();
            Console.WriteLine("Setup finished as expected.");
            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return 1;
        }
    }
}
