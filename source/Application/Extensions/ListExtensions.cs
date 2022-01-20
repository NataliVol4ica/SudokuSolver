using System.Collections.Generic;

namespace Application.Extensions
{
    public static class ListExtensions
    {
        public static List<T> Clone<T>(this List<T> source)
        {
            var result = new List<T>(source.Count);
            foreach (var item in source)
            {
                result.Add(item);
            }

            return result;
        }
    }
}
