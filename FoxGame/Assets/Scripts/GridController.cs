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

    public GameObject cellObject;

    public bool isUsed = false;
    public bool isActive = true;
};

public class GridController : MonoBehaviour
{
    [SerializeField] private int m_AmountOfCols = 2;     // Amount of grid columns
    [SerializeField] private int m_AmountOfRows = 2;     // Amount of grid rows
    [SerializeField] private float m_CellSize = 1.0f;    // Size of each grid cell
    [SerializeField] private Sprite m_gridSprite;        // Sprite image representing each tile

    private float m_GridWidth;
    private float m_GridHeight;

    // Store the grid. 
    private GridPiece[,] m_grid;

    private GridPiece m_ActivePiece = null; // Current active grid cell (the mouse is over it.)

    private void Start()
    {
        // Get the size of the grid. This needs to come before the Initialize function
        m_GridWidth = m_AmountOfCols * m_CellSize;
        m_GridHeight = m_AmountOfRows * m_CellSize;

        // Initialize the grid
        m_grid = new GridPiece[m_AmountOfRows, m_AmountOfCols];
        
        InitializeGridCells();
        DisplayGrid();
    }

    private void Update()
    {
        // Get the mouse position in world coordinates.
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // If the mouse is in the grid bounds
        if (IsInGrid(mousePos, Vector2.zero))
        {
            //Debug.Log("MousePos: " + mousePos);

            // changes color of cell.
            HighlightCell(mousePos);
        }
        else
        {
            // Make sure no cells are highlighted if were not in the grid.
            if (m_ActivePiece != null)
            {
                m_ActivePiece.cellObject.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }

    private void InitializeGridCells()
    {
        for (int i = 0; i < m_AmountOfRows; i++)
        {
            for (int j = 0; j < m_AmountOfCols; j++)
            {
                // Create a new grid tile to add to grid array
                GridPiece cellPiece = new GridPiece();

                // Create and position game object at the center of the world.
                cellPiece.cellObject = new GameObject(i + ", " + j);

                //cellPiece.cellObject.transform.position = new Vector2(i * m_CellSize - (m_GridWidth / 2 - m_CellSize / 2), j * m_CellSize - (m_GridHeight / 2 - m_CellSize / 2));
                cellPiece.cellObject.transform.position = new Vector2(i * m_CellSize - ((m_GridWidth - m_CellSize) / 2), j * m_CellSize - ((m_GridHeight - m_CellSize) / 2));
                
                // Create sprite at position so we can see the grid.
                cellPiece.cellObject.AddComponent<SpriteRenderer>().sprite = m_gridSprite;

                // Set objects parent to the GameObject the script is attached to for organization in the editor.
                cellPiece.cellObject.transform.SetParent(this.transform);

                // Set the piece to inactive so it doesn't display by default.
                cellPiece.cellObject.SetActive(false);

                // Add the object to the grid
                m_grid[i, j] = cellPiece;
            }
        }
    }

    private void DisplayGrid()
    {
        for(int i = 0; i < m_AmountOfRows; i++)
        {
            for (int j = 0; j < m_AmountOfCols; j++)
            {
                if (m_grid[i, j].isActive)
                {
                    m_grid[i, j].cellObject.SetActive(true);
                }
            }
        }
    }

    bool IsInGrid(Vector2 position, Vector2 size)
    {
        // Grid Bounds
        float gridLeft = -(m_GridWidth / 2);
        float gridBottom = -(m_GridHeight / 2);
        float gridRight  = gridLeft + m_GridWidth;
        float gridTop    = gridBottom + m_GridHeight;

        // draw the grid bounds for debugging purposes.
        Debug.DrawLine(new Vector3(gridLeft, gridBottom, 0), new Vector3(gridLeft, gridTop, 0), Color.red);
        Debug.DrawLine(new Vector3(gridLeft, gridBottom, 0), new Vector3(gridRight, gridBottom, 0), Color.red);
        Debug.DrawLine(new Vector3(gridRight, gridBottom, 0), new Vector3(gridRight, gridTop, 0), Color.red);
        Debug.DrawLine(new Vector3(gridLeft, gridTop, 0), new Vector3(gridRight, gridTop, 0), Color.red);

        if ((position.x + size.x/2) > gridLeft &&
            (position.x - size.x/2) < gridRight &&
            (position.y + size.y/2) > gridBottom &&
            (position.y - size.y/2) < gridTop)
        {
            return true;
        }

        return false;
    }

    void HighlightCell(Vector2 cellPos)
    {
        // Get cell at some world position
        //GridPiece cell = GetGridCellAt(cellPos);
        if (m_ActivePiece != null)
        {
            m_ActivePiece.cellObject.GetComponent<SpriteRenderer>().color = Color.white;
        }

        m_ActivePiece = GetGridCellAt(cellPos);

        // if the cell exists change its color.
        if (m_ActivePiece != null)
        {
            m_ActivePiece.cellObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    GridPiece GetGridCellAt(Vector2 cellPos)
    {
        int indX = (int)((cellPos.x + (m_GridWidth / 2)) / m_CellSize);
        int indY = (int)((cellPos.y + (m_GridHeight / 2)) / m_CellSize);

        // If something goes wrong return null;
        if (indX < 0 || indY < 0 || indX >= m_AmountOfCols || indY >= m_AmountOfRows)
        {
            return null;
        }

        return m_grid[indX, indY];
    }
}
