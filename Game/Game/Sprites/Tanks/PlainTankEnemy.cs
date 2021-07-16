using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    /// <summary>
    /// Вражеский обычный танк 
    /// </summary>
    [Serializable]
    class PlainTankEnemy : DrivingTankEnemy
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="position"></param>
        /// <param name="direction"></param>
        public PlainTankEnemy(Point position, Direction direction)
            : base(new Rectangle(position.X, position.Y, SizeGame.WidtchBig, SizeGame.HeighBig), 3, direction)
        {
            moweLeft = new Image[] {
                Game.Properties.Resources.EnemyPlainTankLeft1,
                Game.Properties.Resources.EnemyPlainTankLeft2 };
            moweRight = new Image[] {
                Game.Properties.Resources.EnemyPlainTankRight1,
                Game.Properties.Resources.EnemyPlainTankRight2 };
            moweUp = new Image[] {
                Game.Properties.Resources.EnemyPlainTankUp1,
                Game.Properties.Resources.EnemyPlainTankUp2 };
            moweDown = new Image[] {
                Game.Properties.Resources.EnemyPlainTankDown1,
                Game.Properties.Resources.EnemyPlainTankDown2 };

            spriteImage = moweDown[0];
        }

        /// <summary>
        /// Вызывается при попадании снаряда в обьект
        /// </summary>
        /// <param name="shellObj">Ссылка на снаряд</param>
        public override void Response(ShellObj shellObj)
        {
            // Если снаряд выпущен не етим танком
            if (shellObj != this.shell)
            {
                // Удаление из списка обьектов игры
                Level.DictionaryObjGame[KeyObjGame.TankEnemy].Remove(this);

                // Удаление из списка одново танка для информационной боковой панели
                var list = Level.DictionaryObjGame[KeyObjGame.InformationTank];
                list.RemoveAt(list.Count - 1);
            }

        }

        /// <summary>
        /// Обновление состояния обьекта
        /// </summary>
        public override void Update()
        {
            this.Driving();
            this.CollisionsBoundariesPlayingField();
            this.CollisionObjLevel();
            //if (!isParking)
            Animation();
        }
    }
}
