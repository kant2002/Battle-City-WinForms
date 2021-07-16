using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    /// <summary>
    /// Екран меню
    /// </summary>
    class Menu
    {
        private StateMenu stateMenu;
        private static Label menuControl;
        private Label player;
        private Label saveGame;
        private Label loadGame;
        private Label marker;
        private int position = 0;

        /// <summary>
        /// конструктор екрана игры
        /// </summary>
        /// <param name="control">принимает родительский Control</param>
        public Menu(Control control)
        {
            stateMenu = StateMenu.Game;

            menuControl = new Label();
            menuControl.Parent = control;
            menuControl.Location = new Point(0, 0);
            menuControl.Image = Game.Properties.Resources.Menu;
            menuControl.Size = menuControl.Image.Size;

            player = new Label();
            player.Parent = menuControl;
            player.Location = new Point(265, 770);
            player.BackColor = Color.Transparent;
            player.Font = new Font("COURER", 20, FontStyle.Bold);
            player.ForeColor = Color.White;
            player.TextAlign = ContentAlignment.MiddleLeft;
            player.Size = new Size(200, 30);
            player.Text = "1 PLAYER";

            saveGame = new Label();
            saveGame.Parent = menuControl;
            saveGame.Location = new Point(265, 800);
            saveGame.BackColor = Color.Transparent;
            saveGame.Font = new Font("COURER", 20, FontStyle.Bold);
            saveGame.ForeColor = Color.White;
            saveGame.TextAlign = ContentAlignment.MiddleLeft;
            saveGame.Size = new Size(200, 30);
            saveGame.Text = "SAVE GAME";

            loadGame = new Label();
            loadGame.Parent = menuControl;
            loadGame.Location = new Point(265, 830);
            loadGame.BackColor = Color.Transparent;
            loadGame.Font = new Font("COURER", 20, FontStyle.Bold);
            loadGame.ForeColor = Color.White;
            loadGame.TextAlign = ContentAlignment.MiddleLeft;
            loadGame.Size = new Size(200, 30);
            loadGame.Text = "LOAD GAME";

            marker = new Label();
            marker.Parent = menuControl;
            marker.Image = Game.Properties.Resources.Marker;
            marker.Size = marker.Image.Size;
        }

        public StateMenu StateMenu { get { return stateMenu; } set { stateMenu = value; } }

        public static Control MenuControl { get { return menuControl; } }

        /// <summary>
        /// Сброс позиции отображения меню
        /// </summary>
        public void ResetPosition()
        {
            position = 0;
        }

        /// <summary>
        /// Обновление состояния обьекта
        /// </summary>
        public void Update()
        {
            if (menuControl.Top > menuControl.Parent.Height - menuControl.Height - 30)
            {
                position -= 4;
            }
            else if (Keyboard.Down)
            {
                if (stateMenu < StateMenu.LoadGame)
                {
                    stateMenu++;
                    Thread.Sleep(200);
                }
            }
            else if (Keyboard.Up)
            {
                if (stateMenu > StateMenu.Game)
                {
                    stateMenu--;
                    Thread.Sleep(200);
                }
            }

            switch (stateMenu)
            {
                case StateMenu.Game:
                    marker.Location = new Point(210, 765);
                    break;
                case StateMenu.SaveGame:
                    marker.Location = new Point(210, 795);
                    break;
                case StateMenu.LoadGame:
                    marker.Location = new Point(210, 825);
                    break;
            }
        }

        /// <summary>
        /// Рисует обьект
        /// </summary>
        public void Drow()
        {
            menuControl.Location = new Point(-40, position);
        }
    }
}
