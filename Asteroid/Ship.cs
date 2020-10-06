using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroid
{
    class Ship: BaseObject
    {
        //private int _energy = 100;
        public static event Message MessageDie;

        public int Energy { get; protected set; } = 100;

        public void EnergyLow(int n)
        {
            OnEventLog(this, new EventMessage($"Получен урон! -{n} energy"));
            Energy -= n;
        }

        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.Wheat, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public override void Update()
        {
        }

        public void EatMedicalKit(MedicalKit medicalKit) 
        {
            var t = Energy + medicalKit.Power;
            if (t> 100)
            {
                Energy = 100;
            }
            else
            {
                Energy += medicalKit.Power;
            }            
        }

        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
        }
        public void Down()
        {
            if (Pos.Y < Game.Height) Pos.Y = Pos.Y + Dir.Y;
        }
        public void Die()
        {
            OnEventLog(this, new EventMessage("Корабль уничтожен!"));
            MessageDie?.Invoke();
        }
    }

}
