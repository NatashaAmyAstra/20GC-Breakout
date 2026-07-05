using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    public void BreakBlock()
    {
        BlockManager.Instance.RegisterBlockBreak(this);
        Destroy(gameObject);
    }
}
