﻿using System.Collections.Immutable;
using System.Linq;

namespace TwoThousandFortyEight.Logic
{
    public static class Directions
    {
        public static readonly Direction Up    = new Direction(new[] {'w', 'W'}, 0, -1);
        public static readonly Direction Down  = new Direction(new[] {'s', 'S'}, 0, 1);
        public static readonly Direction Left  = new Direction(new[] {'a', 'A'}, -1, 0);
        public static readonly Direction Right = new Direction(new[] {'d', 'D'}, 1, 0);

        public static readonly ImmutableArray<Direction> Values = ImmutableArray.Create(Up, Down, Left, Right);

        public static Direction? Parse(char key)
        {
            return Values.FirstOrDefault(value => value.Keys.Any(valueKey => valueKey == key));
        }
    }
}
