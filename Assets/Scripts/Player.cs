using UnityEngine;

public class Player : Entity
{
    private SpriteRenderer _sprite;
    private Rigidbody2D _rigidBody2D;
    private Animator _animator;
    private Vector3 _direction;

    private readonly float _jumpForce = 12f;
    private readonly float _speed = 3f;
    private int _lives = 5;
    private bool _isGrounded = false;

    private enum States
    {
        Idle,
        Run,
        Jump
    }

    private States State
    {
        get { return (States)_animator.GetInteger("state"); }
        set { _animator.SetInteger("state", (int)value); }
    }

    private void Awake()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_isGrounded)
            State = States.Idle;
        if (Input.GetButton("Horizontal"))
            Run();
        if (_isGrounded && Input.GetButtonDown("Jump"))
            Jump();
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    public override void GetDamage()
    {
        if (_lives > 0)
        {
            _lives--;
            Debug.Log("Здоровье игрока: " + _lives);
        }
        else
        {
            Die();
            Debug.Log("Вы мертвы.");
        }
    }

    private void Run()
    {
        if (_isGrounded)
            State = States.Run;

        _direction = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + _direction, _speed * Time.deltaTime);

        _sprite.flipX = _direction.x < 0.0f;
    }

    private void Jump()
    {
        _rigidBody2D.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        _isGrounded = collider.Length > 1;

        if (!_isGrounded)
            State = States.Jump;
    }
}