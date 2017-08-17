using System;


namespace Bomber
{
    class Menu
    {
        #region Variables
        GameSettings gameSettings = new GameSettings();
        #endregion
        #region Methods
        static public int ShowStartMenu()
        {
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Clear();
            int result = 0;
            MenuPaint(result);
            ConsoleKeyInfo keyinfo = Console.ReadKey(true);

            while (keyinfo.Key != ConsoleKey.Enter)
            {
                if (keyinfo.Key == ConsoleKey.UpArrow)
                {
                    if (result > 0)
                        result--;
                }
                if (keyinfo.Key == ConsoleKey.DownArrow)
                {
                    if (result < 3)
                        result++;
                }
                if (keyinfo.Key == ConsoleKey.Escape)
                {
                    result = 0;
                    break;
                }

                MenuPaint(result);
                keyinfo = Console.ReadKey(true);
            }

            return result;

        }
        static void MenuPaint(int line)
        {
            bool[] lines = new bool[4];
            lines[line] = true;
            ConsoleColor startColor = Console.BackgroundColor;
            Console.SetCursorPosition(21, 15);
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.WriteLine("==============================");
            Console.SetCursorPosition(21, 16);
            if (lines[0] == true)
            {
                Console.BackgroundColor = ConsoleColor.Green;
            }
            else
                Console.BackgroundColor = ConsoleColor.Cyan;
            Console.WriteLine("**         Play             **");
            Console.SetCursorPosition(21, 17);
            if (lines[1] == true)
            {
                Console.BackgroundColor = ConsoleColor.Green;
            }
            else
                Console.BackgroundColor = ConsoleColor.Cyan;
            Console.WriteLine("**          Save            **");
            Console.SetCursorPosition(21, 18);
            if (lines[2] == true)
            {
                Console.BackgroundColor = ConsoleColor.Green;
            }
            else
                Console.BackgroundColor = ConsoleColor.Cyan;
            Console.WriteLine("**           Load           **");
            Console.SetCursorPosition(21, 19);
            if (lines[3] == true)
            {
                Console.BackgroundColor = ConsoleColor.Green;
            }
            else
                Console.BackgroundColor = ConsoleColor.Cyan;
            Console.WriteLine("**            Exit          **");
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(21, 20);
            Console.WriteLine("==============================");
            Console.BackgroundColor = startColor;
        }
        #endregion

    }
}
