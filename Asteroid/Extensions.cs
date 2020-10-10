using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroid
{
    static class Extensions
    {
        public static void RemoveAsteroid(this List<Asteroid> asteroids, int asteroid, Random rnd) 
        {
            asteroids.RemoveAt(asteroid);
            if (asteroids.Count == 0)
            {
                int temp = Asteroid.CreatedAsteroids + 1;
                Asteroid.CreatedAsteroids = 0;
                asteroids.Capacity = temp;
                for (int i = 0; i < temp; i++)
                {
                    int r = rnd.Next(5, 50);
                    asteroids.Add(new Asteroid(new Point(Game.Width, rnd.Next(0, Game.Height)), new Point(-r / rnd.Next(3, 8), r / rnd.Next(1, 5)), new Size(r, r)));
                }
            }
        }
    }
}
