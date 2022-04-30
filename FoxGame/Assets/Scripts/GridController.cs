using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPiece
{
    public Sprite SpriteImage
    {
        get { return SpriteImage; }
        set
        {
            SpriteImage = value;
        }
    }
    public bool isUsed = false;
};

public class GridController : MonoBehaviour
{
    [SerializeField] private int m_AmountOfCols = 2;        // Amount of grid columns
    [SerializeField] private int m_AmountOfRows = 2;       // Amount of grid rows
    [SerializeField] private float m_CellSize = 1.0f;    // Size of each grid cell
    [SerializeField] private float m_GridTileOffset = 0; // Distance between grid tiles
    [SerializeField] private Sprite m_gridSprite;        // Sprite image representing each tile
    

    private float m_GridWidth;
    private float m_GridHeight;

    // Store the grid
    private GridPiece[,] m_grid; 

    // For organizing game objects in unity editor. Store all tiles as children
    private GameObject m_gridParent;

    private void Start()
    {
        // Initialize the grid
        m_grid = new GridPiece[m_AmountOfRows, m_AmountOfCols];
        
        // Create a parent object to store the grid tiles.
        m_gridParent = new GameObject("GridCells");

        m_GridWidth = m_AmountOfCols * m_CellSize;
        m_GridHeight = m_AmountOfRows * m_CellSize;
        Debug.Log(m_GridWidth + ", " + m_GridHeight);

        DisplayGrid();
    }

    private void Update()
    {
        // Get the mouse position in world coordinates.
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Convert mouse position in grid coordinates. Just cast to int
        Vector2Int mouseGridPos = new Vector2Int((int)mousePos.x, (int)mousePos.y);

        Debug.Log("Mouse: " + mouseGridPos);

        if (mouseGridPos.x >= m_GridWidth)
        {

        }
    }

    private void DisplayGrid()
    {
        for(int i = 0; i < m_AmountOfRows; i++)
        {
            for (int j = 0; j < m_AmountOfCols; j++)
            {
                // Create and position game object.
                GameObject newGo = new GameObject(i + ", " + j);
                newGo.transform.position = new Vector2((i * m_CellSize) - (m_GridWidth/2), j * m_CellSize - (m_GridHeight/2));

                // Create sprite at position so we can see the grid.
                SpriteRenderer newSprite = newGo.AddComponent<SpriteRenderer>();
                newSprite.sprite = m_gridSprite;

                // Set objects parent to be organized in the editor.
                newGo.transform.SetParent(m_gridParent.transform);
            }
        }
    }
}
