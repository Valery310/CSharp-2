using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroid
{
    class Asteroid : BaseObject, ICloneable, IComparable, IComparable<Asteroid>
    {
        public int Power { get; set; } = 3;
        public static int DestroyAsteroid = 0;

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
            // Создаем копию астероида
            Asteroid asteroid = new Asteroid(new Point(Pos.X, Pos.Y), new Point(Dir.X, Dir.Y), new Size(Size.Width, Size.Height)) { Power = Power };
            // Не забываем скопировать новому астероиду Power нашего астероида
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
                    OnEventLog(this, new EventMessage("Обнаружено столкновение со снарядом!"));
                    Die();
                    DestroyAsteroid++;
                }
                return true;
            }
            return false;
        }

        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X < 0) Pos.X = Game.Width;
            if (Pos.X > Game.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        }

        public int CompareTo(object obj)
        {
            if (obj is Asteroid temp)
            {
                if (Power > temp.Power)
                    return 1;
                if (Power < temp.Power)
                    return -1;
                else
                    return 0;
            }
            OnEventLog(this, new EventMessage("Parameter is not а Asteroid!"));
            throw new ArgumentException("Parameter is not а Asteroid!");

        }

        public interface IComparable<T>
        {
            int CompareTo(T obj);
        }

        public int CompareTo(Asteroid obj)
        {
            if (Power > obj.Power)
                return 1;
            if (Power < obj.Power)
                return -1;
            return 0;
        }

        public void Die()
        {
            OnEventLog(this, new EventMessage("Астеройд уничтожен!"));         
        }

    }

}
