using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    /// <summary>
    /// Екран игры
    /// </summary>
    class ScreenGame : Label
    {
        /// <summary>
        /// конструктор екрана игры
        /// </summary>
        /// <param name="control">принимает родительский Control</param>
        public ScreenGame(Control control)
        {
            Parent = control;
            this.BackgroundImage = Game.Properties.Resources.PlayingField;
            Location = new Point();
            ClientSize = new Size(SizeGame.WidtchWindowGame, SizeGame.HeighWindowGame);
            BackColor = Color.Black;
            GraphicsOption();
        }
        private void GraphicsOption()
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            // смещение для обьектов игры
            Point offset = new Point(SizeGame.WidtchSmoll, SizeGame.HeighSmoll);

            base.OnPaint(e);
            Graphics g = e.Graphics;

            Image image1 = Game.Properties.Resources._11; // картинка 1
            // отобразение количества жизней игрока 
            g.TranslateTransform(580, 320);
            g.DrawImage(image1, 0, 0, image1.Width, image1.Height);
            g.ResetTransform();
            // отображение уровня игры 1
            g.TranslateTransform(580, 440);
            g.DrawImage(image1, 0, 0, image1.Width, image1.Height);
            g.ResetTransform();

            // прорисовка всел обьектов игры
            foreach (var list in Level.DictionaryObjGame.Values)
            {
                foreach (var item in list)
                {
                    item.Draw(g, offset);
                }
            }
        }

    }
}
