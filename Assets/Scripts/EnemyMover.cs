using UnityEngine;

public class EnemyMover : Entity
{
    [SerializeField] private float _speed = 1f;

    private SpriteRenderer sprite;
    private Vector3 _direction;
    private Vector3 _targetPosition;

    private readonly float _sphereRadius = 0.01f;
    private readonly float _directionMult = 0.55f;
    private bool _isMovingRight;

    private void Start()
    {
        _direction = transform.right;
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
            player.TakeDamage();
    }

    private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up
            + transform.right * _direction.x * _directionMult, _sphereRadius);

        if (colliders.Length > 0)
            _direction = -_direction;

        _targetPosition = transform.position + _direction;
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);

        _isMovingRight = _direction.x > 0f;
        sprite.flipX = _isMovingRight;
    }
}