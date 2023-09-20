using Maze.Models;
using Color = Raylib_cs.Color;

namespace Maze.Raylib
{
    using Renderers = Dictionary<char, Action<int, int>>;

    public class Display : IDisplay
    {
        private static readonly int WALL_SIZE = 30;

        private readonly string[] schema;
        private readonly Hero hero;
        private readonly Score score;
        private readonly Renderers renderers = new()
        {
            { Map.WALL,   RenderWall },
            { Map.FINISH, RenderFinish }
        };

        public Display(string[] schema, Hero hero, Score score) 
        {
            this.schema = schema;
            this.hero = hero;
            this.score = score;

            var height = schema.Length * WALL_SIZE;
            var width = schema[0].Length * WALL_SIZE;
            Raylib_cs.Raylib.InitWindow(width, height, "Maze");
        }
        public void RenderAfterword()
        {
            var finishedMsg = "Well done! You completed the level! Your score is " + score.Value();
            var quitMsg = "Coward! Why are you running away?! Your score is 0!!!";
            var afterwordMsg = hero.Finished() ? finishedMsg : quitMsg;

            Raylib_cs.Raylib.BeginDrawing();
            Raylib_cs.Raylib.ClearBackground(Color.BLACK);

            Raylib_cs.Raylib.DrawText(afterwordMsg, 12, 12, 20, Color.SKYBLUE);

            Raylib_cs.Raylib.EndDrawing();
        }

        public void RenderMaze()
        {
            Raylib_cs.Raylib.BeginDrawing();
            Raylib_cs.Raylib.ClearBackground(Color.BLACK);

            RenderMap();
            RenderHero();

            Raylib_cs.Raylib.EndDrawing();
        }

        private void RenderMap()
        {
            for (int y = 0; y < schema.Length; ++y)
            {
                var line = schema[y];
                for (int x = 0; x < line.Length; ++x)
                {
                    if (renderers.TryGetValue(line[x], out var draw))
                    {
                        draw(WALL_SIZE * x, WALL_SIZE * y);
                    }
                }
            }
        }

        static private void RenderWall(int x, int y)
        {
            Raylib_cs.Raylib.DrawRectangle(x, y, WALL_SIZE, WALL_SIZE, Color.DARKGREEN);
        }

        static private void RenderFinish(int x, int y)
        {
            Raylib_cs.Raylib.DrawRectangle(x, y, WALL_SIZE, WALL_SIZE, Color.RED);
        }

        private void RenderHero()
        {
            var pos = hero.Position();
            var radius = WALL_SIZE / 2;
            var x = pos.X * WALL_SIZE + radius;
            var y = pos.Y * WALL_SIZE + radius;

            Raylib_cs.Raylib.DrawCircle(x, y, radius, Color.WHITE);
        }

        ~Display()
        {
            Raylib_cs.Raylib.CloseWindow();
        }
    }
}
