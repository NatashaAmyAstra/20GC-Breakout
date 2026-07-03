using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public event EventHandler OnSizeChanged;


    [SerializeField] private float _size;
    [SerializeField] private float _speed;


    private CircleCollider2D _collider;
    private Rigidbody2D _rigidbody;

    private Vector2 _moveVector;



    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CircleCollider2D>();
        UpdateSize();

        _moveVector = Vector2.down;
    }

    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = _moveVector * _speed;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Player player))
        {
            _moveVector = player.GetBounceVectorNormalized(collision.GetContact(0).point);
            return;
        }

        _moveVector = Vector2.Reflect(_moveVector, collision.GetContact(0).normal);
        _moveVector.Normalize();

        if(collision.gameObject.TryGetComponent(out Block block))
        {
            block.BreakBlock();
        }
    }


    

    private void UpdateSize()
    {
        _collider.radius = _size / 2;
        OnSizeChanged?.Invoke(this, EventArgs.Empty);
    }



    public void SetSize(float size)
    {
        _size = size;
        UpdateSize();
    }

    public float GetSize()
    {
        return _size;
    }
}
