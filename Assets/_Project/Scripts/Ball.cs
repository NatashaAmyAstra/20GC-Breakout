using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public static Ball Instance;

    public event EventHandler OnSizeChanged;


    [SerializeField] private float _size;
    [SerializeField] private float _speed;


    private CircleCollider2D _collider;
    private Rigidbody2D _rigidbody;

    private Vector2 _moveVector;



    private void Awake()
    {
        Instance = this;

        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CircleCollider2D>();
        UpdateSize();

        _moveVector = Vector2.down;
    }

    private void FixedUpdate()
    {
        // move the ball at a constant pace
        _rigidbody.linearVelocity = _moveVector * _speed;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // bouncing off the player sets an angle based on where the player was hit
        if(collision.gameObject.TryGetComponent(out Player player))
        {
            _moveVector = player.GetBounceVectorNormalized(collision.GetContact(0).point);
            return;
        }
        
        // bounce off of non-player objects at a reflected angle
        _moveVector = Vector2.Reflect(_moveVector, collision.GetContact(0).normal).normalized;
        
        // if the collided object is a block, break it
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

    public void SetDirection(Vector2 direction)
    {
        _moveVector = direction.normalized;
    }
}
