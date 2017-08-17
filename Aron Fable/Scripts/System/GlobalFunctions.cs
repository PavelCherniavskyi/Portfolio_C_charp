using UnityEngine;
using System.Collections;

public class GlobalFunctions : MonoBehaviour {

    // Точка с полярным смещением: Нужна для создания точки "рядом" с другой точкой.
    public static Vector3 offset_point(Vector3 pos, float angle, float distance)
    {
        return new Vector3(pos.x + Mathf.Sin(angle) * distance,
            pos.y + Mathf.Cos(angle) * distance, pos.z);
    }

    // Точка с полярным смещением: Нужна для создания точки по отношению к дургой точке.
    public static Vector3 offset_point(Vector3 position, Vector3 target, float distance)
    {
        return position + (target - position).normalized * distance;
    }
}

