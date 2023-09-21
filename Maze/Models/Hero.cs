using System.Drawing;

namespace Maze.Models
{
    using Movements = Dictionary<Direction, Action>;

    public enum Direction
    {
        Up, Down, Left, Right
    }

    public interface IScore
    {
        void Enroll();
        uint Value();
    }

    public class Hero
    {
        private readonly Map map;
        private readonly IScore score;
        private readonly Movements movements;

        private Point position;

        public Hero(Map map, IScore score)
        {
            this.map = map;
            this.score = score;
            position = map.Start();
            movements = new()
            {
                { Direction.Up,    () => position.Y -= 1 },
                { Direction.Down,  () => position.Y += 1 },
                { Direction.Left,  () => position.X -= 1 },
                { Direction.Right, () => position.X += 1 },
            };
        }

        public Point Position()
        {
            return new Point(position.X, position.Y);
        }

        public bool Finished()
        {
            return position == map.Finish();
        }

        public void Move(Direction direction)
        {
            if (movements.TryGetValue(direction, out var move))
            {
                var previous = Position();

                move();

                if (map.Obstacle(position))
                {
                    position = previous;
                }
                else
                {
                    score.Enroll();
                }
            }
        }
    }
}
