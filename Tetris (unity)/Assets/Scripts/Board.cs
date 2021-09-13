using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public Tilemap tilemap { get; private set; }
    public Piece activePiece { get; private set; }
    public TetrominoData[] tetrominoes;
    public Vector3Int spawn_pos;
    public Vector2Int board_size = new Vector2Int(10, 20);

    public RectInt Bounds
    {
        get
        {
            Vector2Int position = new Vector2Int(-this.board_size.x / 2, -this.board_size.y / 2);
            return new RectInt(position, this.board_size);
        }
    }

    private void Awake()
    {
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.activePiece = GetComponentInChildren<Piece>();

        for (int i = 0; i < this.tetrominoes.Length; i++)
        {
            this.tetrominoes[i].Initialize();
        }
    }

    private void Start()
    {
        SpawnPiece();
    }

    public void SpawnPiece()
    {
        int random = Random.Range(0, this.tetrominoes.Length);
        TetrominoData data = this.tetrominoes[random];

        this.activePiece.Initialize(this, data, this.spawn_pos);
        Set(this.activePiece);
    }

    public void Set(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tile_pos = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tile_pos, piece.data.tile);
        }
    }

    public void Clear(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tile_pos = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tile_pos, null);
        }
    }
    public bool IsValidPosition(Piece piece, Vector3Int position)
    {
        RectInt bounds = this.Bounds;

        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tile_pos= piece.cells[i] + piece.position;

            if (!bounds.Contains((Vector2Int)tile_pos))
            {
                return false;
            }

            if (this.tilemap.HasTile(tile_pos))
            {
                return false;
            }
        }

        return true;
    }
}
