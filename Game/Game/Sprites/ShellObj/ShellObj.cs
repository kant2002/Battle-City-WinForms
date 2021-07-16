using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    /// <summary>
    /// Базовый клас для снаряда
    /// </summary>
    [Serializable]
    abstract class ShellObj : CollisionsSprite
    {
        // tru если сдетонировала
        protected bool detonation;
        // tru если в полете
        protected bool isAlive;
        public bool IsAlive { get { return isAlive; } set { isAlive = value; } }

        // Имя танк которому пренадлежит снаряд
        protected KeyObjGame nameTank;
        public KeyObjGame NameTank
        {
            get { return nameTank; }
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="rect">Прямоугольник описывающий позицию обьекта на екране, ширину и высоту</param>
        /// <param name="velocity">Скорость</param>
        /// <param name="direction">Направление движения</param>
        /// <param name="nameTank">Ссылка на танк которому пренадлежит снаряд</param>
        public ShellObj(Rectangle rect, int velocity, Direction direction, KeyObjGame nameTank)
            : base(rect, velocity, direction)
        {
            isAlive = true;
            this.nameTank = nameTank;
            this.velocity = velocity;
            this.direction = direction;

            // Загрузка картинок в зависимости от направления снаряда
            switch (direction)
            {
                case Direction.Up:
                    spriteImage = Game.Properties.Resources.BulletUp;
                    break;
                case Direction.Right:
                    spriteImage = Game.Properties.Resources.BulletRight;
                    break;
                case Direction.Down:
                    spriteImage = Game.Properties.Resources.BulletDown;
                    break;
                case Direction.Left:
                    spriteImage = Game.Properties.Resources.BulletLeft;
                    break;
            }
            // Добавление в список обектов игры
            Level.DictionaryObjGame[KeyObjGame.ShellObj].Add(this);
        }
    }
}
