using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.IO;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;

namespace Asteroid
{
    class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        public static BaseObject[] _objs;
        public static BaseObject[] starField;
        private static List<Bullet> _bullets;
        private static List<Asteroid> _asteroids;
        private static Ship _ship;
        private static Timer _timer = new Timer() { Interval = 41 };
        public static Random Rnd = new Random();
        public static event EventHandler<EventMessage> GameLog;
        public static FileStream fs;
        public static MedicalKit medical;
        // private static Bullet _bullet;
        // Свойства
        // Ширина и высота игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }

        static Game()
        {
        }

        public static void Init(Form form)
        {
            FileCreate();
            GameLog += ConsoleOutput;
            GameLog += FileOutput;
            BaseObject.EventLog += ConsoleOutput;
            BaseObject.EventLog += FileOutput;
            GameLog.Invoke(null, new EventMessage("Началась инициализация."));
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
            //Подписываемся на событие уничтожения корабля
            _ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(10, 10));
            Ship.MessageDie += Finish;
            _timer.Start();
            _timer.Tick += Timer_Tick;
            //Добавляем обработчик событий нажатия клавиши
            form.KeyDown += Form_KeyDown;
            GameLog.Invoke(null, new EventMessage("Инициализация выполнена успешно."));
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space){ _bullets.Add(new Bullet(new Point(_ship.Rect.X + 10, _ship.Rect.Y + 4), new Point(4, 0), new Size(4, 1))); GameLog.Invoke(null, new EventMessage("Нажата клавиша \"Space\"")); }
            if (e.KeyCode == Keys.Up) { _ship.Up(); GameLog.Invoke(null, new EventMessage("Нажата клавиша \"Up\"")); }
            if (e.KeyCode == Keys.Down) { _ship.Down(); GameLog.Invoke(null, new EventMessage("Нажата клавиша \"Down\"")); }
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
                obj?.Draw();
            foreach (Asteroid obj in _asteroids)
                obj?.Draw();
            foreach (BaseObject star in starField)
                star?.Draw();
            foreach (Bullet _bullet in _bullets)
            {
                _bullet?.Draw();
            }
            medical?.Draw();
            _ship?.Draw();
            if (_ship != null)
                Buffer.Graphics.DrawString("Energy:" + _ship.Energy, SystemFonts.DefaultFont, Brushes.White, 0, 0);
                Buffer.Graphics.DrawString("Destroyed:" + Asteroid.DestroyAsteroid, SystemFonts.DefaultFont, Brushes.White, 0, 12);
            Buffer.Render();
        }

        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj?.Update();
            foreach (BaseObject star in starField)
                star?.Update();
            foreach (var _bullet in _bullets)
            {
                _bullet?.Update();
            }

           
            medical?.Update();

            for (int a = 0; a < _asteroids.Count; a++)
            {
                if (_asteroids[a] == null) continue;
                _asteroids[a]?.Update();

                    for (int _bullet = 0; _bullet < _bullets.Count; _bullet++)
                    {
                        if (_asteroids[a] != null && _asteroids[a].Collision(_bullets[_bullet]))
                        {
                             System.Media.SystemSounds.Hand.Play();
                            _bullets.RemoveAt(_bullet);
                            _asteroids.RemoveAsteroid(a, Rnd);
                            _bullet = _bullet > 0 ? _bullet--: _bullet;
                            a = a > 0 ? --a: a;
                        }
                    if ( _bullet < _bullets.Count &&!(_bullets[_bullet]?.Pos.X < (Game.Width - 1)))
                    {
                        _bullets.RemoveAt(_bullet);
                        _bullet = _bullet > 0 ? _bullet-- : _bullet;
                    }
                }
                    if (_asteroids[a] != null || !_ship.Collision(_asteroids[a]))
                    {
                        continue;
                    }
                    _ship?.EnergyLow(Rnd.Next(1, 10));
                    System.Media.SystemSounds.Asterisk.Play();
                    if (_ship.Energy <= 0) _ship?.Die();
            }

        }

        public static void Load()
        {
            GameLog.Invoke(null, new EventMessage("Начало загрузки объектов"));
            _objs = new BaseObject[30];
            _bullets = new List<Bullet>();            
            _asteroids = new List<Asteroid>(3);
            var rnd = new Random();

            for (var i = 0; i < _objs.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _objs[i] = new Star(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r, r), new Size(3, 3));
            }
            for (var i = 0; i < _asteroids.Capacity; i++)
            {
                int r = rnd.Next(5, 50);
                _asteroids.Add(new Asteroid(new Point(Game.Width, rnd.Next(0, Game.Height)), new Point(-r /rnd.Next(3, 8), r/rnd.Next(1, 5)), new Size(r, r)));
            }

            //int medicalKit = Rnd.Next(1, 100);
            //int mkr = medicalKit = Rnd.Next(5, 50);

            //if (medicalKit > 20 && medicalKit < 50)
            //{
            //    medical = new MedicalKit(new Point(Game.Width, Rnd.Next(0, Game.Height)), new Point(-mkr / 5, mkr), new Size(mkr, mkr));
            //}

            starField = new BaseObject[5000];
            Random random = new Random();

            for (int i = 0; i < starField.Length; i++)
            {
                int r = rnd.Next(5, 50);
                starField[i] = new StarField(new Point(random.Next(-Width, Width), random.Next(-Height, Height)), new Point(-r / 5, r), new Size(10, 10), Width, Height, random.Next(1, Width));
            }
            GameLog.Invoke(null, new EventMessage("Объекты загружены успешно."));
        }

        public static void Finish()
        {
            _timer.Stop();
            Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Render();
            GameLog.Invoke(null, new EventMessage("Игра окончена."));
        }

        public async static void ConsoleOutput(object obj, EventMessage eventMessage)
        {
            await Task.Run(() =>
            {
                Console.WriteLine(obj?.GetType() + ":" + eventMessage.Message);
            });
        }

        public static void FileCreate() 
        {
            string path = Application.StartupPath + "\\log.txt";
            
            if (!File.Exists(path))
            {
                FileStream fs = File.Create(path);
                fs.Close();
            }
            
            }

        public async static void FileOutput(object obj, EventMessage eventMessage)
        {
            await Task.Run(()=> {
                fs = new FileStream(Application.StartupPath + "\\log.txt",
                FileMode.Append, FileAccess.Write, FileShare.Write,
                bufferSize: 4096, useAsync: true);
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    sw.WriteLineAsync(obj?.GetType() + ":" + eventMessage.Message);
                    sw.Close();
                }
            });
          
                        
        }
    }
}
