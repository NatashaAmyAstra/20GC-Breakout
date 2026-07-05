using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public event EventHandler OnSizeChanged;


    [SerializeField] private Transform _ballHoldPositionTransform;
    [SerializeField] private LayerMask _collisionLayerMask;
    [SerializeField] private float _size;
    [SerializeField] private float _speed;
    [SerializeField] private float _initialShotAngle = 20f;

    [SerializeField] private float _maximumBallBounceAngle = 80f;
    
    private CapsuleCollider2D _collider;

    private bool _isFirstShot;


    private void Awake()
    {
        Instance = this;

        _collider = GetComponent<CapsuleCollider2D>();
        UpdateSize();

        InputManager.OnShootPerformed += Shoot;
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

    private void Shoot(object sender, EventArgs e)
    {
        Ball.Instance.transform.parent = null;
        Ball.Instance.enabled = true;

        Vector2 shootDirection = GetBounceVectorNormalized(Ball.Instance.transform.position);
        if (_isFirstShot)
        {
            shootDirection = Quaternion.Euler(0f, 0f, _initialShotAngle) * Vector2.up;
        }

        Ball.Instance.SetDirection(shootDirection);
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


    private void GrabBall(float ballPositionFactor)
    {
        Ball.Instance.transform.position = BallFactorToPosition(ballPositionFactor, _ballHoldPositionTransform.position.y);
        Ball.Instance.transform.parent = _ballHoldPositionTransform;
        Ball.Instance.enabled = false;
    }


    public void GetReady()
    {
        transform.position = new Vector2(0, transform.position.y);
        GrabBall(0);
        _isFirstShot = true;
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

    public float BallPositionToFactor(Vector2 ballPosition)
    {
        return (transform.position.x - ballPosition.x) / (_size / 2);
    }

    public Vector2 BallFactorToPosition(float factor, float yPosition)
    {
        float xPosition = transform.position.x + factor * (_size / 2);
        return new Vector2(xPosition, yPosition);
    }

    public Vector2 GetBounceVectorNormalized(Vector2 contactPoint)
    {
        float contactPointFactor = BallPositionToFactor(contactPoint);

        Vector2 bounceVector = Vector2.up;
        bounceVector = Quaternion.Euler(0, 0, _maximumBallBounceAngle * contactPointFactor) * bounceVector;

        return bounceVector.normalized;
    }
}
