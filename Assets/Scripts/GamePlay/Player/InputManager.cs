using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public LayerMask GroundLayer;


    public float SpeedMove = 5;


    public bool IsGrounded = false;

    private bool _controlEnabled = true;
    private bool _jump, _stopJump;

    private Vector2 _move;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private Collider2D _collider2d;
    private Rigidbody2D _body2d;

    private JumpState _jumpState = JumpState.Grounded;



    // Start is called before the first frame update
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _collider2d = GetComponent<Collider2D>();
        _body2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_controlEnabled)
        {
            _move.x = Input.GetAxis("Horizontal");
            if (_jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
                _jumpState = JumpState.PrepareToJump;

            else if (Input.GetButtonDown("Jump"))
                _stopJump = true;
        }
        Movement();
    }

    void UpdateJumpState()
    {
        _jump = false;
        switch (_jumpState)
        {
            case JumpState.PrepareToJump:
                _jumpState = JumpState.Jumping;
                _jump = true;
                _stopJump = false;
                break;

            case JumpState.Jumping:
                if (!IsGrounded)
                    _jumpState = JumpState.InFlight;
                break;

            case JumpState.InFlight:
                if (IsGrounded)
                    _jumpState = JumpState.Landed;
                break;

            case JumpState.Landed:
                _jumpState = JumpState.Grounded;
                break;
        }
    }


    private void Movement()
    {
        _body2d.velocity = _move * SpeedMove;

        if (_move.x > 0.01f)
        {
            _spriteRenderer.flipX = false;
        }
        else if (_move.x < -0.01f)
        {
            _spriteRenderer.flipX = true;
        }


        Vector3 rayCastDir = Vector3.down;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayCastDir, 0.5f, GroundLayer.value);
        IsGrounded = hit.collider != null;


        _animator.SetFloat("velocityX", Mathf.Abs(_body2d.velocity.x));
        _animator.SetBool("grounded", IsGrounded);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, _body2d.velocity);
    }

    public enum JumpState
    {
        Grounded,
        PrepareToJump,
        Jumping,
        InFlight,
        Landed
    }
}
