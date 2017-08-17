using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coursor : MonoBehaviour
{

    public Texture2D CoursorIdle;
    public Texture2D CousoreClick;

    private void Start()
    {
        Cursor.SetCursor(CoursorIdle, new Vector2(), CursorMode.Auto);
    }

    
    void Update () {

        if(Input.GetMouseButton(0))
            Cursor.SetCursor(CousoreClick, new Vector2(), CursorMode.Auto);
        if(Input.GetMouseButtonUp(0))
            Cursor.SetCursor(CoursorIdle, new Vector2(), CursorMode.Auto);

    }
}
