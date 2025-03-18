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
    //�����̴� �������� ����
    private bool isMoving;
    //������ǥ
    private Vector3 direction;
    //����
    private Direction dirType;

    //���� ��ġ
    public Vector3 currentPos;
    //���� ��ġ
    public Vector3 nextPos;
    //���� ��ġ
    private Vector3 originPos;

    //�� ������ ����
    private float interval = 3f;
    // ��ǥ������ �����ߴ����� ����
    public bool isGoal;

    // �����гη� ���� �������ϰ� �ִ����� ����
    public bool isControlling;

    //��
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
    /// Ű �Է��� �ް� �ش� ������ �����ָ� īƮ ȸ���� ���ִ� ���
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
    /// ���� �������� �����ִ� ���
    /// </summary>
    /// <param name="vector"> ���� ��ġ </param>
    /// <param name="direction"> �̵� ���� </param>
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
    /// īƮ�� ���������� ���Դ��� üũ�ϴ� ���
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
