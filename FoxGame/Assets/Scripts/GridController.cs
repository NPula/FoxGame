using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] private int m_GridWidth = 2;
    [SerializeField] private int m_GridHeight = 2;
    [SerializeField] private float m_GridTileOffset; // distance between grid tiles
    [SerializeField] private Sprite m_gridSprite;  // sprite representing each tile
    
    private Grid m_grid;

    private GameObject m_gridParent;

    private void Start()
    {
        m_grid = new Grid(0, 0, m_GridWidth, m_GridHeight);
        m_gridParent = new GameObject("Parent");

        DisplayGrid();
    }

    private void DisplayGrid()
    {
        for(int i = 0; i < m_GridWidth; i++)
        {
            for (int j = 0; j < m_GridHeight; j++)
            {
                GameObject newGo = new GameObject(i + ", " + j);
                newGo.transform.position = new Vector2(i * m_grid.CellSize/* + m_GridTileOffset*/, j * m_grid.CellSize/* + m_GridTileOffset*/);
                SpriteRenderer newSprite = newGo.AddComponent<SpriteRenderer>();
                newSprite.sprite = m_gridSprite;
                //newSprite.size = 
                newGo.transform.SetParent(m_gridParent.transform);
            }
        }
    }
}
