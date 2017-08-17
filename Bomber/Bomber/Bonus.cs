using System;


namespace Bomber
{
    class Bonus
    {
        static DateTime timer = DateTime.Now;
        static TimeSpan interval = TimeSpan.FromSeconds(30);
        static Random rd = new Random();
        static char bombBonus = 'b';
        static char lifeBonus = 'l';

        public static char BombBonus
        {
            get { return bombBonus; }
        }
        public static char LifeBonus
        {
            get { return lifeBonus; }
        }
        static public void Update(Map map)
        {
            if (DateTime.Now > timer + interval)
            {
                int x;
                int y;
                while (true)
                {
                    x = rd.Next(2, WindowSettings.GameWindowWidth - 2);
                    y = rd.Next(2, WindowSettings.GameWindowHeight - 2);
                    if (map[y, x] != '=' && map[y, x] != '|' && map[y, x] != Bomb.BombTexture && map[y, x] != Player.PlayerTexture && map[y, x] != '0' && map[y, x] != Enemy.EnemyTexture)
                    {
                        break;
                    }
                }
                int temp = rd.Next(1, 3);
                if(temp == 1)
                    map[y, x] = bombBonus;
                else
                    map[y, x] = lifeBonus;

                ConsoleColor forCol = Console.ForegroundColor;
                ConsoleColor backCol = Console.BackgroundColor;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.SetCursorPosition(x, y);
                Console.Write(map[y, x]);
                Console.ForegroundColor = forCol;
                Console.BackgroundColor = backCol;
                timer = DateTime.Now;
            }

        }
    }
}
