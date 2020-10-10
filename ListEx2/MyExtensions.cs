using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace ListEx2
{
    static class MyExtensions
    {
        public static Dictionary<int, int> GetRep(this List<int> list) 
        {
            return list.GroupBy(i => i).Select(g => new { Value = g.Key, Count = g.Count() }).ToDictionary(g => g.Value, i => i.Count);
        }

        public static Dictionary<T, int> GetRep<T>(this List<T> list)
        {
            return list.GroupBy(i => i).Select(g => new { Value = g.Key, Count = g.Count() }).ToDictionary(g => g.Value, i => i.Count);
        }
    }
}
