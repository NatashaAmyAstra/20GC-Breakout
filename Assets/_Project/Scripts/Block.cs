using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    public event EventHandler OnBlockBroken;


    public void BreakBlock()
    {
        OnBlockBroken?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }
}
