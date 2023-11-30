using Maze.Models;
using System.Drawing;

namespace Models.Map_spec;

public static class Fixtures
{
    public static readonly Map Map        = new(new[] { "*█", " x" });
    public static readonly Point Start    = new(0, 0);
    public static readonly Point Passage  = new(0, 1);
    public static readonly Point Obstacle = new(1, 0);
    public static readonly Point Finish   = new(1, 1);
    public static readonly uint Area      = 4;
}

public class A_point_is_a_start_point
{
    [Test]
    public void if_it_is_a_star_symbol()
    {
        var start = Fixtures.Map.Start();

        Assert.That(start, Is.EqualTo(Fixtures.Start));
    }
}

public class A_point_is_not_a_start_point
{
    [Test]
    public void if_it_is_not_a_star_symbol()
    {
        var start = Fixtures.Map.Start();

        Assert.That(start, Is.Not.EqualTo(Fixtures.Passage));
        Assert.That(start, Is.Not.EqualTo(Fixtures.Finish));
        Assert.That(start, Is.Not.EqualTo(Fixtures.Obstacle));
    }
}

public class A_point_is_an_obstacle_point
{
    [Test]
    public void if_it_is_a_full_block_symbol()
    {
        Assert.That(Fixtures.Map.Obstacle(Fixtures.Obstacle), Is.True);
    }
}

public class A_point_is_not_an_obstacle_point
{
    [Test]
    public void if_it_is_not_a_full_block_symbol()
    {
        Assert.That(Fixtures.Map.Obstacle(Fixtures.Start),   Is.False);
        Assert.That(Fixtures.Map.Obstacle(Fixtures.Passage), Is.False);
        Assert.That(Fixtures.Map.Obstacle(Fixtures.Finish),  Is.False);
    }
}

public class A_point_is_a_finish_point
{
    [Test]
    public void if_it_is_an_x_symbol()
    {
        var finish = Fixtures.Map.Finish();

        Assert.That(finish, Is.EqualTo(Fixtures.Finish));
    }
}

public class A_point_is_not_a_finish_point
{
    [Test]
    public void if_it_is_not_an_x_symbol()
    {
        var finish = Fixtures.Map.Finish();

        Assert.That(finish, Is.Not.EqualTo(Fixtures.Start));
        Assert.That(finish, Is.Not.EqualTo(Fixtures.Passage));
        Assert.That(finish, Is.Not.EqualTo(Fixtures.Obstacle));
    }
}

public class A_map_area
{
    [Test]
    public void returns_calculated_schema_area()
    {
        Assert.That(Fixtures.Map.Area(), Is.EqualTo(Fixtures.Area));
    }
}
