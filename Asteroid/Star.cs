using System;

using System.Drawing;

namespace Asteroid
{
    class Star : BaseObject
    {
        public Star(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {
            // Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
            // Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + Size.Width, Pos.Y, Pos.X, Pos.Y + Size.Height);
            int size = random.Next(2, 5);
            Game.Buffer.Graphics.FillEllipse(Brushes.LightGoldenrodYellow, Pos.X + Size.Width, Pos.Y, size, size);
        }

        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
            if (Pos.X > Game.Width)
            { 
                Pos.X = Game.Width;
                int size = random.Next(1, 3);
                this.Size.Height = size;
                this.Size.Width = size;
            }
        }
    }
}
