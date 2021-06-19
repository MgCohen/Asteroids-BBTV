using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{
    private Vector2 area;

    private void Start()
    {
        area = PlayArea.Frame + Vector2.one;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Mathf.Abs(transform.position.x) > area.x)
            WarpPositionX();

        if (Mathf.Abs(transform.position.y) > area.y)
            WarpPositionY();
    }

    void WarpPositionX()
    {
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, -area.x, area.x);
        position.x *= -1;
        transform.position = position;
    }

    void WarpPositionY()
    {
        Vector3 position = transform.position;
        position.y = Mathf.Clamp(position.y, -area.y, area.y);
        position.y *= -1;
        transform.position = position;
    }
}
