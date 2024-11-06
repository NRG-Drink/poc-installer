using NRG.Installations;

namespace NRG.Greeter.Installations;

internal class Program
{
    static void Main(string[] args)
    {
        var installer = new Installer.Builder()
            .FromZipPath("app.zip")
            ;
    }
}
