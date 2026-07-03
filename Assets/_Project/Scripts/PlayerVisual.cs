using System;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private Player _player;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void Start()
    {
        _player.OnSizeChanged += ChangeSize;
        ChangeSize(this, EventArgs.Empty);
    }


    private void ChangeSize(object sender, EventArgs e)
    {
        _spriteRenderer.size = new Vector2(_player.GetSize(), _spriteRenderer.size.y);
    }
}
