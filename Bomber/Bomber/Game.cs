using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Bomber
{
    class Game
    {

        #region Variables
        bool isGameOver;
        Panel panel = new Panel();
        Menu menu = new Menu();
        Player player = new Player();
        Stopwatch timer = Stopwatch.StartNew();
        TimeSpan timePerFrame = TimeSpan.FromMilliseconds(16);
        Map map = null;
        EnemyController enemyController;
        static int curLvl = 1;
        #endregion
        #region Constructors
        public Game()
        {
        }
        #endregion
        #region Properties
        static public int CurLevel
        {
            get { return curLvl; }
        }
        #endregion
        #region Methods
        void LevelSetUp(int level)
        {
            LoadMap(level);
            map[player.CurrentPos[0], player.CurrentPos[1]] = Player.PlayerTexture;
            player.RenderDirection(map);
            enemyController = new EnemyController(map, panel);
            enemyController.AddEnemy(map.Enemies);
            Render();
            Bomb.ClearAllBombs();
        }
        void LoadMap(int lvl)
        {
            switch (lvl)
            {
                case 1:
                    map = new Level_1();
                    break;
                case 2:
                    map = new Level_2();
                    break;
                case 3:
                    map = new Level_3();
                    break;
            }
        }

        public void run()
        {
            SwitchMenuChoice(Menu.ShowStartMenu());
            LevelSetUp(curLvl);

            while (true)
            {
                if (timer.Elapsed > timePerFrame)
                {
                    if (!isGameOver)
                    {
                        InputHandler();
                        Update();
                        timer.Restart();
                    }
                }
            }
        }

        void InputHandler()
        {

            if (Console.KeyAvailable)
            {
                IMove imove = player;
                IBomber bomber = player;
                ConsoleKey button = Console.ReadKey(true).Key;
                if (button == ConsoleKey.Escape)
                {
                    SwitchMenuChoice(Menu.ShowStartMenu());
                    Render();
                }
                else if (button == ConsoleKey.LeftArrow)
                {
                    imove.MoveLeft(map);
                }
                else if (button == ConsoleKey.RightArrow)
                {
                    imove.MoveRight(map);
                }
                else if (button == ConsoleKey.UpArrow)
                {
                    imove.MoveUp(map);
                }
                else if (button == ConsoleKey.DownArrow)
                {
                    imove.MoveDown(map);
                }
                else  if (button == ConsoleKey.Spacebar)
                {
                    bomber.PlantBomb(map);
                }
                else if (button == ConsoleKey.W)
                {
                    player.ChangeDirection(Direction.Up, map);
                }
                else if (button == ConsoleKey.A)
                {
                    player.ChangeDirection(Direction.Left, map);
                }
                else if (button == ConsoleKey.S)
                {
                    player.ChangeDirection(Direction.Down, map);
                }
                else if (button == ConsoleKey.D)
                {
                    player.ChangeDirection(Direction.Right, map);
                }

            }
        }
        void SwitchMenuChoice(int menuChoice)
        {
            if (menuChoice == 3)
                Exit();
            else if (menuChoice == 2)
                Load();
            else if (menuChoice == 1)
                Save();
        }
        void Update()
        {
            enemyController.Update();
            Bomb.Update();
            Bonus.Update(map);
            isWin();
            isNextLevel();
        }

        void isWin()
        {
            if (Player.Lives < 0)
            {
                Panel.Debug("Game Over...");
                isGameOver = true;
            }
        }

        void isNextLevel()
        {
            if (EnemyController.Count == 0)
            {
                if (curLvl == 3)
                    Panel.Debug("You win!!!");
                else
                    LevelSetUp(++curLvl);
            }
        }

        void RenderMap()
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Clear();
            map.Draw();
        }

        void Render()
        {
            RenderMap();
            Panel.Render();
        }

        void Save()
        {
            using (StreamWriter sw = new StreamWriter("Save.log", false))
            {
                sw.WriteLine(Player.BombPower);
                sw.WriteLine(Player.Lives);
                sw.WriteLine(CurLevel);
                sw.Close();
            }
        }

        void Load()
        {
            using (StreamReader sr = new StreamReader("Save.log", Encoding.UTF8))
            {
                Player.BombPower = Int32.Parse(sr.ReadLine());
                Player.Lives = Int32.Parse(sr.ReadLine());
                curLvl = Int32.Parse(sr.ReadLine());
                sr.Close();

            }
        }
        void Exit()
        {
            Environment.Exit(0);
        }
        #endregion


    }
}
