using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroid
{
    class MedicalKit: BaseObject
    {
        public int Power { get; set; } = 3;

        public MedicalKit(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Power = 1;
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.FillRectangle(Brushes.Green, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override bool Collision(BaseObject o)
        {
            if (o.Rect.IntersectsWith(this.Rect))
            {
                if (o is Ship)
                {
                    System.Media.SystemSounds.Hand.Play();
                    (o as Ship).EatMedicalKit(this);
                    OnEventLog(this, new EventMessage("Получена аптечка!"));
                    Die(this);
                }
                return true;
            }
            return false;
        }

        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X < 0) Die(this);
            if (Pos.X > Game.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        }

        public static void  Die(MedicalKit medicalKit) 
        {
            medicalKit = null;
        }
    }
}
