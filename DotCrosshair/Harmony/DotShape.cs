using System;

namespace DotCrosshair.Harmony
{
    public class DotShape
    {
        private static readonly DotShape Round = new DotShape("Round");
        private static readonly DotShape Square = new DotShape("Square");
        private string Name { get; }

        private DotShape(string name)
        {
            Name = name;
        }

        public static DotShape FromString(string shape)
        {
            if (shape.Equals(Round.Name, StringComparison.OrdinalIgnoreCase)) return Round;
            if (shape.Equals(Square.Name, StringComparison.OrdinalIgnoreCase)) return Square;

            throw new ArgumentException($"Invalid shape: {shape}");
        }

        public bool IsSquare()
        {
            return this == Square;
        }

        public bool IsRound()
        {
            return this == Round;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}