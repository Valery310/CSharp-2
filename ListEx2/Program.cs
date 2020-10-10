using System;
using System.Collections.Generic;
using System.Linq;

namespace ListEx2
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random(Environment.TickCount);
            List<int> list = new List<int>(100);
            for (int i = 0; i < list.Capacity; i++)
            {
                list.Add(random.Next(0, 50));
            }

            #region Ex A
            foreach (var item in list.GetRep())
            {
                Console.WriteLine($"Ex \"A\", {item.Key,5} - {item.Value}");
            }
            #endregion

            #region Ex B
            foreach (var item in list.GetRep<int>())
            {
                Console.WriteLine($"Ex \"B\", {item.Key,5} - {item.Value}");
            }
            #endregion

            #region Ex C
            var temp = from i in list
                       group i by new { i } into g
                       select new
                       {
                           item = g.Key,
                           itemcount = g.Count()
                       };

            foreach (var item in temp)
            {
                Console.WriteLine($"Ex \"C\", {item.item.i, 5} - {item.itemcount}");
            }
            #endregion



            Console.WriteLine("Hello World!");
        }
    }
}
