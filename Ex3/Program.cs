using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Ex3
{
    class Program
    {
        public static int GetOrder(KeyValuePair<string, int> pair) => pair.Value;

        static void Main(string[] args)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>()
              {
                {"four",4 },
                {"two",2 },
                { "one",1 },
                {"three",3 },
              };
            var d = dict.OrderBy(delegate (KeyValuePair<string, int> pair) { return pair.Value; });
            foreach (var pair in d)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }


            var dl = dict.OrderBy(pair => pair.Value);
            foreach (var pair in d)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }

            Func<KeyValuePair<string, int>, int> r = GetOrder;
            var dd = dict.OrderBy(r);
            foreach (var pair in d)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }
        }
    }
}
