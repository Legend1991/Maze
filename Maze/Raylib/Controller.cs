using Maze.Models;
using RaylibCS = Raylib_cs.Raylib;
using KeyboardKey = Raylib_cs.KeyboardKey;

namespace Maze.Raylib
{
    using KeyBinding = Dictionary<KeyboardKey, Action>;

    public class Controller(Action<Direction> handler) : IController
    {
        private readonly KeyBinding keyBinding = new()
        {
            { KeyboardKey.KEY_UP,    () => handler(Direction.Up) },
            { KeyboardKey.KEY_DOWN,  () => handler(Direction.Down) },
            { KeyboardKey.KEY_LEFT,  () => handler(Direction.Left) },
            { KeyboardKey.KEY_RIGHT, () => handler(Direction.Right) }
        };

        public void Interrupt()
        {
            var input = (KeyboardKey) RaylibCS.GetKeyPressed();
            if (keyBinding.TryGetValue(input, out var handle))
            {
                handle();
            }
        }

        public bool Quit()
        {
            return RaylibCS.WindowShouldClose();
        }
    }
}
