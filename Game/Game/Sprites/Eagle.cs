using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    /// <summary>
    /// Орел штаба playera
    /// </summary>
    [Serializable]
    class Eagle : Sprite
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="respawn">Позицыя на карте</param>
        public Eagle(Point respawn)
            : base(new Rectangle(respawn.X, respawn.Y, SizeGame.WidtchBig, SizeGame.HeighBig))
        {
            spriteImage = Game.Properties.Resources.Eagle;//todo
        }

        /// <summary>
        /// Вызывается при попадании снаряда в обьект
        /// </summary>
        /// <param name="shellObj">Ссылка на снаряд</param>
        public override void Response(ShellObj shellObj)
        {
            // Устанавливаем картинку убитого орла
            spriteImage = Game.Properties.Resources.Eagle2; // todo
            // Миняем состояние Level
            Level.StateOfLevel = StateOfLevel.GameOver;
        }
    }
}
