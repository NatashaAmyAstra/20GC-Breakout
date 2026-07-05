using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public static BlockManager Instance;

    public event EventHandler<OnPointsScoredEventArgs> OnPointsScored;
    public class OnPointsScoredEventArgs : EventArgs
    {
        public int Points;
    }


    [SerializeField] private LevelLayoutSO[] _levelLayoutSOArray;
    [SerializeField] private Transform _blockPrefab;

    [SerializeField] private int _pointsPerBlock;

    private int _activeLevelIndex;

    private List<Block> _blockList = new();
    private Grid _grid;

    private void Awake()
    {
        Instance = this;
        _grid = GetComponent<Grid>();
    }

    private void Start()
    {
        _activeLevelIndex = 0;
        InstantiateLevel();
    }


    private void InstantiateLevel()
    {
        foreach(Vector3Int position in _levelLayoutSOArray[_activeLevelIndex].BlockPositionArray)
        {
            Vector3 worldPosition = _grid.CellToWorld(position);
            Block block = Instantiate(_blockPrefab, worldPosition, Quaternion.identity).GetComponent<Block>();

            block.SetManager(this);
            _blockList.Add(block);
        }
    }


    public void RegisterBlockBreak(Block block)
    {
        OnPointsScored?.Invoke(this, new OnPointsScoredEventArgs
        {
            Points = _pointsPerBlock
        });


        _blockList.Remove(block);

        if(_blockList.Count == 0)
        {
            LevelComplete();
        }
    }

    private void LevelComplete()
    {
        // code for on completion

        // load new level
        _activeLevelIndex++;
        InstantiateLevel();
    }
}
