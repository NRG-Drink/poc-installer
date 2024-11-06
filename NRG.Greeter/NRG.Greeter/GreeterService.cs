using static System.Console;

namespace NRG.Greeter;

public class GreeterService
{
    public void Greet(string name)
        => WriteLine($"Greetings {name}!");
}
