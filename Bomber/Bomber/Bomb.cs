using System;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;

namespace Bomber
{
    delegate void ExplosionDel(int x, int y);
    class Bomb
    {
        #region Variables
        public static event ExplosionDel ExplosionEvent;
        protected int ExplosionSize = Player.BombPower;
        private static readonly List<Bomb> BombArray = new List<Bomb>();
        private readonly DateTime _startTime = DateTime.Now;
        private readonly TimeSpan _livingTime = TimeSpan.FromSeconds(3);
        private DateTime _endTime;
        private readonly int _posX;
        private readonly int _posY;
        private bool _isExplosionDone;
        private const char Image = '*';
        private readonly Map _map;
        
        #endregion
        #region Properties
        public static char BombTexture => Image;

        #endregion
        #region Constructors
        public Bomb(Tuple<int, int, Map> tripple)
        {
            _map = tripple.Item3;
            _posX = tripple.Item1;
            _posY = tripple.Item2;
            _endTime = _startTime + _livingTime;
            ExplosionEvent += GotHit;
            _map[_posY, _posX] = Image;
            ConsoleColor forCol = Console.ForegroundColor;
            ConsoleColor backCol = Console.BackgroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(_posX, _posY);
            Console.Write(Image);
            Console.ForegroundColor = forCol;
            Console.BackgroundColor = backCol;
            BombArray.Add(this);
        }
        #endregion
        #region Methods

        public static void RemoveBomb(Bomb bomb)
        {
            BombArray.Remove(bomb);
        }
        public static void ClearAllBombs()
        {
            BombArray.Clear();
        }
        public static void Update()
        {
            foreach (var a in BombArray)
            {
                var clearTime = a._endTime + TimeSpan.FromMilliseconds(200);
                if (DateTime.Now > a._endTime && !a._isExplosionDone)
                {
                    a.ExplodeOut();
                }

                if (DateTime.Now > clearTime && a._isExplosionDone)
                {
                    a.ExplodeClear();
                    break;
                }
            }
        }
        void DrawAndAssingSymbol(int x, int y, char c)
        {
            _map[y, x] = c;
            Console.SetCursorPosition(x, y);
            Console.Write(c);
        }
        void ExplodeClear()
        {
            DrawAndAssingSymbol(_posX, _posY, ' ');

            for (int i = 1; i < ExplosionSize + 1; i++)
            {
                if (_map[_posY, _posX + i] == '=' || _map[_posY, _posX + i] == '|')
                {
                    break;
                }
                if (_map[_posY, _posX + i] == '0' || _map[_posY, _posX + i] == Player.PlayerTexture)
                {
                    break;
                }
                DrawAndAssingSymbol(_posX + i, _posY, ' ');
            }
            for (int i = 1; i < ExplosionSize + 1; i++)
            {
                if (_map[_posY, _posX - i] == '=' || _map[_posY, _posX - i] == '|')
                {
                    break;
                }
                if (_map[_posY, _posX - i] == '0' || _map[_posY, _posX - i] == Player.PlayerTexture)
                {
                    break;
                }
                DrawAndAssingSymbol(_posX - i, _posY, ' ');
            }
            for (int i = 1; i < ExplosionSize + 1; i++)
            {
                if (_map[_posY + i, _posX] == '=' || _map[_posY + i, _posX] == '|')
                {
                    break;
                }
                if (_map[_posY + i, _posX] == '0' || _map[_posY + i, _posX] == Player.PlayerTexture)
                {
                    break;
                }
                DrawAndAssingSymbol(_posX, _posY + i, ' ');
            }
            for (int i = 1; i < ExplosionSize + 1; i++)
            {
                if (_map[_posY - i, _posX] == '=' || _map[_posY - i, _posX] == '|')
                {
                    break;
                }
                if (_map[_posY - i, _posX] == '0' || _map[_posY - i, _posX] == Player.PlayerTexture)
                {
                    break;
                }
                DrawAndAssingSymbol(_posX, _posY - i, ' ');
            }
            RemoveBomb(this);
        }
        void ExplodeOut()
        {
            ExplodeColorChange(true);
            DrawAndAssingSymbol(_posX, _posY, '+');

            for (int i = 1; i < ExplosionSize + 1; i++)
            {
                if (_map[_posY, _posX + i] == '=' || _map[_posY, _posX + i] == '|')
                {
                    break;
                }
                if (_map[_posY, _posX + i] == '0')
                {
                    DrawAndAssingSymbol(_posX + i, _posY, '+');
                    break;
                }
                if (_map[_posY, _posX + i] == Player.PlayerTexture || _map[_posY, _posX + i] == Enemy.EnemyTexture || _map[_posY, _posX + i] == Bomb.Image)
                {
                    ExplosionEvent(_posX + i, _posY);
                    break;
                }
                ExplodeColorChange(true);
                DrawAndAssingSymbol(_posX + i, _posY, '+');
            }
            for (int i = 1; i < ExplosionSize + 1; i++)
            {
                if (_map[_posY, _posX - i] == '=' || _map[_posY, _posX - i] == '|')
                {
                    break;
                }
                if (_map[_posY, _posX - i] == '0')
                {
                    DrawAndAssingSymbol(_posX - i, _posY, '+');
                    break;
                }
                if (_map[_posY, _posX - i] == Bomb.Image || _map[_posY, _posX - i] == Player.PlayerTexture || _map[_posY, _posX - i] == Enemy.EnemyTexture)
                {
                    ExplosionEvent(_posX - i, _posY);
                    break;
                }
                ExplodeColorChange(true);
                DrawAndAssingSymbol(_posX - i, _posY, '+');
            }
            for (int i = 1; i < ExplosionSize + 1; i++)
            {
                if (_map[_posY + i, _posX] == '=' || _map[_posY + i, _posX] == '|')
                {
                    break;
                }
                if (_map[_posY + i, _posX] == '0')
                {
                    DrawAndAssingSymbol(_posX, _posY + i, '+');
                    break;
                }
                if (_map[_posY + i, _posX] == Bomb.Image || _map[_posY + i, _posX] == Player.PlayerTexture || _map[_posY + i, _posX] == Enemy.EnemyTexture)
                {
                    ExplosionEvent(_posX, _posY + i);
                    break;
                }
                ExplodeColorChange(true);
                DrawAndAssingSymbol(_posX, _posY + i, '+');
            }
            for (int i = 1; i < ExplosionSize + 1; i++)
            {
                if (_map[_posY - i, _posX] == '=' || _map[_posY - i, _posX] == '|')
                {
                    break;
                }
                if (_map[_posY - i, _posX] == '0')
                {
                    DrawAndAssingSymbol(_posX, _posY - i, '+');
                    break;
                }
                if (_map[_posY - i, _posX] == Bomb.Image || _map[_posY - i, _posX] == Player.PlayerTexture || _map[_posY - i, _posX] == Enemy.EnemyTexture)
                {
                    ExplosionEvent(_posX, _posY - i);
                    break;
                }
                ExplodeColorChange(true);
                DrawAndAssingSymbol(_posX, _posY - i, '+');
            }

            ExplodeColorChange(false);
            _isExplosionDone = true;
            _endTime = DateTime.Now;
        }
        void GotHit(int x, int y)
        {

            if (_posY != y || _posX != x)
                return;
            ExplodeOut();
        }
        void ExplodeColorChange(bool on)
        {
            if (on)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            
        }
        #endregion

    }
    class Bomb2 : Bomb
    {
        #region Constructors
        public Bomb2(Tuple<int, int, Map> tripple) 
            : base(tripple)
        {
            ExplosionSize = 2;
        }
        #endregion
    }
    class Bomb3 : Bomb
    {
        #region Constructors
        public Bomb3(Tuple<int, int, Map> tripple)
            : base(tripple)
        {
            ExplosionSize = 3;
        }
        #endregion
    }
    class Bomb4 : Bomb
    {
        #region Constructors
        public Bomb4(Tuple<int, int, Map> tripple)
            : base(tripple)
        {
            ExplosionSize = 4;
        }
        #endregion
    }
    class Bomb5 : Bomb
    {
        #region Constructors
        public Bomb5(Tuple<int, int, Map> tripple)
            : base(tripple)
        {
            ExplosionSize = 5;
        }
        #endregion
    }
    class Bomb6 : Bomb
    {
        #region Constructors
        public Bomb6(Tuple<int, int, Map> tripple)
            : base(tripple)
        {
            ExplosionSize = 6;
        }
        #endregion
    }
    class Bomb7 : Bomb
    {
        #region Constructors
        public Bomb7(Tuple<int, int, Map> tripple)
            : base(tripple)
        {
            ExplosionSize = 7;
        }
        #endregion
    }
    class Bomb8 : Bomb
    {
        #region Constructors
        public Bomb8(Tuple<int, int, Map> tripple)
            : base(tripple)
        {
            ExplosionSize = 8;
        }
        #endregion
    }
    class Bomb9 : Bomb
    {
        #region Constructors
        public Bomb9(Tuple<int, int, Map> tripple)
            : base(tripple)
        {
            ExplosionSize = 9;
        }
        #endregion
    }
    class Bomb10 : Bomb
    {
        #region Constructors
        public Bomb10(Tuple<int, int, Map> tripple)
            : base(tripple)
        {
            ExplosionSize = 10;
        }
        #endregion
    }
}
