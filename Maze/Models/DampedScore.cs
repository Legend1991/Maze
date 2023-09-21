namespace Maze.Models
{
    public class DampedScore : IScore
    {
        private uint count = 0;
        private readonly uint magnitude = 1;

        public DampedScore(uint complexity)
        {
            while (complexity > 0)
            {
                magnitude *= 10;
                complexity /= 10;
            }
            magnitude *= 100;
        }

        public void Enroll()
        {
            count++;
        }

        // The more count value is the less output value is [from magnitude to 1]
        public uint Value()
        {
            return (uint) Math.Ceiling((decimal) 1 / (count + 1) * magnitude);
        }
    }
}
