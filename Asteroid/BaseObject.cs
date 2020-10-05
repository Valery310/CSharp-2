using System;
using System.Drawing;

namespace Asteroid
{
    abstract class BaseObject : ICollision
    {
        public Point Pos;
        protected Point Dir;
        protected Size Size;
        protected Random random;
        public delegate void Message();
        protected BaseObject(Point pos, Point dir, Size size)
        {
            random = new Random(Environment.TickCount);
            Pos = pos;
            Dir = dir;
            Size = size;
            if (Pos.X > Game.Width+150 || Pos.Y > Game.Height + 150)
            {
                throw new GameObjectException("Объект располагается слишком далеко за пределами экрана.");
            }
            if (Dir.X > 150 || Dir.Y > 150)
            {
                throw new GameObjectException("Слишком высокая скорость объекта.");
            }
            if (Size.Width > 150 || Size.Height >  150)
            {
                throw new GameObjectException("Слишком большой размер объекта.");
            }
        }
        public abstract void Draw();

        public abstract void Update();

        // Так как переданный объект тоже должен будет реализовывать интерфейс ICollision, мы 
        // можем использовать его свойство Rect и метод IntersectsWith для обнаружения пересечения с
        // нашим объектом (а можно наоборот)
        public virtual bool Collision(BaseObject o) 
        {
            if (o.Rect.IntersectsWith(this.Rect))
            {
                return true;
            }
            return false;              
        }

        public Rectangle Rect => new Rectangle(Pos, Size);
    }
}

