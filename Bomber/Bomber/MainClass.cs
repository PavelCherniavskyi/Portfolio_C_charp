using System;


namespace Bomber
{
    class MainClass
    {
        static public void Main()
        {
            try
            {
                Game game = new Game();
                game.run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
