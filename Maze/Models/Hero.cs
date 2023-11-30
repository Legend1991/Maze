using System.Drawing;

namespace Maze.Models;

using Movements = Dictionary<Direction, Func<Point>>;

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
    private readonly Map map      = map;
    private readonly IScore score = score;
    private Point position        = map.Start();
    private Movements movements   => new()
    {
        { Direction.Up,    () => new Point(position.X,     position.Y - 1) },
        { Direction.Down,  () => new Point(position.X,     position.Y + 1) },
        { Direction.Left,  () => new Point(position.X - 1, position.Y)     },
        { Direction.Right, () => new Point(position.X + 1, position.Y)     },
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
            var nextPosition = move();

            if (map.Obstacle(nextPosition) == false)
            {
                position = nextPosition;
                score.Enroll();
            }
        }
    }
}
