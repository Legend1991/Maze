using ConsoleApp1;

namespace Test
{
    class CustomChance: IChance
    {
        public double Chance;

        public double Take()
        {
            return Chance;
        }
    }
    public class CoinTests
    {
        private static void AssertThatToss(Coin coin, CoinSide side)
        {
            Assert.Multiple(() =>
            {
                Assert.That(coin.Random(), Is.EqualTo(side));
                Assert.That(coin.MatchTop(side), Is.True);
            });
        }

        [Test]
        public void TestReturnOppositeSide()
        {
            Assert.Multiple(() =>
            {
                Assert.That(Coin.Opposite(CoinSide.Head), Is.EqualTo(CoinSide.Tail));
                Assert.That(Coin.Opposite(CoinSide.Tail), Is.EqualTo(CoinSide.Head));
            });
        }

        [Test]
        public void TestFullChanceTossHead()
        {
            var chance = new CustomChance() { Chance = 1 };
            var coin = new Coin(chance);

            coin.Toss();

            AssertThatToss(coin, CoinSide.Head);
        }

        [Test]
        public void TestHalfChanceTossHead()
        {
            var chance = new CustomChance() { Chance = 0.5 };
            var coin = new Coin(chance);

            coin.Toss();

            AssertThatToss(coin, CoinSide.Head);
        }

        [Test]
        public void TestBelowHalfChanceTossHead()
        {
            var chance = new CustomChance() { Chance = 0.49 };
            var coin = new Coin(chance);

            coin.Toss();

            AssertThatToss(coin, CoinSide.Tail);
        }

        [Test]
        public void TestZeroChanceTossTail()
        {
            var chance = new CustomChance() { Chance = 0 };
            var coin = new Coin(chance);

            coin.Toss();

            AssertThatToss(coin, CoinSide.Tail);
        }

        [Test]
        public void TestFullChanceWithoutToss()
        {
            var chance = new CustomChance() { Chance = 1 };
            var coin = new Coin(chance);

            AssertThatToss(coin, CoinSide.Head);
        }

        [Test]
        public void TestZeroChanceWithoutToss()
        {
            var chance = new CustomChance() { Chance = 0 };
            var coin = new Coin(chance);

            Assert.Multiple(() =>
            {
                Assert.That(coin.Random(), Is.EqualTo(CoinSide.Tail));
                Assert.That(coin.MatchTop(CoinSide.Head), Is.True);
            });
        }
    }
}
