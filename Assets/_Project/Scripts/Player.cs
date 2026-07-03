using System;
using UnityEngine;

public class Player : MonoBehaviour
{

    public event EventHandler OnSizeChanged;


    [SerializeField] private LayerMask _collisionLayerMask;
    [SerializeField] private float _size;
    [SerializeField] private float _speed;

    [SerializeField] private float _maximumBallBounceAngle = 80f;
    
    private CapsuleCollider2D _collider;


    private void Awake()
    {
        _collider = GetComponent<CapsuleCollider2D>();
        UpdateSize();
    }


    private void Update()
    {
        HandleMovement();
    }



    private void HandleMovement()
    {
        float newXPosition = transform.position.x + InputManager.PlayerXInputValue * _speed * Time.deltaTime;

        Vector2 newPosition = new Vector2(newXPosition, transform.position.y);
        HandleWallCollision(ref newPosition);

        transform.position = newPosition;
    }

    private void HandleWallCollision(ref Vector2 newPosition)
    {
        Collider2D collision = Physics2D.OverlapCapsule(newPosition, _collider.size, _collider.direction, 0f, _collisionLayerMask);
        
        if (collision == null)
            return;

        float closestPointX = collision.ClosestPoint(transform.position).x;
        float direction = Mathf.Sign(newPosition.x - closestPointX);
        newPosition.x = closestPointX + _size / 2 * direction;
    }

    
    private void UpdateSize()
    {
        _collider.size = new Vector2(_size, _collider.size.y);
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

    public Vector2 GetBounceVectorNormalized(Vector2 contactPoint)
    {
        float contactPointFactor = (transform.position.x - contactPoint.x) * 2 / _size;

        Vector2 bounceVector = Vector2.up;
        bounceVector = Quaternion.Euler(0, 0, _maximumBallBounceAngle * contactPointFactor) * bounceVector;

        return bounceVector.normalized;
    }
}
