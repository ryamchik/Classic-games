using UnityEngine;
using UnityEngine.Tilemaps;

public enum Tetromino
{
    I,
    O,
    T,
    J,
    L,
    S,
    Z,
}

[System.Serializable]
public struct TetrominoData
{
    public Tetromino tetromino;
    public Tile tile;
    public Vector2Int[] cells { get; private set; }
    public Vector2Int[,] wall_kicks { get; private set; }

    public void Initialize()
    {
        this.cells = Data.Cells[this.tetromino];
        this.wall_kicks = Data.WallKicks[this.tetromino];
    }
}

