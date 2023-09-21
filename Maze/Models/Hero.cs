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

    public class Hero(Map map, IScore score)
    {
        private readonly Map map = map;
        private readonly IScore score = score;
        private Point position = map.Start();
        private Movements movements => new()
        {
            { Direction.Up,    () => position.Y -= 1 },
            { Direction.Down,  () => position.Y += 1 },
            { Direction.Left,  () => position.X -= 1 },
            { Direction.Right, () => position.X += 1 },
        };

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
