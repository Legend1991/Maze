using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public enum CoinSide
    {
        Head,
        Tail
    }

    public struct CoinPlayer
    {
        public String Name;
        public CoinSide Side;
    }

    public interface IChance
    {
        double Take();
    }

    public class Coin
    {
        private CoinSide top = CoinSide.Head;
        private readonly IChance chance;

        public static CoinSide Opposite(CoinSide side)
        {
            return side == CoinSide.Head ? CoinSide.Tail : CoinSide.Head;
        }

        public Coin(IChance chance)
        {
            this.chance = chance;
        }

        public CoinSide Random()
        {
            return chance.Take() >= 0.5 ? CoinSide.Head : CoinSide.Tail;
        }

        public void Toss()
        {
            top = Random();
        }

        public bool MatchTop(CoinSide side)
        {
            return top == side;
        }
    }

    public class CoinFlipping
    {
        Coin coin;
        CoinPlayer player1;
        CoinPlayer player2;

        public CoinFlipping(Coin coin, CoinPlayer player1, CoinPlayer player2)
        {
            this.coin = coin;
            this.player1 = player1;
            this.player2 = player2;
        }

        public void Play()
        {
            player1.Side = coin.Random();
            player2.Side = Coin.Opposite(player1.Side);
            coin.Toss();
        }

        public CoinPlayer Winner()
        {
            return coin.MatchTop(player1.Side) ? player1 : player2;
        }
    }

    class RandomChance : IChance
    {
        private readonly Random random = new((int)DateTime.Now.Ticks & 0x0000FFFF);
        public double Take()
        {
            return random.NextDouble();
        }
    }
}

//var chance = new RandomChance();
//var coin = new Coin(chance);
//var player1 = new CoinPlayer { Name = "Mark" };
//var player2 = new CoinPlayer { Name = "Tom" };
//var game = new CoinFlipping(coin, player1, player2);

//do
//{
//    game.Play();
//    var winner = game.Winner();

//    Console.WriteLine(winner.Name + " won with a flip of " + winner.Side);
//}
//while (Console.ReadKey().Key != ConsoleKey.Escape);
