using UnityEngine;
using UnityEngine.Assertions.Must;

public enum Direction
{
    Left,
    Right,
    Up,
    Down
}

public class Cart : MonoBehaviour
{
    //움직이는 중인지의 여부
    private bool isMoving;
    //방향좌표
    private Vector3 direction;
    //방향
    private Direction dirType;

    //현재 위치
    public Vector3 currentPos;
    //다음 위치
    public Vector3 nextPos;
    //원점 위치
    private Vector3 originPos;

    //길 사이의 간격
    private float interval = 3f;
    // 목표지점에 도착했는지의 여부
    public bool isGoal;

    // 조종패널로 인해 조종당하고 있는지의 여부
    public bool isControlling;

    //길
    int[,] path = {
    { 0, 0, 0, 0, 1, 0, 0, 0, 0, 2 },
    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 },
    { 0, 0, 1, 0, 0, 0, 0, 0, 0, 1 },
    { 1, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
};

    void Start()
    {
        isMoving = false;
        isGoal = false;
        isControlling = false;
        originPos = gameObject.transform.position;
        currentPos = gameObject.transform.position;
    }

    void Update()
    {
        if(!isMoving && isControlling && Input.GetKeyUp(KeyCode.Space))
        {
            isControlling = false;
        }

        if (isControlling)
        {
            DoCommandMove();
            if (isMoving)
            {
                transform.position += direction * 8f * Time.deltaTime;

                switch (dirType)
                {
                    case Direction.Up:
                        if (nextPos.z <= transform.position.z)
                        {
                            currentPos = nextPos;
                            isMoving = false;
                        }
                        break;
                    case Direction.Down:
                        if (nextPos.z >= transform.position.z)
                        {
                            currentPos = nextPos;
                            isMoving = false;
                        }
                        break;
                    case Direction.Left:
                        if (nextPos.x >= transform.position.x)
                        {
                            currentPos = nextPos;
                            isMoving = false;
                        }
                        break;
                    case Direction.Right:
                        if (nextPos.x <= transform.position.x)
                        {
                            currentPos = nextPos;
                            isMoving = false;
                        }
                        break;
                }
            }

            if (!isMoving)
            {
                CheckGoal(currentPos);
            }
        }
    }

    /// <summary>
    /// 키 입력을 받고 해당 방향을 정해주며 카트 회전을 해주는 기능
    /// </summary>
    public void DoCommandMove()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow) && !isMoving)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, -90, 0);
            direction = Vector3.forward;
            dirType = Direction.Up;
            nextPos = Arraival(currentPos, Direction.Up);
            isMoving = true;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) && !isMoving)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            direction = Vector3.right;
            dirType = Direction.Right;
            nextPos = Arraival(currentPos, Direction.Right);
            isMoving = true;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow) && !isMoving)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
            direction = Vector3.back;
            dirType = Direction.Down;
            nextPos = Arraival(currentPos, Direction.Down);
            isMoving = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) && !isMoving)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            direction = Vector3.left;
            dirType = Direction.Left;
            nextPos = Arraival(currentPos, Direction.Left);
            isMoving = true;
        }
    }

    /// <summary>
    /// 다음 목적지를 정해주는 기능
    /// </summary>
    /// <param name="vector"> 현재 위치 </param>
    /// <param name="direction"> 이동 방향 </param>
    /// <returns></returns>
    public Vector3 Arraival(Vector3 vector, Direction direction)
    {
        int curPathXPos = (int)((vector.x - originPos.x) / interval);
        int curPathZPos = (int)((originPos.z - vector.z) / interval);

        if (direction == Direction.Right)
        {
            for (int i = curPathXPos; i < 10; i++)
            {
                if (path[curPathZPos, i] == 1)
                {
                    return new Vector3((i - 1) * interval + originPos.x, originPos.y, originPos.z - curPathZPos * interval);
                }
            }
            return new Vector3(9 * interval + originPos.x, originPos.y, originPos.z - curPathZPos * interval);
        }

        else if (direction == Direction.Left)
        {
            for (int i = curPathXPos; i >= 0; i--)
            {
                if (path[curPathZPos, i] == 1)
                {
                    return new Vector3((i + 1) * interval + originPos.x, originPos.y, originPos.z - curPathZPos * interval);
                }
            }
            return new Vector3(originPos.x, originPos.y, originPos.z - curPathZPos * interval);
        }

        else if (direction == Direction.Down)
        {
            for (int i = curPathZPos; i < 10; i++)
            {
                if (path[i, curPathXPos] == 1)
                {
                    return new Vector3(curPathXPos * interval + originPos.x, originPos.y, originPos.z - (i - 1) * interval);
                }
            }
            return new Vector3(curPathXPos * interval + originPos.x, originPos.y, originPos.z - 9 * interval);
        }

        else if (direction == Direction.Up)
        {
            for (int i = curPathZPos; i >= 0; i--)
            {
                if (path[i, curPathXPos] == 1)
                {
                    return new Vector3(curPathXPos * interval + originPos.x, originPos.y, originPos.z - (i + 1) * interval);
                }
            }
            return new Vector3(curPathXPos * interval + originPos.x, originPos.y, originPos.z);
        }

        return new Vector3();
    }

    /// <summary>
    /// 카트가 골인지점에 들어왔는지 체크하는 기능
    /// </summary>
    /// <param name="vector"></param>
    void CheckGoal(Vector3 vector)
    {
        int curPathXPos = (int)((vector.x - originPos.x) / interval);
        int curPathZPos = (int)((originPos.z - vector.z) / interval);

        if (path[curPathZPos, curPathXPos] == 2 && !isGoal)
        {
            isGoal = true;
            isControlling = false;
        }
    }
}
