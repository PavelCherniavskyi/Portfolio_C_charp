using System;


namespace Bomber
{
    class WindowSettings
    {
        static int _windowHeight = 45;
        static int _windowWidth = 70;
        static int _gameWindHeight = 40;
        static int _gameWindowWidth = _windowWidth;
        static string gameName = "Bomber";
        static ConsoleColor _backColor = ConsoleColor.DarkGray;
        public static int GameWindowHeight
        {
            get
            {
                return _gameWindHeight;
            }
        }
        public static int GameWindowWidth
        {
            get
            {
                return _gameWindowWidth;
            }
        }
        public static int WindowHeight
        {
            get
            {
                return _windowHeight;
            }
        }
        public static int WindowWidth
        {
            get
            {
                return _windowWidth;
            }
        }

        static WindowSettings()
        {
            Console.WindowHeight = _windowHeight;
            Console.WindowWidth = _windowWidth;
            Console.Title = gameName;
            Console.SetBufferSize(_windowWidth, _windowHeight);
            Console.BackgroundColor = _backColor;
        }

        
    }
}
