using Maze.Models;

namespace Maze;

internal class Program
{
    static void Main(string[] args)
    {
        var schema = new string[] {
            "█x█████████████████████████████",
            "█  █        █                 █",
            "█  ███████  ████  ██████████  █",
            "█  █           █  █  █     █  █",
            "█  █  █  ███████  █  ████  █  █",
            "█     █                    █  █",
            "███████  ████  █  █████████████",
            "█     █  █     █        █  █  █",
            "████  █  █████████████  █  █  █",
            "█  █     █  █  █  █  █  █  █  █",
            "█  █  █  █  █  █  █  █  █  █  █",
            "█     █     █        █        █",
            "█  ████  ███████  █  █  █  ████",
            "█     █  █        █  █  █  █  █",
            "████  ████  ████  ███████  █  █",
            "█     █        █  █  █  █     █",
            "█  ████  ██████████  █  ████  █",
            "█  █  █        █              █",
            "█  █  ████  █  ████  ███████  █",
            "█  █        █              █ *█",
            "███████████████████████████████"
        };

        var map = new Map(schema);
        var score = new DampedScore(map.Area());
        var hero = new Hero(map, score);

        // var controller = new Maze.Console.Controller(hero.Move);
        // var display = new Maze.Console.Display(schema, hero, score);
        var controller = new Maze.Raylib.Controller(hero.Move);
        var display = new Maze.Raylib.Display(schema, hero, score);

        var game = new MazeApplication(controller, display, hero);

        game.Run();
    }
}
