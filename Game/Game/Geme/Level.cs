using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game
{
    [Serializable]
    class Level
    {
        public static StateOfLevel StateOfLevel;
        private int intervalGameOver;
        private ImgGameOver imgGameOver;

        public static Dictionary<KeyObjGame, List<IObjGame>> DictionaryObjGame;
        private Dictionary<KeyObjGame, List<IObjGame>> dictionaryObjGame;

        private List<IObjGame> listTankEnemy;
        private List<IObjGame> listEagle;
        private List<IObjGame> listTankPlayer;
        private List<IObjGame> listBrickWall;
        private List<IObjGame> listBullet;
        private List<IObjGame> listInformationTank;
        private TankPlayer tankPlayer;
        private TankEnemy tankEnemy;
        private Eagle eagle;

        /// <summary>
        /// место постоянного появления объекта
        /// </summary>
        private Point respawnEagle;
        private Point respawnPlayer;
        private Point respawnEnemy1;
        private Point respawnEnemy2;
        private Point respawnEnemy3;

        private Random random;
        private int countEnemy;
        private Rectangle rec;

        public Level()
        {
            dictionaryObjGame = new Dictionary<KeyObjGame, List<IObjGame>>();

            listEagle = new List<IObjGame>();
            listTankPlayer = new List<IObjGame>();
            listBrickWall = new List<IObjGame>();
            listBullet = new List<IObjGame>();
            listTankEnemy = new List<IObjGame>();
            listInformationTank = new List<IObjGame>();

            respawnEagle = new Point(12 * SizeGame.WidtchSmoll, 22 * SizeGame.HeighSmoll);
            respawnPlayer = new Point(9 * SizeGame.WidtchSmoll, 22 * SizeGame.HeighSmoll);
            respawnEnemy1 = new Point();
            respawnEnemy2 = new Point(12 * SizeGame.WidtchSmoll, 0);
            respawnEnemy3 = new Point(24 * SizeGame.WidtchSmoll, 0);
            
            random = new Random();

            dictionaryObjGame.Add(KeyObjGame.TankEnemy, listTankEnemy);
            dictionaryObjGame.Add(KeyObjGame.InformationTank, listInformationTank);
            dictionaryObjGame.Add(KeyObjGame.TankPlayer, listTankPlayer);
            dictionaryObjGame.Add(KeyObjGame.Eagle, listEagle);
            dictionaryObjGame.Add(KeyObjGame.BrickWall, listBrickWall);
            dictionaryObjGame.Add(KeyObjGame.ShellObj, listBullet);

            Level.DictionaryObjGame = dictionaryObjGame;
        }

        public void LodLevel()
        {
            imgGameOver = new ImgGameOver();
            intervalGameOver = SizeGame.HeighWindowGame;
            StateOfLevel = StateOfLevel.Game;

            for (int i = 1; i <= 10; i++)
            {
                listInformationTank.Add(new InformationTank(new Point(540, i * 18)));
                listInformationTank.Add(new InformationTank(new Point(560, i * 18)));
            }

            string[] linesTileMap = Game.Properties.Resources.Level_1.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            int x = 0, y = 0;
            foreach (string line in linesTileMap)
            {
                foreach (char c in line)
                {
                    if (c == 'B')
                    {
                        listBrickWall.Add(new BrickWall(new Point(x, y)));
                    }

                    x += SizeGame.WidtchSmoll;
                }
                x = 0;
                y += SizeGame.HeighSmoll;
            }

            eagle = new Eagle(respawnEagle);

            tankPlayer = new SmallTankPlayer(respawnPlayer, Direction.Up);

            tankEnemy = new PlainTankEnemy(respawnEnemy1, Direction.Down);
            listTankEnemy.Add(tankEnemy);

            tankEnemy = new PlainTankEnemy(respawnEnemy2, Direction.Down);
            listTankEnemy.Add(tankEnemy);

            tankEnemy = new PlainTankEnemy(respawnEnemy3, Direction.Down);
            listTankEnemy.Add(tankEnemy);

            listTankPlayer.Add(tankPlayer);
            listEagle.Add(eagle);

            countEnemy = 3;            
        }

        public void InitDictionaru()
        {
            Level.DictionaryObjGame = dictionaryObjGame;
        }

        public void Drow()
        {
            BattleCity.ScreenGame.Invalidate(BattleCity.ScreenGame.ClientRectangle);
        }
        

        public void Update()
        {
            for (int i = 0; i < listTankEnemy.Count; i++)
            {
                listTankEnemy[i].Update();
            }
            for (int i = 0; i < listBullet.Count; i++)
            {
                listBullet[i].Update();
            }

            if (StateOfLevel != StateOfLevel.GameOver)
            {
                tankPlayer.Update();
            }
            else if (intervalGameOver == SizeGame.HeighWindowGame / 2)
            {
                BattleCity.StateOfGame = StateOfGame.GameOver;
            }
            else if (intervalGameOver == SizeGame.HeighWindowGame)
            {
                listEagle.Add(imgGameOver);
                intervalGameOver--;
            }
            else
            {
                imgGameOver.Update();
                intervalGameOver--;
            }


            if (dictionaryObjGame[KeyObjGame.TankEnemy].Count < 3 && countEnemy < 20)
            {
                AddEnemy();
            }
        }

        private void AddEnemy()
        {
            int selection = random.Next(0, 2);
            Point respawn;
            switch (selection)
            {
                case 0:
                    respawn = respawnEnemy1;
                    break;
                case 1:
                    respawn = respawnEnemy2;
                    break;
                default:
                    respawn = respawnEnemy3;
                    break;
            }
            rec = new Rectangle(respawn, new Size(SizeGame.WidtchBig, SizeGame.HeighBig));
            foreach (var KeyValue in dictionaryObjGame)
            {
                if (KeyValue.Key == KeyObjGame.BrickWall) continue;
                foreach (var sprite in KeyValue.Value)
                {
                    if (rec.IntersectsWith(sprite.Rect))
                        return;
                }
            }
            listTankEnemy.Add(new PlainTankEnemy(respawn, Direction.Down));
            countEnemy++;
        }

        public void Clear()
        {
            foreach (var list in dictionaryObjGame.Values)
            {
                list.Clear();
            }
        }
    }
}
