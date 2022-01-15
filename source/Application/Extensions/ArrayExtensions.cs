using System;

namespace Application.Extensions
{
    public static class ArrayExtensions
    {
        public static T[,] ToMultidimensional<T>(this T[][] source)
        {
            // TODO: Validation and special-casing for arrays.Count == 0
            int minorLength = source[0].Length;
            var result = new T[source.Length, minorLength];
            for (int i = 0; i < source.Length; i++)
            {
                var array = source[i];
                if (array.Length != minorLength)
                {
                    throw new ArgumentException("All arrays must be of the same length");
                }
                for (int j = 0; j < minorLength; j++)
                {
                    result[i, j] = array[j];
                }
            }
            return result;
        }
    }
}
