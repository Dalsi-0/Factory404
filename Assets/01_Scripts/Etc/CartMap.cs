using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartMap : MonoBehaviour
{
    public GameObject cart;

    private float interval = 3f;

    int[,] path = {
    {0, 0, 0, 0, 1, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 1, 0, 0},
    {0, 2, 1, 0, 0, 0, 0, 0, 0, 1},
    {1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 1, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 1, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
};

    public Vector3 Arraival(Vector3 vector, Direction direction)
    {
        int x = (int)(vector.x / interval);
        int y = (int)(vector.z / interval);
        if (direction == Direction.Right)
        {
            for (int i = y; i < 10; i++)
            {
                if (path[x, i] == 1)
                {
                    return new Vector2(x * interval + 3, (i - 1) * interval + 3);
                }
            }
            return new Vector2(x * interval + 3, 7 * interval + 3);
        }
        else if (direction == Direction.Left)
        {
            for (int i = y; i >= 0; i--)
            {
                if (path[x, i] == 1)
                {
                    return new Vector2(x * interval + 3, (i + 1) * interval + 3);
                }
            }
            return new Vector2(x * interval + 3, 3);
        }
        else if (direction == Direction.Up)
        {
            for (int i = x; i < 10; i++)
            {
                if (path[i, y] == 1)
                {
                    return new Vector2((i - 1) * interval + 3, y * interval + 3);
                }
            }
            return new Vector2(7 * interval + 3, y * interval + 3);
        }
        else if (direction == Direction.Down)
        {
            for (int i = x; i >= 0; i--)
            {
                if (path[i, y] == 1)
                {
                    return new Vector2((i + 1) * interval + 3, y * interval + 3);
                }
            }
            return new Vector2(3, y * interval + 3);
        }


        return new Vector3();
    }
}

