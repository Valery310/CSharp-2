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

        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X > Game.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        }
    }
}
