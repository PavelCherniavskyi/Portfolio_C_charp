using System;


namespace Bomber
{
    abstract class Map
    {
        #region Variables
        protected char[,] Field;
        #endregion
        #region Properties and Indexers
        public char this[int i, int j]
        {
            get
            {
                return Field[i, j];
            }
            set
            {
                Field[i, j] = value;
            }
        }
        public virtual int Enemies { set; get; }
        #endregion
        #region Methods
        public void Draw()
        {
            for (int i = 0; i < WindowSettings.GameWindowHeight; i++)
            {
                for (int j = 0; j < WindowSettings.GameWindowWidth; j++)
                {
                    if (Field[i, j] == '=' || Field[i, j] == '|')
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write(Field[i, j]);
                    }
                    else if (Field[i, j] == '0')
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        ConsoleColor col = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(Field[i, j]);
                        Console.ForegroundColor = col;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.Write(Field[i, j]);
                    }

                }

            }
        }
        #endregion



    }

    class Level_1 : Map
    {

        #region Variables
        int enemies = 10;
        string[] tempField = new string[]
           {"======================================================================",
            "|                                                                    |",
            "|   0                                                                |",
            "|                  000000                               00           |",
            "|000               000000            0000000            00           |",
            "|                                    0000000 00                      |",
            "|                           0                                        |",
            "|           0                                                      00|",
            "|                           0               0000                   00|",
            "|                                                                    |",
            "|                00                              000                 |",
            "|                                 000000000000000                    |",
            "|                                                                    |",
            "|                   000                           000000             |",
            "|00                 000           0               000000             |",
            "|                   000                                              |",
            "|                   000                     00                       |",
            "|           00                                             00        |",
            "|           00               00000                                   |",
            "|                                               00                   |",
            "|                                                                    |",
            "|                          000                       0000000         |",
            "|        000000                                                      |",
            "|           000               0000000                                |",
            "|                             0000000                                |",
            "|                                             000                    |",
            "|                                                      000000        |",
            "|               00       0000                                        |",
            "|                                                                   0|",
            "|                                                                    |",
            "|            00000         000           00000                       |",
            "|                                        00000                       |",
            "|                                                                    |",
            "|                               00000000000000000                    |",
            "|                                                      00            |",
            "|                000                                                 |",
            "|                000                                                 |",
            "|                                                                    |",
            "|0                                             0                     |",
            "======================================================================"};
        #endregion
        #region Constructors
        public Level_1()
        {
            Field = new char[40, 70];
            for (int i = 0; i < 40; i++)
            {
                for (int j = 0; j < 70; j++)
                {
                    Field[i, j] = tempField[i][j];
                }
            }
        }
        #endregion
        #region Properties
        public override int Enemies
        {
            get { return enemies; }
        }
        #endregion
    }
    class Level_2 : Map
    {

        #region Variables
        int enemies = 20;
        string[] tempField = new string[]
           {"======================================================================",
            "|            00                        00000                         |",
            "|                                                                    |",
            "|                00000000000                                         |",
            "|                                                 00000              |",
            "|                                                                    |",
            "|               000000                                               |",
            "|               000000                                               |",
            "|                                       00000000                  000|",
            "|0000                                                                |",
            "|                                                                    |",
            "|                              0000                     00000        |",
            "|          0000000                            00                 0000|",
            "|                                                                    |",
            "|                                                                    |",
            "|                                           000000                   |",
            "|            000                             000000                  |",
            "|            000      0000                                           |",
            "|                                                                    |",
            "|                                 000000000                          |",
            "|                                      0000                          |",
            "|             000                        00                          |",
            "|                                                                    |",
            "|                                  0000                              |",
            "|00         00                                          00000        |",
            "|00                                                                  |",
            "|                             0000                                   |",
            "|                            00000                                   |",
            "|                                                                    |",
            "|                                                                    |",
            "|                  000000                      000                   |",
            "|                  000000                                 00         |",
            "|                                                                    |",
            "|                                        00000                       |",
            "|                                        00000                       |",
            "|                    000                                             |",
            "|                   0000                              00             |",
            "|                                                  0000              |",
            "|                            000                                     |",
            "======================================================================"};
        #endregion
        #region Constructors
        public Level_2()
        {
            Field = new char[40, 70];
            for (int i = 0; i < 40; i++)
            {
                for (int j = 0; j < 70; j++)
                {
                    Field[i, j] = tempField[i][j];
                }
            }
        }
        #endregion
        #region Properties
        public override int Enemies
        {
            get { return enemies; }
        }
        #endregion
    }
    class Level_3 : Map
    {

        #region Variables
        int enemies = 30;
        string[] tempField = new string[]
           {"======================================================================",
            "|                                                0000                |",
            "|   0                                                                |",
            "|                  000000                                            |",
            "|                  000000                                            |",
            "|                                            00            000       |",
            "|                           0                             000        |",
            "|           0                          00000                0        |",
            "|                           0                               00       |",
            "|                                                          00        |",
            "|                00                              000                 |",
            "|                           00000                                    |",
            "|  0000                     000                00                    |",
            "|                                              0000                  |",
            "|                 000             0            000                   |",
            "|                000                                                 |",
            "|                                                                    |",
            "|           00                                             00        |",
            "|           00             000000000000                            00|",
            "|                                               00                 00|",
            "|                                                                    |",
            "|                          000                         0000          |",
            "|                                                                    |",
            "|               0000000              0000000000                      |",
            "|                  000                                               |",
            "|                                             000           0        |",
            "|                               00000                       0        |",
            "|               00                00                        0        |",
            "|                              00000000000000               0        |",
            "|                                       000                 0        |",
            "|            00000                                          0        |",
            "|                                                                    |",
            "|                                                                    |",
            "|                               00000000000000000                    |",
            "|           0000000                                                  |",
            "|              00000         00000                                   |",
            "|                                              0000000               |",
            "|                                                0000                |",
            "|0                                                                   |",
            "======================================================================"};
        #endregion
        #region Constructors
        public Level_3()
        {
            Field = new char[40, 70];
            for (int i = 0; i < 40; i++)
            {
                for (int j = 0; j < 70; j++)
                {
                    Field[i, j] = tempField[i][j];
                }
            }
        }
        #endregion
        #region Properties
        public override int Enemies
        {
            get { return enemies; }
        }
        #endregion
    }
}
