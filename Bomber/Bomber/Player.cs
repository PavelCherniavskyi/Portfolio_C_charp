using System;

namespace Bomber
{
    
    class Player : IMove, IBomber
    {
        #region Variables
        private static Direction direction = Direction.Right;
        #endregion
        #region Constructors
        public Player()
        {
            Bomb.ExplosionEvent += GotHit;
        }
        #endregion
        #region Properties
        public static int BombPower { get; set; } = 1;

        public int[] CurrentPos { get; } = new int[2] { 10, 10 };

        public static char PlayerTexture { get; } = 'P';

        public static char PlayerDirection { get; } = '+';

        public static int Lives { get; set; } = 3;

        #endregion
        #region Methods
        public void ChangeDirection(Direction d, Map map)
        {
            direction = d;
            ClearDirectionTrace(map, CurrentPos[1], CurrentPos[0]);
            RenderDirection(map);
        }
        void GotHit(int x, int y)
        {
            
            if (CurrentPos[0] != y || CurrentPos[1] != x)
                return;
            Lives--;
            Panel.Render();
        }
        public void PlantBomb(Map map)
        {
            switch (direction)
            {
                case Direction.Left:
                    CreateBomb(new Tuple<int, int, Map>(CurrentPos[1] - 1, CurrentPos[0], map));
                    break;
                case Direction.Right:
                    CreateBomb(new Tuple<int, int, Map>(CurrentPos[1] + 1, CurrentPos[0], map));
                    break;
                case Direction.Up:
                    CreateBomb(new Tuple<int, int, Map>(CurrentPos[1], CurrentPos[0] - 1, map));
                    break;
                case Direction.Down:
                    CreateBomb(new Tuple<int, int, Map>(CurrentPos[1], CurrentPos[0] + 1, map));
                    break;
            }
        }
        private void CreateBomb(object obj)
        {
            Tuple<int, int, Map> tripple = (Tuple<int, int, Map>) obj;
            if (tripple.Item3[tripple.Item2, tripple.Item1] != PlayerDirection)
                return;

            

            if (BombPower == 1)
                new Bomb(tripple);
            else if (BombPower == 2)
                new Bomb2(tripple);
            else if (BombPower == 3)
                new Bomb3(tripple);
            else if (BombPower == 4)
                new Bomb4(tripple);
            else if (BombPower == 5)
                new Bomb5(tripple);
            else if (BombPower == 6)
                new Bomb6(tripple);
            else if (BombPower == 7)
                new Bomb7(tripple);
            else if (BombPower == 8)
                new Bomb8(tripple);
            else if (BombPower == 9)
                new Bomb9(tripple);
            else if (BombPower == 10)
                new Bomb10(tripple);
            else 
                Panel.Debug("Wrong bomb level");
           
        }
        public void MoveLeft(Map map)
        {
            if (CurrentPos[1] <= 1)
                return;
            if (map[CurrentPos[0], CurrentPos[1] - 1] == '0' || map[CurrentPos[0], CurrentPos[1] - 1] == Bomb.BombTexture)
                return;

            if (map[CurrentPos[0], CurrentPos[1] - 1] == Bonus.BombBonus)
            {
                if (BombPower < 10)
                {
                    BombPower++;
                    Panel.Render();
                }
                
            }
            else if (map[CurrentPos[0], CurrentPos[1] - 1] == Bonus.LifeBonus)
            {
                if (Lives < 10)
                {
                    Lives++;
                    Panel.Render();
                }
               
            }
            if (map[CurrentPos[0], CurrentPos[1] - 1] == Enemy.EnemyTexture)
            {
                Lives--;
                Panel.Render();
            }
                
            Render(map, CurrentPos[1] - 1, CurrentPos[0], PlayerTexture);
            Render(map, CurrentPos[1], CurrentPos[0], ' ');
            CurrentPos[1]--;
            RenderDirection(map);
        }
        public void MoveRight(Map map)
        {
            if (CurrentPos[1] >= WindowSettings.GameWindowWidth - 2)
                return;
            if (map[CurrentPos[0], CurrentPos[1] + 1] == '0' || map[CurrentPos[0], CurrentPos[1] + 1] == Bomb.BombTexture)
                return;
            if (map[CurrentPos[0], CurrentPos[1] + 1] == Bonus.BombBonus)
            {
                if (BombPower < 10)
                {
                    BombPower++;
                    Panel.Render();
                }
            }
            else if (map[CurrentPos[0], CurrentPos[1] + 1] == Bonus.LifeBonus)
            {
                if (Lives < 10)
                {
                    Lives++;
                    Panel.Render();
                }
            }
            if (map[CurrentPos[0], CurrentPos[1] + 1] == Enemy.EnemyTexture)
            {
                Lives--;
                Panel.Render();
            }

            Render(map, CurrentPos[1] + 1, CurrentPos[0], PlayerTexture);
            Render(map, CurrentPos[1], CurrentPos[0], ' ');
            CurrentPos[1]++;
            RenderDirection(map);
        }
        public void MoveUp(Map map)
        {
            if (CurrentPos[0] <= 1)
                return;
            if (map[CurrentPos[0] - 1, CurrentPos[1]] == '0' || map[CurrentPos[0] - 1, CurrentPos[1]] == Bomb.BombTexture)
                return;
            if (map[CurrentPos[0] - 1, CurrentPos[1]] == Bonus.BombBonus)
            {
                if (BombPower < 10)
                {
                    BombPower++;
                    Panel.Render();
                }
            }
            else if (map[CurrentPos[0] - 1, CurrentPos[1]] == Bonus.LifeBonus)
            {
                if (Lives < 10)
                {
                    Lives++;
                    Panel.Render();
                }
            }
            if (map[CurrentPos[0] - 1, CurrentPos[1]] == Enemy.EnemyTexture)
            {
                Lives--;
                Panel.Render();
            }

            Render(map, CurrentPos[1], CurrentPos[0] - 1, PlayerTexture);
            Render(map, CurrentPos[1], CurrentPos[0], ' ');
            CurrentPos[0]--;
            RenderDirection(map);
        }
        public void MoveDown(Map map) 
        {
            if (CurrentPos[0] >= WindowSettings.GameWindowHeight - 2)
                return;
            if (map[CurrentPos[0] + 1, CurrentPos[1]] == '0' || map[CurrentPos[0] + 1, CurrentPos[1]] == Bomb.BombTexture)
                return;
            if (map[CurrentPos[0] + 1, CurrentPos[1]] == Bonus.BombBonus)
            {
                if (BombPower < 10)
                {
                    BombPower++;
                    Panel.Render();
                }
            }
            else if (map[CurrentPos[0] + 1, CurrentPos[1]] == Bonus.LifeBonus)
            {
                if (Lives < 10)
                {
                    Lives++;
                    Panel.Render();
                }
            }
            if (map[CurrentPos[0] + 1, CurrentPos[1]] == Enemy.EnemyTexture)
            {
                Lives--;
                Panel.Render();
            }

            Render(map, CurrentPos[1], CurrentPos[0] + 1, PlayerTexture);
            Render(map, CurrentPos[1], CurrentPos[0], ' ');
            CurrentPos[0]++;
            RenderDirection(map);
        }
        public void RenderDirection(Map map){
            int x = CurrentPos[1];
            int y = CurrentPos[0];
            
            switch (direction)
            {
                case Direction.Left:
                    if (map[y, x - 1] == '|' || map[y, x - 1] == '=')
                        return;
                    ClearDirectionTrace(map, x - 1, y);
                    if (map[y, x - 1] != '0' && map[y, x - 1] != Bomb.BombTexture && map[y, x - 1] != Enemy.EnemyTexture
                        && map[y, x - 1] != Bonus.BombBonus && map[y, x - 1] != Bonus.LifeBonus)
                        Render(map, x - 1, y, PlayerDirection);
                    break;
                case Direction.Right:
                    if (map[y, x + 1] == '|' || map[y, x + 1] == '=')
                        return;
                    ClearDirectionTrace(map, x + 1, y);
                    if (map[y, x + 1] != '0' && map[y, x + 1] != Bomb.BombTexture && map[y, x + 1] != Enemy.EnemyTexture
                        && map[y, x + 1] != Bonus.BombBonus && map[y, x + 1] != Bonus.LifeBonus)
                        Render(map, x + 1, y, PlayerDirection);
                    break;
                case Direction.Up:
                    if (map[y - 1, x] == '|' || map[y - 1, x] == '=')
                        return;
                    ClearDirectionTrace(map, x, y - 1);
                    if (map[y - 1, x] != '0' && map[y - 1, x] != Bomb.BombTexture && map[y - 1, x] != Enemy.EnemyTexture
                        && map[y - 1, x] != Bonus.BombBonus && map[y - 1, x] != Bonus.LifeBonus)
                        Render(map, x, y - 1, PlayerDirection);
                    break;
                case Direction.Down:
                    if (map[y + 1, x] == '|' || map[y + 1, x] == '=')
                        return;
                    ClearDirectionTrace(map, x, y + 1);
                    if (map[y + 1, x] != '0' && map[y + 1, x] != Bomb.BombTexture && map[y + 1, x] != Enemy.EnemyTexture
                        && map[y + 1, x] != Bonus.BombBonus && map[y + 1, x] != Bonus.LifeBonus)
                        Render(map, x, y + 1, PlayerDirection);
                        
                    break;
            }
        }
        void ClearDirectionTrace(Map map, int x, int y)
        {
            if (map[y + 1, x] == PlayerDirection)
            {
                Render(map, x, y + 1, ' ');
            }
            else if (map[y - 1, x] == PlayerDirection)
            {
                Render(map, x, y - 1, ' ');
            }
            else if (map[y, x - 1] == PlayerDirection)
            {
                Render(map, x - 1, y, ' ');
            }
            else if (map[y, x + 1] == PlayerDirection)
            {
                Render(map, x + 1, y, ' ');
            }
        }
        public void Render(Map map, int x, int y, char image)
        {

            map[y, x] = image;
            Console.SetCursorPosition(x, y);
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Write(image);
        }
        #endregion
    }
}
