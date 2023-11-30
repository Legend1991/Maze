using Maze.Models;

namespace Models.Hero_spec;

public class ScoreSpy : IScore
{
    private uint value = 0;

    public void Enroll() { value++; }
    public uint Value() { return value; }
}

[TestFixture]
public class A_new_hero
{
    private Map map;
    private IScore score;
    private Hero hero;

    [SetUp]
    public void SetUp()
    {
        map   = Map_spec.Fixtures.Map;
        score = new ScoreSpy();
        hero  = new Hero(map, score);
    }

    [Test]
    public void spawns_at_a_start_point()
    {
        Assert.That(hero.Position(), Is.EqualTo(Map_spec.Fixtures.Start));
        Assert.That(score.Value(), Is.EqualTo(0));
    }

    [Test]
    public void cannot_move_to_an_obstacle_point()
    {
        var previousPosition = hero.Position();

        hero.Move(Direction.Up);

        Assert.That(hero.Position(), Is.EqualTo(previousPosition));
        Assert.That(hero.Finished(), Is.False);
        Assert.That(score.Value(), Is.EqualTo(0));
    }

    [Test]
    public void can_move_to_a_passage_point()
    {
        hero.Move(Direction.Left);

        Assert.That(hero.Position(), Is.EqualTo(Map_spec.Fixtures.Passage));
        Assert.That(hero.Finished(), Is.False);
        Assert.That(score.Value(), Is.EqualTo(1));
    }

    [Test]
    public void can_move_to_a_finish_point()
    {
        hero.Move(Direction.Left);
        hero.Move(Direction.Up);

        Assert.That(hero.Position(), Is.EqualTo(Map_spec.Fixtures.Finish));
        Assert.That(hero.Finished(), Is.True);
        Assert.That(score.Value(), Is.EqualTo(2));
    }
}

