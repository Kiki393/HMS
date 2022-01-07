// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RandomNumber.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>
// <summary>
//   Defines the RandomNumber type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HMS.Random_Number_Generator
{
    using System;

    /// <summary>
    /// The random number.
    /// </summary>
    public static class RandomNumber
    {
        /// <summary>
        /// The _random.
        /// </summary>
        private static readonly Random Random = new Random();

        /// Generates a random number within a range.
        /// <summary>
        /// The random number.
        /// </summary>
        /// <param name="min">
        /// The min.
        /// </param>
        /// <param name="max">
        /// The max.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int RandomNum(int min, int max)
        {
            return Random.Next(min, max);
        }
    }
}