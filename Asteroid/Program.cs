using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Windows.Forms;

namespace Asteroid
{
    class Program
    {
        static void Main(string[] args)
        {
            Form form = new Form
            {
                Width = Screen.PrimaryScreen.Bounds.Width,
                Height = Screen.PrimaryScreen.Bounds.Height
            };

            try
            {
                Game.Init(form);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
                form.Width = 1000;
                form.Height = 1000;
                Game.Init(form);
            }
            
            form.Show();
            Game.Load();
            Game.Draw();
            Application.Run(form);
        }
    }
}
