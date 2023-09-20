using System.Drawing;
using Maze.Models;

namespace Maze.Console
{
    public class Display : IDisplay
    {
        private readonly string[] schema;
        private readonly Hero hero;
        private readonly Score score;

        private Point previous;
        private bool mapRendered = false;

        public Display(string[] schema, Hero hero, Score score)
        {
            this.schema = schema;
            this.hero = hero;
            this.score = score;

            System.Console.OutputEncoding = System.Text.Encoding.UTF8;
        }

        public void RenderMaze()
        {
            if (mapRendered == false)
            {
                RenderMap();
                mapRendered = true;
            }

            RenderHero();
        }

        public void RenderAfterword()
        {
            var finishedMsg = "Well done! You completed the level! Your score is " + score.Value();
            var quitMsg = "CCoward! Why are you running away?! Your score is 0!!!";
            var afterwordMsg = hero.Finished() ? finishedMsg : quitMsg;

            System.Console.WriteLine();
            System.Console.WriteLine(afterwordMsg);
        }

        private void RenderMap()
        {
            foreach (var line in schema)
            {
                System.Console.WriteLine(line);
            }
        }

        private void RenderHero()
        {
            var current = hero.Position();

            if (previous.IsEmpty)
            {
                previous = current;
            }

            System.Console.MoveBufferArea(previous.X, previous.Y, 1, 1, current.X, current.Y);

            previous = current;
        }
    }
}
