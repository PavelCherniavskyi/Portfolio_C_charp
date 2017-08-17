using System;
using System.Collections.Generic;


namespace Bomber
{
    class Panel
    {
        #region Variables
        static int lvl = Game.CurLevel;
        static int lives = Player.Lives;
        static int enemyCount = EnemyController.Count;
        static int bombPower = Player.BombPower;
        #endregion
        #region Constructors
        public Panel()
        {
            Render();
        }
        #endregion
        #region Properties
  
        #endregion
        #region Methods

        public static void Render()
        {
            bombPower = Player.BombPower;
            enemyCount = EnemyController.Count;
            lives = Player.Lives;
            lvl = Game.CurLevel;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(0, WindowSettings.GameWindowHeight + 1);
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Write("Level: " + lvl); Console.WriteLine("          Enemy left: " + enemyCount + " ");
            Console.Write("Lives: " + lives); Console.WriteLine("          Bomb power: " + bombPower);
        }
        public static void Debug(String str)
        {
            ConsoleColor colBack = Console.BackgroundColor;
            Console.SetCursorPosition(50, WindowSettings.GameWindowHeight + 1);
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine(str);
            Console.BackgroundColor = colBack;
        }
        #endregion


    }
}
