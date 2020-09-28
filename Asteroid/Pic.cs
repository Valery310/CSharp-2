using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroid
{
    class Pic : BaseObject
    {
        protected Bitmap bitmap;

        public Pic(Point pos, Point dir, Size size, string filepath) : base (pos, dir, size)
        {
            bitmap = new Bitmap(filepath);
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(bitmap, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
    }
}
