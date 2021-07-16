using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    /// <summary>
    /// Менеджер игры Battle City
    /// </summary>
    class BattleCity
    {
        // Уровень
        private Level level;
        // Состояние игры
        public static StateOfGame StateOfGame;

        // Екран игры
        private static ScreenGame screenGame;
        // Екран меню
        private Menu menu;
        // Екран Game Over
        private GameOver gameOver;
        // Таймер запуска цыклов игры
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="form">Родительская Forma</param>
        public BattleCity(Form form)
        {
            form.ClientSize = new Size(SizeGame.WidtchWindowGame, SizeGame.HeighWindowGame);
            form.Icon = Game.Properties.Resources.Ico;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Text = "Battle City";

            level = new Level();
            level.LodLevel();

            menu = new Menu(form);
            gameOver = new GameOver(form);
            screenGame = new ScreenGame(form);

            StateOfGame = StateOfGame.Menu;
            Menu.MenuControl.BringToFront();
        }

        public static Control ScreenGame { get { return screenGame; } }

        // Запуск игры
        public void Play()
        {
            timer.Interval = 20;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        //  Запуск нового цыкла игры
        private void Timer_Tick(object sender, EventArgs e)
        {
            Update();
            Drow();
        }

        /// <summary>
        /// Отрисовка екранов игры
        /// </summary>
        private void Drow()
        {
            switch (StateOfGame)
            {
                case StateOfGame.Menu:
                    menu.Drow();
                    break;
                case StateOfGame.Game:
                    level.Drow();
                    break;
                case StateOfGame.GameOver:
                    gameOver.Drow();
                    break;
            }
        }

        /// <summary>
        /// Обновление состояния
        /// </summary>
        private void Update()
        {
            ProcessingKeystrokes();

            switch (StateOfGame)
            {
                case StateOfGame.Menu:
                    menu.Update();
                    break;
                case StateOfGame.Game:
                    level.Update();
                    break;
                case StateOfGame.GameOver:
                    level.Clear();
                    gameOver.Update();
                    break;
            }
        }

        /// <summary>
        /// Обработка нажатия клавиш
        /// </summary>
        private void ProcessingKeystrokes()
        {
            if (Keyboard.Escape)
            {
                StateOfGame = StateOfGame.Menu;
                Menu.MenuControl.BringToFront();
                Thread.Sleep(100);
            }
            else
            if (Keyboard.Enter && menu.StateMenu == StateMenu.SaveGame)
            {
                timer.Stop();
                // Сериализация
                var stream = new FileStream("Serialized", FileMode.Create);
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, level);
                stream.Close();

                menu.StateMenu = StateMenu.Game;
                StateOfGame = StateOfGame.Menu;
                Menu.MenuControl.BringToFront();
                Thread.Sleep(100);
                timer.Start();
            }
            else if (Keyboard.Enter && menu.StateMenu == StateMenu.LoadGame)
            {
                timer.Stop();
                // Десериализвция
                level.Clear();
                var stream = new FileStream("Serialized", FileMode.Open);
                var formatter = new BinaryFormatter();
                level = formatter.Deserialize(stream) as Level;
                stream.Close();

                menu.StateMenu = StateMenu.Game;
                StateOfGame = StateOfGame.Menu;
                Menu.MenuControl.BringToFront();
                Thread.Sleep(100);
                level.InitDictionaru();
                timer.Start();
            }
            else if (Keyboard.Enter && StateOfGame == StateOfGame.GameOver)
            {
                level.LodLevel();
                StateOfGame = StateOfGame.Menu;
                Menu.MenuControl.BringToFront();
                Thread.Sleep(100);
            }
            else if (Keyboard.Enter && StateOfGame == StateOfGame.Menu && menu.StateMenu == StateMenu.Game)
            {
                StateOfGame = StateOfGame.Game;
                screenGame.BringToFront();
                menu.ResetPosition();
            }
        }
    }
}
