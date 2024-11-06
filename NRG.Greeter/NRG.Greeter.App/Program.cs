namespace NRG.Greeter.App;

internal class Program
{
    static async Task Main(string[] _)
    {
        var fileContent = await File.ReadAllTextAsync(Path.Combine(".dat", "welcome_banner.txt"));
        Console.WriteLine(fileContent);
        while (true)
        {
            Console.Write("Enter name: ");
            var name = Console.ReadLine();

            var greeter = new GreeterService();
            greeter.Greet(name ?? "No Name");

            await Task.Delay(3_000);
        }
    }
}
