using System;
using UnityEngine;

public class Player : MonoBehaviour
{

    public event EventHandler OnSizeChanged;


    [SerializeField] private float _size;
    [SerializeField] private float _speed;
    
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
        float clampValue = PlayArea.Width / 2 - _size / 2;

        newXPosition = Mathf.Clamp(newXPosition, -clampValue, clampValue);
        Vector3 newPosition = new Vector2(newXPosition, transform.position.y);

        transform.position = newPosition;
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

    public float GetNormalizedContactPoint(Vector3 contactPoint)
    {
        return (contactPoint.x - transform.position.x) / _size;
    }
}
