using System.Drawing;
using Maze.Models;

namespace Maze.Console;

public class Display(string[] schema, Hero hero, IScore score) : IDisplay
{
    private readonly string[] schema = schema;
    private readonly Hero hero = hero;
    private readonly IScore score = score;

    private Point previous;
    private bool mapRendered = false;

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
