using Maze.Models;

namespace Maze.Console;

using KeyBinding = Dictionary<ConsoleKey, Action>;

public class Controller(Action<Direction> handler) : IController
{
    private bool quit = false;
    private KeyBinding keyBinding => new()
    {
        { ConsoleKey.UpArrow,    () => handler(Direction.Up) },
        { ConsoleKey.DownArrow,  () => handler(Direction.Down) },
        { ConsoleKey.LeftArrow,  () => handler(Direction.Left) },
        { ConsoleKey.RightArrow, () => handler(Direction.Right) },
        { ConsoleKey.Escape,     () => { quit = true; } }
    };

    public void Interrupt()
    {
        var input = System.Console.ReadKey().Key;
        if (keyBinding.TryGetValue(input, out var handle))
        {
            handle();
        }
    }

    public bool Quit()
    {
        return quit;
    }
}
