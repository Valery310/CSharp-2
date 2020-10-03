using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroid
{
    class Asteroid : BaseObject, ICloneable
    {
        public int Power { get; set; }
        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Power = 1;
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.White, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public object Clone()
        {
            // Создаем копию
            Asteroid asteroid = new Asteroid(new Point(Pos.X, Pos.Y), new Point(Dir.X, Dir.Y), new Size(Size.Width, Size.Height));
            // Не забываем скопировать новому астероиду Power нашего астероида
            asteroid.Power = Power;
            return asteroid;
        }

        public override bool Collision(BaseObject o)
        {
            if (o.Rect.IntersectsWith(this.Rect))
            {
                if (o is Bullet)
                {
                    System.Media.SystemSounds.Hand.Play();

                        this.Pos.X = Game.Width;
                        this.Pos.Y = random.Next(0, Game.Height);
                        o.Pos.X = 0;
                        o.Pos.Y = random.Next(0, Game.Height);                
                }
                return true;
            }
            return false;
        }

        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X < 0) Pos.X = Game.Width;// Dir.X = -Dir.X;
            if (Pos.X > Game.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        }
    }

}
