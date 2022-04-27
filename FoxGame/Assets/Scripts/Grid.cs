using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPiece
{
    //public float width = 0;
    //public float height = 0;

    //public virtual void Create() { }
    //public virtual void Build() { }

    public bool isUsed = false;
}

public class Grid
{
    public GridPiece[,] grid;
    
    public float PosX { get; set; }
    public float PosY { get; set; }
    public int GridWidth { get; set; }
    public int GridHeight { get; set; }

    private float cellSize = .5f;
    public float CellSize { 
        get { return cellSize; } 
        set { 
            if (value != 0) 
            { 
                cellSize = value; 
            } 
        } 
    }


    public Grid(int x, int y, int w, int h)
    {
        PosX = x;
        PosY = y;
        GridWidth = w;
        GridHeight = h;

        grid = new GridPiece[GridWidth, GridHeight];
    }
}
