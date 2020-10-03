using System;
using System.Windows.Forms;
using System.Drawing;

namespace Asteroid
{
    class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        public static BaseObject[] _objs;
        public static BaseObject[] starField;
        private static Bullet[] _bullets;
        private static Asteroid[] _asteroids;
        // Свойства
        // Ширина и высота игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }

        static Game()
        {
        }

        public static void Init(Form form)
        {
            // Графическое устройство для вывода графики            
            Graphics g;
            // Предоставляет доступ к главному буферу графического контекста для текущего приложения
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            // Создаем объект (поверхность рисования) и связываем его с формой
            // Запоминаем размеры формы
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            //Проверка размера экрана
            if (Width > 1000 || Height > 1000)
            {
                throw new ArgumentOutOfRangeException();
            }
            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
           // Load();
            Timer timer = new Timer { Interval = 41 };
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
                obj.Draw();
            foreach (Asteroid obj in _asteroids)
                obj.Draw();
            foreach (BaseObject star in starField)
                star.Draw();
            foreach (Bullet _bullet in _bullets)
            {
                _bullet.Draw();
            }          
            Buffer.Render();
        }

        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
            foreach (BaseObject star in starField)
                star.Update();
            foreach (var _bullet in _bullets)
            {
                _bullet.Update();
            }
            foreach (Asteroid a in _asteroids)
            {
                a.Update();
                foreach (var _bullet in _bullets)
                {
                    if (a.Collision(_bullet)) { System.Media.SystemSounds.Hand.Play(); }
                }               
            }

        }

        public static void Load()
        {
            _objs = new BaseObject[30];
            _bullets = new Bullet[10];
            
            _asteroids = new Asteroid[3];
            var rnd = new Random();
            for (int i = 0; i < 10; i++)
            {
                _bullets[i] = new Bullet(new Point(rnd.Next(-50, 0), rnd.Next(0, Game.Height)), new Point(5, 0), new Size(4, 1));
            }
            for (var i = 0; i < _objs.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _objs[i] = new Star(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r, r), new Size(3, 3));
            }
            for (var i = 0; i < _asteroids.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _asteroids[i] = new Asteroid(new Point(Game.Width, rnd.Next(0, Game.Height)), new Point(-r / 5, r), new Size(r, r));
            }

            starField = new BaseObject[15000];
            Random random = new Random();

            for (int i = 0; i < starField.Length; i++)
                starField[i] = new StarField(new Point(random.Next(-Width, Width), random.Next(-Height, Height)), new Point(10,10), new Size(10, 10), Width, Height,random.Next(1, Width));
        }
    }
}
