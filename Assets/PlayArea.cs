using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayArea : MonoBehaviour
{

    public static Vector2 Frame
    {
        get
        {
            if (!framed)
            {
                float height = Camera.main.orthographicSize;
                float width = height * Camera.main.aspect;
                _frame = new Vector2(width, height);
                framed = true;
            }
            return _frame;
        }
    }

    static Vector2 _frame;
    static bool framed = false;
}
