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

    private List<GridPiece> m_enabledGrid;

    private GridPiece m_ActivePiece = null; // Current active grid cell (the mouse is over it.)

    private void Start()
    {
        // Store enabled grid pieces for easy access.
        m_enabledGrid = new List<GridPiece>();

        // Get the size of the grid. This needs to come before the Initialize function
        m_GridWidth = m_AmountOfCols * m_CellSize;
        m_GridHeight = m_AmountOfRows * m_CellSize;

        // Initialize the grid
        m_grid = new GridPiece[m_AmountOfRows, m_AmountOfCols];
        
        InitializeGridCells();
        //DisplayGrid();
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
            //HighlightCell(mousePos);
        }
        else
        {
            // Make sure no cells are highlighted if were not in the grid.
            if (m_ActivePiece != null)
            {
                //m_ActivePiece.cellObject.GetComponent<SpriteRenderer>().color = Color.white;
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
                SpriteRenderer sprite = cellPiece.cellObject.AddComponent<SpriteRenderer>();
                sprite.sprite = m_gridSprite;
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 10.0f);
                //sprite.color = new Color32(0xBE, 0xBF, 0xBE, 0xFF); // grey color

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

    public void EnableGridInRange(Vector2 start, Vector2 end)
    {
        Vector2 gridStart = WorldPositionToGrid(start);
        Vector2 gridEnd = WorldPositionToGrid(end);

        // If something goes wrong return;
        if (gridStart.x < 0 || gridEnd.x < 0)
        {
            return;
        }

        for (int i = (int)gridStart.x; i < gridEnd.x; i++)
        {
            for (int j = (int)gridStart.y; j < gridEnd.y; j++)
            {
                if (m_grid[i, j].isActive)
                {
                    m_grid[i, j].cellObject.SetActive(true);
                    m_enabledGrid.Add(m_grid[i, j]);

                    if (m_grid[i, j].isUsed)
                    {
                        m_grid[i, j].cellObject.GetComponent<SpriteRenderer>().color = new Color(255.0f, 0.0f, 0.0f, .3f);
                    }
                    else
                    {
                        m_grid[i, j].cellObject.GetComponent<SpriteRenderer>().color = new Color(255.0f, 255.0f, 255.0f, .3f);
                    }
                }

                /* TODO - fix resizing.
                if (i == (int)gridStart.x)
                {
                    ResizeGridAt(i, j);
                }
                if (i == gridEnd.x-1)
                {
                    ResizeGridAt(i, j);
                }
                if (j == (int)gridStart.y)
                {
                    ResizeGridAt(i, j);
                }
                if (i == gridEnd.y - 1)
                {
                    ResizeGridAt(i, j);
                }
                */
            }
        }
    }

    public void SetUsedInRange(Vector2 start, Vector2 end)
    {
        Vector2 gridStart = WorldPositionToGrid(start);
        Vector2 gridEnd = WorldPositionToGrid(end);

        // If something goes wrong return;
        if (gridStart.x < 0 || gridEnd.x < 0)
        {
            return;
        }

        for (int i = (int)gridStart.x; i < gridEnd.x; i++)
        {
            for (int j = (int)gridStart.y; j < gridEnd.y; j++)
            {
                if (m_grid[i, j].isActive)
                {
                    m_grid[i, j].isUsed = true;
                }
            }
        }
    }

    public bool CanPlaceObjectHere(Vector2 start, Vector2 end)
    {
        Vector2 gridStart = WorldPositionToGrid(start);
        Vector2 gridEnd = WorldPositionToGrid(end);

        for (int i = (int)gridStart.x; i < gridEnd.x; i++)
        {
            for (int j = (int)gridStart.y; j < gridEnd.y; j++)
            {
                if (m_grid[i,j].isUsed)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public void ResizeGridAt(int x, int y)
    {
        if (m_grid[x, y].cellObject.transform.localScale == m_gridSprite.bounds.size)
        {
            m_grid[x, y].cellObject.transform.localScale *= .5f;
        }

        //for (int i = (int)start.x; i < end.x; i++)
        //{
        //    m_grid[x, y].cellObject.transform.localScale *= .5f;
        //}
    }

    public void ResetSize()
    {
        //m_enabledGrid[i].cellObject.transform.localScale = m_gridSprite.bounds.size;
    }



    public void DisableGrid()
    {
        for (int i = 0; i < m_enabledGrid.Count; i++)
        {
            // reset the size of the grid sprite.
            m_enabledGrid[i].cellObject.transform.localScale = m_gridSprite.bounds.size;
            m_enabledGrid[i].cellObject.SetActive(false);
            m_enabledGrid.RemoveAt(i);
        }
    }

    public bool IsInGrid(Vector2 position, Vector2 size)
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

    public Vector2 WorldPositionToGrid(Vector2 position)
    {
        Vector2 gridPosition = new Vector2((int)((position.x + (m_GridWidth / 2)) / m_CellSize), (int)((position.y + (m_GridHeight / 2)) / m_CellSize));
       
        // If something goes wrong return a negative value that we can check for; 
        // Maybe change this to return something else later.
        if (gridPosition.x < 0 || gridPosition.y < 0 || gridPosition.x >= m_AmountOfCols || gridPosition.y >= m_AmountOfRows)
        {
            return new Vector2(-1, -1);
        }

        return gridPosition;
    }

    public GridPiece GetGridCellAt(Vector2 cellPos)
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
