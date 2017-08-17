using System;
using System.Diagnostics;

namespace Bomber
{
    class Enemy : IMove
    {
        #region Variables
        Direction direction;
        static Random rd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
        double speed = 0.4;
        double switchTime;
        Stopwatch switchTimer = Stopwatch.StartNew();
        Stopwatch moveTimer = Stopwatch.StartNew();
        Map map;
        EnemyController enemyController;
        int[] _currentPos = new int[2];
        static char _enemyTexture = 'E';
        #endregion
        #region Constructors
        public Enemy(Map map, int[] currentPos, EnemyController enemyController)
        {
            this.enemyController = enemyController;
            switchTime = rd.Next(2, 6);
            this.map = map;
            _currentPos = currentPos;
            direction = (Direction)rd.Next(0, 4);
            Bomb.ExplosionEvent += GotHit;
            switchTimer.Start();
            moveTimer.Start();
        }
        #endregion
        #region Properties
        public static char EnemyTexture
        {
            get
            {
                return _enemyTexture;
            }
        }
        #endregion
        #region Methods
        public void MoveTowardsDirection()
        {
            if (moveTimer.Elapsed < TimeSpan.FromSeconds(speed))
                return;
            else
                moveTimer.Restart();
            if(switchTimer.Elapsed > TimeSpan.FromSeconds(switchTime))
            {

                switchTimer.Restart();
                direction = (Direction)rd.Next(0, 4);
            }
            switch (direction)
            {
                case Direction.Left:
                    MoveLeft(map);
                    break;
                case Direction.Right:
                    MoveRight(map);
                    break;
                case Direction.Up:
                    MoveUp(map);
                    break;
                case Direction.Down:
                    MoveDown(map);
                    break;
            }
        }
        void ChangeDirection()
        {
            switchTimer.Restart();
            direction = (Direction)rd.Next(0, 3);
        }

        void ChangeDirectionDeadEnd()
        {
            switchTimer.Restart();
            Direction dir = direction;
            while (dir == direction)
            {
                direction = (Direction)rd.Next(0, 3);
            }
            
        }
        void GotHit(int x, int y)
        {

            if (_currentPos[0] != y || _currentPos[1] != x)
                return;
            enemyController.RemoveEnemy(this);

        }
        public void MoveLeft(Map map)
        {
            if (_currentPos[1] <= 1)
                ChangeDirection();
            if (map[_currentPos[0], _currentPos[1] - 1] == '0' || map[_currentPos[0], _currentPos[1] - 1] == Bomb.BombTexture || map[_currentPos[0], _currentPos[1] - 1] == _enemyTexture)
                ChangeDirectionDeadEnd();
            if (map[_currentPos[0], _currentPos[1] - 1] == Player.PlayerTexture)
            {
                Player.Lives--;
                Panel.Render();
            }
            if (map[_currentPos[0], _currentPos[1] - 1] == ' ')
            {
                Render(map, _currentPos[1] - 1, _currentPos[0], _enemyTexture);
                Render(map, _currentPos[1], _currentPos[0], ' ');
                _currentPos[1]--;
            }
        }
        public void MoveRight(Map map)
        {
            if (_currentPos[1] >= WindowSettings.WindowWidth - 1)
                ChangeDirection();
            if (map[_currentPos[0], _currentPos[1] + 1] == '0' || map[_currentPos[0], _currentPos[1] + 1] == Bomb.BombTexture || map[_currentPos[0], _currentPos[1] + 1] == _enemyTexture)
                ChangeDirectionDeadEnd();
            if (map[_currentPos[0], _currentPos[1] + 1] == Player.PlayerTexture)
            {
                Player.Lives--;
                Panel.Render();
            }
            if (map[_currentPos[0], _currentPos[1] + 1] == ' ')
            {
                Render(map, _currentPos[1] + 1, _currentPos[0], _enemyTexture);
                Render(map, _currentPos[1], _currentPos[0], ' ');
                _currentPos[1]++;
            }
        }
        public void MoveUp(Map map)
        {
            if (_currentPos[0] <= 1)
                ChangeDirection();
            if (map[_currentPos[0] - 1, _currentPos[1]] == '0' || map[_currentPos[0] - 1, _currentPos[1]] == Bomb.BombTexture || map[_currentPos[0] - 1, _currentPos[1]] == _enemyTexture)
                ChangeDirectionDeadEnd();
            if (map[_currentPos[0] - 1, _currentPos[1]] == Player.PlayerTexture)
            {
                Player.Lives--;
                Panel.Render();
            }
            if (map[_currentPos[0] - 1, _currentPos[1]] == ' ')
            {
                Render(map, _currentPos[1], _currentPos[0] - 1, _enemyTexture);
                Render(map, _currentPos[1], _currentPos[0], ' ');
                _currentPos[0]--;
            }
        }
        public void MoveDown(Map map)
        {
            if (_currentPos[0] >= WindowSettings.WindowHeight - 1)
                ChangeDirection();
            if (map[_currentPos[0] + 1, _currentPos[1]] == '0' || map[_currentPos[0] + 1, _currentPos[1]] == Bomb.BombTexture || map[_currentPos[0] + 1, _currentPos[1]] == _enemyTexture)
                ChangeDirectionDeadEnd();
            if (map[_currentPos[0] + 1, _currentPos[1]] == Player.PlayerTexture)
            {
                Player.Lives--;
                Panel.Render();
            }
            if (map[_currentPos[0] + 1, _currentPos[1]] == ' ')
            {
                Render(map, _currentPos[1], _currentPos[0] + 1, _enemyTexture);
                Render(map, _currentPos[1], _currentPos[0], ' ');
                _currentPos[0]++;
            }
        }
        public void Render(Map map, int x, int y, char image)
        {

            map[y, x] = image;
            ConsoleColor forCol = Console.ForegroundColor;
            ConsoleColor backCol = Console.BackgroundColor;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(x, y);
            Console.Write(image);
            Console.ForegroundColor = forCol;
            Console.BackgroundColor = backCol;
            
        }
        #endregion


    }
}
