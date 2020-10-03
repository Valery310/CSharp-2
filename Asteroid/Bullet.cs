using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroid
{
    class Bullet : BaseObject
    {
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(Pens.OrangeRed, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public override void Update()
        {
            if (Pos.X + 3 < Game.Width)
            {
                Pos.X = Pos.X + 3;
            }
            else
            {
                Pos.X = 0;
                Pos.Y = random.Next(0, Game.Width);
            }
           
        }
    }
}
