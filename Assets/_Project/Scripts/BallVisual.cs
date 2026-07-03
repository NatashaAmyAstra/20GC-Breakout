using System;
using UnityEngine;

public class BallVisual : MonoBehaviour
{
    [SerializeField] private Ball _ball;

    private void Start()
    {
        _ball.OnSizeChanged += ChangeSize;
        ChangeSize(this, EventArgs.Empty);
    }

    private void ChangeSize(object sender, EventArgs e)
    {
        transform.localScale = Vector3.one * _ball.GetSize();
    }
}
