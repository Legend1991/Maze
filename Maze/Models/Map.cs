using System.Drawing;

namespace Maze.Models
{
    public class Map
    {
        public static readonly char START = '*';
        public static readonly char FINISH = 'x';
        public static readonly char WALL = '█';

        private readonly string[] schema;
        private readonly Point start;
        private readonly Point finish;

        public Map(string[] schema)
        {
            this.schema = schema;

            for (int y = 0; y < schema.Length; ++y)
            {
                var line = schema[y];
                for (int x = 0; x < line.Length; ++x)
                {
                    if (line[x] == START) start = new Point(x, y);
                    if (line[x] == FINISH) finish = new Point(x, y);
                }
            }
        }

        public uint Complexity()
        {
            return (uint)(schema.Length * schema[0].Length);
        }

        public Point Start()
        {
            return new Point(start.X, start.Y);
        }

        public Point Finish()
        {
            return new Point(finish.X, finish.Y);
        }

        public bool Obstacle(Point p)
        {
            return schema[p.Y][p.X] == WALL;
        }
    }
}
