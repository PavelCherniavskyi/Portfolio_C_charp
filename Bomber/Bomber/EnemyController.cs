using System;
using System.Collections.Generic;


namespace Bomber
{
    class EnemyController
    {
        #region Variables
        static List<Enemy> enemyArray = new List<Enemy>();
        Map map;
        Panel panel;
        #endregion
        #region Constructors
        public EnemyController(Map map, Panel panel)
        {
            this.map = map;
            this.panel = panel;
        }
        #endregion
        #region Properties
        public static int Count
        {
            get
            {
                return enemyArray.Count;
            }
        }

        #endregion
        #region Methods
        public void AddEnemy(int count)
        {
            Random rd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            int x;
            int y;
            for (int i = 0; i < count; i++)
            {
                while (true)
                {
                    x = rd.Next(2, WindowSettings.GameWindowWidth - 2);
                    y = rd.Next(2, WindowSettings.GameWindowHeight - 2);
                    if (map[y, x] != '=' && map[y, x] != '|' && map[y, x] != Bomb.BombTexture && map[y, x] != Player.PlayerTexture && map[y, x] != '0' && map[y, x] != Enemy.EnemyTexture)
                    {
                        break;
                    }
                }
                enemyArray.Add(new Enemy(map, new int[] { y, x }, this));
            }
        }
        public void RemoveEnemy(Enemy enemy)
        {
            enemyArray.Remove(enemy);
            Panel.Render();
        }
        public static void ClearEnemies()
        {
            enemyArray.Clear();
        }
        public void Update()
        {
            foreach(var a in enemyArray)
            {
                a.MoveTowardsDirection();
            }
        }
        #endregion
    }
}
