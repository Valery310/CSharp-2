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
                Width = 1000,// Screen.PrimaryScreen.Bounds.Width,
                Height = 1000// Screen.PrimaryScreen.Bounds.Height
            };

            try
            {
                Game.Init(form);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
                form.Width = 800;
                form.Height = 800;
                Game.Init(form);
            }
            
            form.Show();
            Game.Load();
            Game.Draw();
            Application.Run(form);
        }
    }
}
