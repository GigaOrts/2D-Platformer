using UnityEngine;

public class WalkingEnemy : Entity
{
    [SerializeField] private float _speed = 1f;

    private SpriteRenderer sprite;
    private Vector3 _direction;

    private readonly float _sphereRadius = 0.01f;
    private readonly float _directionMult = 0.55f;

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
            player.GetDamage();
    }

    private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up
            + transform.right * _direction.x * _directionMult, _sphereRadius);

        if (colliders.Length > 0)
            _direction *= -1f;

        transform.position = Vector3.MoveTowards(transform.position, transform.position + _direction, _speed * Time.deltaTime);
        sprite.flipX = _direction.x > 0f;
    }
}