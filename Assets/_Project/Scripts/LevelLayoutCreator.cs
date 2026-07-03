using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class LevelLayoutCreator : MonoBehaviour
{
    [SerializeField] private LevelLayoutSO _levelLayoutSO;
    private Grid _grid;

    private void Awake()
    {
        _grid = GetComponent<Grid>();
    }


    public void ToggleCellAt(Vector2 input)
    {
        Vector2Int gridPoint = (Vector2Int)_grid.WorldToCell(input);

        List<Vector2Int> layoutList = _levelLayoutSO.BlockPositionArray.ToList();

        if (layoutList.Contains(gridPoint))
        {
            layoutList.Remove(gridPoint);
        }
        else
        {
            layoutList.Add(gridPoint);
        }


        _levelLayoutSO.BlockPositionArray = layoutList.ToArray();
    }

    private void OnDrawGizmos()
    {
        if (_levelLayoutSO == null)
            return;

        foreach(Vector2Int gridPoint in _levelLayoutSO.BlockPositionArray)
        {
            Vector3 centerPoint = _grid.CellToWorld((Vector3Int)gridPoint);
            centerPoint += _grid.cellSize / 2;

            Gizmos.DrawCube(centerPoint, _grid.cellSize);
        }
    }
}
