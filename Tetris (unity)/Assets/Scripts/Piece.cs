using UnityEngine;

public class Piece : MonoBehaviour
{
    public Board board { get; private set; }
    public TetrominoData data { get; private set; }
    public Vector3Int[] cells { get; private set; }
    public Vector3Int position { get; private set; }

    public void Initialize(Board board, TetrominoData data, Vector3Int position)
    {
        this.board = board;
        this.data = data;
        this.position = position;
        
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

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Vector2Int.left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Vector2Int.right);
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(Vector2Int.down);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            HardDrop();
        }

        this.board.Set(this);
    }

    private void HardDrop()
    {
        while (Move(Vector2Int.down))
        {
            continue;
        }
    }

    private bool Move(Vector2Int translation)
    {
        Vector3Int new_pos = this.position;
        new_pos.x += translation.x;
        new_pos.y += translation.y;

        bool valid = this.board.IsValidPosition(this, new_pos);

        if (valid)
        {
            this.position = new_pos;
        }

        return valid;
    }
}
