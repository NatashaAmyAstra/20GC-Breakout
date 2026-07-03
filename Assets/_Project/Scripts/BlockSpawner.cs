using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] private LevelLayoutSO _levelLayoutSO;
    [SerializeField] private Transform _blockPrefab;

    private Grid _grid;

    private void Awake()
    {
        _grid = GetComponent<Grid>();
    }

    private void Start()
    {
        foreach(Vector3Int position in _levelLayoutSO.BlockPositionArray)
        {
            Vector3 worldPosition = _grid.CellToWorld(position);
            Instantiate(_blockPrefab, worldPosition, Quaternion.identity);
        }
    }
}
