using Maze.Models;
using RaylibCS = Raylib_cs.Raylib;
using ConfigFlags = Raylib_cs.ConfigFlags;
using Color = Raylib_cs.Color;

namespace Maze.Raylib
{
    using Renderers = Dictionary<char, Action<int, int>>;

    public class Display : IDisplay
    {
        private static readonly int WALL_SIZE = 30;
        private static readonly Color WALL_COLOR = new(3, 201, 160, 255);
        private static readonly Color FINISH_COLOR = new(255, 189, 0, 255);
        private static readonly int SCORE_FONT_SIZE = 20;

        private readonly string[] schema;
        private readonly Hero hero;
        private readonly IScore score;
        private readonly Renderers renderers = new()
        {
            { Map.WALL,   RenderWall },
            { Map.FINISH, RenderFinish }
        };

        public Display(string[] schema, Hero hero, IScore score) 
        {
            this.schema = schema;
            this.hero = hero;
            this.score = score;

            RaylibCS.SetConfigFlags(ConfigFlags.FLAG_MSAA_4X_HINT);

            var height = schema.Length * WALL_SIZE + SCORE_FONT_SIZE;
            var width = schema[0].Length * WALL_SIZE;
            RaylibCS.InitWindow(width, height, "Maze");
        }

        public void RenderAfterword()
        {
            var finishedMsg = "Well done! You completed the level! Your score is " + score.Value();
            var quitMsg = "Coward! Why are you running away?! Your score is 0!!!";
            var afterwordMsg = hero.Finished() ? finishedMsg : quitMsg;

            RaylibCS.BeginDrawing();
            RaylibCS.ClearBackground(Color.BLACK);

            RaylibCS.DrawText(afterwordMsg, 12, 12, 20, Color.SKYBLUE);

            RaylibCS.EndDrawing();
        }

        public void RenderMaze()
        {
            RaylibCS.BeginDrawing();
            RaylibCS.ClearBackground(Color.BLACK);

            RenderMap();
            RenderHero();
            RenderScore();

            RaylibCS.DrawFPS(200, schema.Length * WALL_SIZE);

            RaylibCS.EndDrawing();
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
            RaylibCS.DrawRectangle(x, y, WALL_SIZE, WALL_SIZE, WALL_COLOR);
        }

        static private void RenderFinish(int x, int y)
        {
            var size = WALL_SIZE / 2;
            var shift = size / 2;
            RaylibCS.DrawRectangle(x + shift, y + shift, size, size, FINISH_COLOR);
        }

        private void RenderHero()
        {
            var pos = hero.Position();
            var radius = WALL_SIZE / 2;
            var x = pos.X * WALL_SIZE + radius;
            var y = pos.Y * WALL_SIZE + radius;

            RaylibCS.DrawCircle(x, y, radius, Color.WHITE);
        }

        public void RenderScore()
        {
            RaylibCS.DrawText("Score: " + score.Value(), 0, schema.Length * WALL_SIZE, SCORE_FONT_SIZE, Color.SKYBLUE);
        }

        ~Display()
        {
            RaylibCS.CloseWindow();
        }
    }
}
