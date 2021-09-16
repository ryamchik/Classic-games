using UnityEngine;

public class Piece : MonoBehaviour
{
    public Board board { get; private set; }
    public TetrominoData data { get; private set; }
    public Vector3Int[] cells { get; private set; }
    public Vector3Int position { get; private set; }
    public int rotat_index { get; private set; }

    public float step_delay = 1f;
    public float lock_delay = 0.5f;

    public float step_time;
    public float lock_time;

    public void Initialize(Board board, TetrominoData data, Vector3Int position)
    {
        this.board = board;
        this.data = data;
        this.position = position;
        this.rotat_index = 0;
        this.step_time = Time.time + this.step_delay;
        this.lock_time = 0f;

        if (this.cells == null)
        {
            this.cells = new Vector3Int[data.cells.Length];
        }

        for (int i = 0; i < data.cells.Length; i++)
        {
            this.cells[i] = (Vector3Int)data.cells[i];
        }
    }

    private void Update()
    {
        this.board.Clear(this);

        this.lock_time += Time.deltaTime;

        // Поворот фиргуры против часовой и по часовой стрелке.
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Rotate(-1);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Rotate(1);
        }

        // Движение фигуры влево и вправо.
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Vector2Int.left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Vector2Int.right);
        }

        // Движение фигуры вниз.
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(Vector2Int.down);
        }

        // Резкое опускание фигуры.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HardDrop();
        }

        if (Time.time >= this.step_time)
        {
            Step();
        }

        this.board.Set(this);
    }

    // Метод для запуска самостоятельного движения фигуры.
    private void Step()
    {
        this.step_time = Time.time + this.step_delay;

        Move(Vector2Int.down);

        if (this.lock_time >= this.lock_delay)
        {
            Lock();
        }
    }

    // Метод для резкого спуска фигуры.
    private void HardDrop()
    {
        while (Move(Vector2Int.down))
        {
            continue;
        }

        Lock();
    }

    //  Методя для блокировки фигуры, достигшей дна.
    public void Lock()
    {
        this.board.Set(this);
        this.board.ClearLines();
        this.board.SpawnPiece();
    }

    //  Метод для перемещения фигуры.
    private bool Move(Vector2Int translation)
    {
        Vector3Int new_pos = this.position;
        new_pos.x += translation.x;
        new_pos.y += translation.y;

        bool valid = this.board.IsValidPosition(this, new_pos);

        if (valid)
        {
            this.position = new_pos;
            this.lock_time = 0f;
        }

        return valid;
    }

    // Метод для поворота фигуры.
    private void Rotate(int direct)
    {
        int orig_rotat = this.rotat_index;
        this.rotat_index = Wrap(this.rotat_index + direct, 0, 4);

        ApplyMatrix(direct);

        if (!TestWall(this.rotat_index, direct))
        {
            this.rotat_index = orig_rotat;
            ApplyMatrix(-direct);
        }
    }

    private void ApplyMatrix(int direct)
    {
        float[] matrix = Data.RotationMatrix;

        for (int i = 0; i < this.cells.Length; i++)
        {
            Vector3 cell = this.cells[i];

            int x, y;

            switch (this.data.tetromino)
            {
                case Tetromino.I:
                case Tetromino.O:
                    cell.x -= 0.5f;
                    cell.y -= 0.5f;
                    x = Mathf.CeilToInt((cell.x * matrix[0] * direct) + (cell.y * matrix[1] * direct));
                    y = Mathf.CeilToInt((cell.x * matrix[2] * direct) + (cell.y * matrix[3] * direct));
                    break;
                default:
                    x = Mathf.RoundToInt((cell.x * matrix[0] * direct) + (cell.y * matrix[1] * direct));
                    y = Mathf.RoundToInt((cell.x * matrix[2] * direct) + (cell.y * matrix[3] * direct));
                    break;
            }

            this.cells[i] = new Vector3Int(x, y, 0);
        }
    }

    // Функция для проверки захождения фигуры за пределы стены.
    private bool TestWall(int rotat_index, int rotat_direct)
    {
        int index = GetIndex(rotat_index, rotat_direct);

        for (int i = 0; i < this.data.wall_kicks.GetLength(1); i++)
        {
            Vector2Int translation = this.data.wall_kicks[index, i];

            if (Move(translation))
            {
                return true;
            }
        }

        return false;
    }

    // Функция для получения индекса прохода за стену.
    private int GetIndex(int rotat_index, int rotat_direct)
    {
        int index = rotat_index * 2;

        if (rotat_direct < 0)
        {
            index--;
        }

        return Wrap(index, 0, this.data.wall_kicks.GetLength(0));
    }

    // Функция для изменения направления фигуры.
    private int Wrap(int input, int min, int max)
    {
        if (input < min)
        {
            return max - (min - input) % (max - min);
        } 
        else
        {
            return min + (input - min) % (max - min);
        }
    }
}
