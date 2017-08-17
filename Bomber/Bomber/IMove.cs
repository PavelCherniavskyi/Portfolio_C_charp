using System;


namespace Bomber
{
    enum Direction { Left, Right, Up, Down };

    interface IMove
    {
        void MoveLeft(Map map);
        void MoveRight(Map map);
        void MoveUp(Map map);
        void MoveDown(Map map);
    }
}
