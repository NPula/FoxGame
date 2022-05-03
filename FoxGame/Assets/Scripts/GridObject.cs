using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    // Grid in editor with the Grid Controller on it.
    [SerializeField] private GridController m_grid;
    
    // The amount of grid slots this object takes.
    [SerializeField] protected int objectGridSize;

    // Distance around the object the we want to draw the grid.
    [SerializeField] private float gridDistanceScale = 1.5f;


    private bool m_placedObject;

    private void Awake()
    {
        m_grid = FindObjectOfType<GridController>();
    }

    private void Start()
    {
        m_placedObject = false;
    }

    private void Update()
    {
        m_grid.DisableGrid();

        if (!m_placedObject)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;

            if (m_grid.IsInGrid(transform.position, Vector2.zero))
            {
                GridPiece cell = m_grid.GetGridCellAt(transform.position);
            
                if (!cell.isUsed)
                {
                    transform.position = cell.cellObject.transform.position;

                    // Show the grid only at the objects location
                    m_grid.EnableGridInRange(transform.position - transform.localScale, transform.position + transform.localScale * gridDistanceScale);

                    if (Input.GetMouseButtonDown(0))
                    {
                        //if (m_grid.CanPlaceObjectHere(transform.position - transform.localScale, transform.position + transform.localScale * gridDistanceScale))
                       // {
                        //}
                        m_grid.SetUsedInRange(transform.position - transform.localScale, transform.position + transform.localScale * gridDistanceScale);
                        //cell.isUsed = true;
                        m_placedObject = true;
                    }
                }

            }
        }
    }
}
