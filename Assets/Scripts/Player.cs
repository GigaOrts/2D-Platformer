using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Player : Entity
{
    private SpriteRenderer _sprite;
    private Rigidbody2D _rigidBody2D;
    private Animator _animator;
    private Vector3 _direction;

    private const string CurrentState = "state";
    private readonly float _jumpForce = 12f;
    private readonly float _speed = 3f;
    private readonly float _overlapCircleRadius = 0.3f;
    private float _firstOverlappedObject = 1f;
    private int _lives = 5;
    private bool _isOnGround = false;
    private bool _isMovingLeft;

    private enum States
    {
        Idle,
        Run,
        Jump
    }

    private States State
    {
        get { return (States)_animator.GetInteger(CurrentState); }
        set { _animator.SetInteger(CurrentState, (int)value); }
    }

    private void Awake()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_isOnGround)
            State = States.Idle;

        if (Input.GetButton("Horizontal"))
            Run();

        if (_isOnGround && Input.GetButtonDown("Jump"))
            Jump();
    }

    private void FixedUpdate()
    {
        _isOnGround = IsGrounded();

        if (!_isOnGround)
            State = States.Jump;
    }

    public override void TakeDamage()
    {
        _lives--;

        if (_lives <= 0)
            Die();
    }

    private void Run()
    {
        if (_isOnGround)
            State = States.Run;

        _direction = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + _direction, _speed * Time.deltaTime);

        _isMovingLeft = _direction.x < 0f;
        _sprite.flipX = _isMovingLeft;
    }

    private void Jump()
    {
        _rigidBody2D.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
    }

    private bool IsGrounded()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, _overlapCircleRadius);
        return collider.Length > _firstOverlappedObject;
    }
}