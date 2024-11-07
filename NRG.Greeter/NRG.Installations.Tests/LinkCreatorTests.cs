namespace NRG.Installations.Tests;

public class LinkCreatorTests : IAsyncLifetime
{
    private static readonly string _folder = Path.Combine(Path.GetTempPath(), "test-folder");
    private static readonly string _filePath = Path.Combine(_folder, "testFile.txt");
    private static readonly string _linkPath = Path.Combine(_folder, "Link.lnk");
    private static readonly string _wd1 = Path.Combine(_folder, "wd1");
    private static readonly string _wd2 = Path.Combine(_folder, "wd2");
    private static readonly string _wd3 = Path.Combine(_folder, "wd3");

    public async Task InitializeAsync()
    {
        Directory.CreateDirectory(_folder);
        Directory.CreateDirectory(_wd1);
        Directory.CreateDirectory(_wd2);
        Directory.CreateDirectory(_wd3);
        await File.WriteAllTextAsync(_filePath, "hello world");
        // Open folder in explorer for debug purposes.
        //Process.Start("explorer.exe", _folder);
    }

    public Task DisposeAsync()
    {
        if (Directory.Exists(_folder))
        {
            Directory.Delete(_folder, true);
        }

        return Task.CompletedTask;
    }

    [Fact]
    public void OverwriteTests()
    {
        var creator = new LinkCreator();
        Assert.False(File.Exists(_linkPath));
        creator.CreateLink(_linkPath, _filePath, _wd1, false);
        Assert.True(File.Exists(_linkPath));
        var time1 = File.GetLastWriteTime(_linkPath);

        creator.CreateLink(_linkPath, _filePath, _wd2, true);
        var time2 = File.GetLastWriteTime(_linkPath);
        Assert.True(time1 < time2);

        Assert.Throws<LinkOverwriteException>(() => creator.CreateLink(_linkPath, _filePath, _wd3, false));
        var time3 = File.GetLastWriteTime(_linkPath);
        Assert.Equal(time2, time3);
    }
}
