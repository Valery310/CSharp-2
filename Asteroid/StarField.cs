using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroid
{
    class StarField : BaseObject
    {
        protected float Z;
        protected float X;
        protected float Y;
        protected float _Size;
        protected int Width;
        protected int Height;
        protected static new Random random;

        static StarField() 
        {
            random = new Random();
        }

        public StarField(Point pos, Point dir, Size size, int width, int height, int z) : base(pos, dir, size)
        { 
            Z = z; 
            Width = width; 
            Height = height;
            _Size = Map(Z, 0, Width, 5, 0);
            X = Map((Pos.X / Z), 0, 1, 0, Width) + Width / 2;
            Y = Map((Pos.Y / Z), 0, 1, 0, Height) + Height / 2;  
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.LightGoldenrodYellow, X, Y, _Size, _Size);
        }

        private static float Map(float n, float start1, float end1, float start2, float end2) 
        {
            return ((n - start1)/(end1 - start1)) * (end2 - start2) + start2;
        }

        public override void Update()
        {
            X = X + Dir.X;
            if (X < -Width)
            {
                X = Game.Width + random.Next(0, 250);
                Y = Game.Height + random.Next(-Height, Height);
                int r = random.Next(5, 50);
                //Dir.X = -r/5;
                int size = random.Next(1, Width);
                _Size = Map(size, 0, Width, 5, 0);
            }
        }
    }
}
