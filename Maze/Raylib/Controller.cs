using Maze.Models;

namespace Maze.Raylib
{
    using KeyBinding = Dictionary<int, Action>;
    public class Controller : IController
    {
        private readonly KeyBinding keyBinding;

        public Controller(Action<Direction> handler)
        {
            keyBinding = new()
            {
                { 265, () => handler(Direction.Up) },
                { 264, () => handler(Direction.Down) },
                { 263, () => handler(Direction.Left) },
                { 262, () => handler(Direction.Right) }
            };
        }
        public void Interrupt()
        {
            int input = Raylib_cs.Raylib.GetKeyPressed();
            if (keyBinding.TryGetValue(input, out var handle))
            {
                handle();
            }
        }

        public bool Quit()
        {
            return Raylib_cs.Raylib.WindowShouldClose();
        }
    }
}
