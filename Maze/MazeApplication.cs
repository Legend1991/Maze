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
    public class MazeApplication
    {
        private readonly IController controller;
        private readonly IDisplay display;
        private readonly Hero hero;
        private readonly Score score;

        public MazeApplication(IController controller, IDisplay display, Hero hero, Score score)
        {
            this.controller = controller;
            this.display = display;
            this.hero = hero;
            this.score = score;
        }

        public void Run()
        {
            while (controller.Quit() == false && hero.Finished() == false)
            {
                display.RenderMaze();
                controller.Interrupt();
                score.Enroll();
            }

            display.RenderAfterword();
        }
    }
}
