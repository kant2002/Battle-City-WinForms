using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    /// <summary>
    /// Отображение в завершения игры Level
    /// </summary>
    [Serializable]
    class ImgGameOver : Sprite
    {       
        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public ImgGameOver()
            : base(new Rectangle(SizeGame.WidtchSmoll * 11, SizeGame.HeighWindowGame, Game.Properties.Resources.game_over1.Width, Game.Properties.Resources.game_over1.Height))
        {
            spriteImage = Game.Properties.Resources.game_over1;
        }
        
        /// <summary>
        /// Обновление состояния обьекта
        /// </summary>
        public override void Update()
        {
            if (spriteRectangle.Y > SizeGame.HeighWindowGame / 2 - spriteImage.Height / 2)
                spriteRectangle.Y -= 5;
        }
    }
}
