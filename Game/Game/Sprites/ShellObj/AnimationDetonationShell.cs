using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    /// <summary>
    /// Анимация взрыва снаряда
    /// </summary>
    [Serializable]
    class AnimationDetonationShell : CollisionShell
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="rect">Прямоугольник описывающий позицию обьекта на екране, ширину и высоту</param>
        /// <param name="velocity">Скорость</param>
        /// <param name="direction">Направление движения</param>
        /// <param name="nameTank">Ссылка на танк которому пренадлежит снаряд</param>
        public AnimationDetonationShell(Rectangle rect, int velocity, Direction direction, KeyObjGame nameTank)
            : base(rect, velocity, direction, nameTank)
        { }

        // Переменная для хранения интервала задержки между сменой действий
        private int interval = 0;

        /// <summary>
        /// Обновление состояния обьекта
        /// </summary>
        public override void Update()
        {
            // Если обьект сдетонировал
            if (detonation)
            {
                if (interval == 0)
                {
                    interval++;
                    // Меняем изображения снаряда на изображения взрыва
                    spriteImage = Game.Properties.Resources.Detonation;
                    // Вычисление положения обьекта
                    Position();

                }
                else if (interval == 3)
                {
                    // Если было столкновение с обьектом
                    if (this.objResponse != null)
                        // Вызываем метод попадания снаряда
                        this.objResponse.Response(this);

                    interval++;
                    // Меняем изображения взрыва
                    spriteImage = Game.Properties.Resources.Detonation2;
                    // Вычисление положения обьекта
                    Position();
                }
                else if (interval == 6)
                {
                    interval++;
                    // Меняем изображения взрыва
                    spriteImage = Game.Properties.Resources.Detonation3;
                    // Вычисление положения обьекта
                    Position();
                }
                else if (interval == 9)
                {
                    // Помичаем обьект как умерший
                    isAlive = false;
                    // Удаляем из списка снарядов
                    Level.DictionaryObjGame[KeyObjGame.ShellObj].Remove(this);
                }
                else
                    interval++;
            }
            else
                // Если обьект не сдетонировал
                base.Update();
        }

        /// <summary>
        /// Вычисление положения обьекта при смене изображения и размеров
        /// </summary>
        private void Position()
        {
            switch (direction)
            {
                case Direction.Up:
                    spriteRectangle.Location = new Point(spriteRectangle.X - ((spriteImage.Width - spriteRectangle.Width) / 2), spriteRectangle.Top - (spriteImage.Height - spriteRectangle.Width) / 2);
                    break;
                case Direction.Right:
                    spriteRectangle.Location = new Point(spriteRectangle.Left - (spriteImage.Width - spriteRectangle.Width) / 2, spriteRectangle.Y - (spriteImage.Height / 2 - spriteRectangle.Height / 2));
                    break;
                case Direction.Down:
                    spriteRectangle.Location = new Point(spriteRectangle.X - (spriteImage.Width / 2 - spriteRectangle.Width / 2), spriteRectangle.Top - (spriteImage.Height - spriteRectangle.Width) / 2);
                    break;
                case Direction.Left:
                    spriteRectangle.Location = new Point(spriteRectangle.Left - (spriteImage.Width - spriteRectangle.Width) / 2, spriteRectangle.Y - (spriteImage.Height / 2 - spriteRectangle.Height / 2));
                    break;
            }
            spriteRectangle.Size = spriteImage.Size;
        }
    }
}
