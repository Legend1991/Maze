using Maze.Models;

namespace Maze
{
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
            var hero = new Hero(map);
            var score = new Score(map.Complexity());

            var controller = new Maze.Console.Controller(hero.Move);
            var display = new Maze.Console.Display(schema, hero, score);

            var game = new MazeApplication(controller, display, hero, score);

            game.Run();
        }
    }
}
