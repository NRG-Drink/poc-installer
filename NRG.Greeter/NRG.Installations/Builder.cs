using System.Text;

namespace NRG.Installations;

public partial class Installer
{
    public partial class Builder
    {
        private static readonly string _userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        private static readonly string _programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private readonly Installer _installer = new();

        public Installer Build()
            => IsInstallerValid() is var report && string.IsNullOrEmpty(report)
            ? _installer
            : throw new Exception(report);

        public Builder FromZipPath(string zipPath)
        {
            _installer.ZipPath = zipPath;
            return this;
        }

        public Builder ToAppLocation(string appLocation)
        {
            _installer.AppLocation = appLocation;
            return this;
        }

        public Builder WithAppExePath(string path)
        {
            _installer.AppExe = path;
            return this;
        }

        public Builder OpenExplorerAfterInstallation()
        {
            _installer.OpenExplorerAfterInstallation = true;
            return this;
        }

        public Builder CreateLinkAt(string path)
        {
            _installer.LinkLocations.Add(path);
            return this;
        }

        private string IsInstallerValid()
        {
            var sb = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_installer.AppLocation))
            {
                sb.Append($"no {nameof(AppLocation)} is set.;");
            }

            return sb.ToString();
        }
    }
}
