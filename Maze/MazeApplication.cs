using Maze.Models;

namespace Maze
{
    public interface IController
    {
        void Interrupt();
        bool Quit();
    }

    public interface IDisplay
    {
        void RenderMaze();
        void RenderAfterword();
    }

    public class MazeApplication(IController controller, IDisplay display, Hero hero)
    {
        private readonly IController controller = controller;
        private readonly IDisplay display = display;
        private readonly Hero hero = hero;

        public void Run()
        {
            while (controller.Quit() == false && hero.Finished() == false)
            {
                display.RenderMaze();
                controller.Interrupt();
            }

            display.RenderAfterword();
        }
    }
}
