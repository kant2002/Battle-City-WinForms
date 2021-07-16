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
    /// Малый танк Player
    /// </summary>
    [Serializable]
    class SmallTankPlayer : DrivingTankPlayer
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="position">Положение на карте</param>
        /// <param name="direction">Направление</param>
        public SmallTankPlayer(Point position, Direction direction)
            : base(new Rectangle(position.X, position.Y, SizeGame.WidtchBig, SizeGame.HeighBig), 3, direction)
        {
            moweLeft = new Image[] {
                Game.Properties.Resources.SmallTankPlayerLeft_1,
                Game.Properties.Resources.SmallTankPlayerLeft_2 };
            moweRight = new Image[] {
                Game.Properties.Resources.SmallTankPlayerRight_1,
                Game.Properties.Resources.SmallTankPlayerRight_2 };
            moweUp = new Image[] {
                Game.Properties.Resources.SmallTankPlayerUp_1,
                Game.Properties.Resources.SmallTankPlayerUp_2 };
            moweDown = new Image[] {
                Game.Properties.Resources.SmallTankPlayerDown_1,
                Game.Properties.Resources.SmallTankPlayerDown_2 };
            Animation();
        }

        /// <summary>
        /// Вызывается при попадании снаряда в обьект
        /// </summary>
        /// <param name="shellObj">Ссылка на снаряд</param>
        public override void Response(ShellObj shellObj)
        {
            // Если снаряд выпущен не етим танком
            if (shellObj != this.shell)
                // Удаление со списка обектов игры
                Level.DictionaryObjGame[KeyObjGame.TankPlayer].Remove(this);

            // Миняем статус Level yf game over
            Level.StateOfLevel = StateOfLevel.GameOver;
        }

        /// <summary>
        /// Обновление состояния обьекта
        /// </summary>
        public override void Update()
        {
            this.Driving();
            this.CollisionsBoundariesPlayingField();
            this.CollisionObjLevel();
            if (!isParking)
                Animation();
        }
    }
}
