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
    /// Екран Game Over
    /// </summary>
    class GameOver
    {
        private static Label gameOver;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="control">Родительский Control</param>
        public GameOver(Control control)
        {

            gameOver = new Label();
            gameOver.Parent = control;
            gameOver.Location = new Point(0, 0);
            gameOver.Size = new Size(control.ClientRectangle.Width, control.ClientRectangle.Height);
            gameOver.BackColor = Color.Black;
            gameOver.Image = Game.Properties.Resources.Game_Over;
            gameOver.ImageAlign = ContentAlignment.MiddleCenter;
        }

        /// <summary>
        /// Обновление состояния обьекта
        /// </summary>
        public void Update()
        {
            gameOver.BringToFront();
        }

        public void Drow()
        {
            gameOver.Refresh();
        }
    }
}
