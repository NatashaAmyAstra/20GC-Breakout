using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private BlockManager _blockManager;

    public void BreakBlock()
    {
        _blockManager.RegisterBlockBreak(this);
        Destroy(gameObject);
    }

    public void SetManager(BlockManager manager)
    {
        _blockManager = manager;
    }
}
