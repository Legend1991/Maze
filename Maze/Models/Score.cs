namespace Maze.Models
{
    public class Score
    {
        private uint count = 0;
        private readonly uint magnitude = 1;

        public Score(uint complexity)
        {
            while (complexity > 0)
            {
                magnitude *= 10;
                complexity /= 10;
            }
        }

        public void Enroll()
        {
            count++;
        }

        // The more count value is the less output value is [from magnitude to 0]
        public uint Value()
        {
            return (uint)Math.Ceiling((decimal)1 / (count + 1) * magnitude);
        }
    }
}
