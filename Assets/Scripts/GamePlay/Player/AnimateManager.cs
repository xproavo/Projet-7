using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateManager : MonoBehaviour
{
    

    private Animator _animator;
    private Rigidbody2D _body2d;
    private SpriteRenderer _spriteRenderer;

    private MoveManager _moveManager;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _body2d = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _moveManager = GetComponent<MoveManager>();
    }

    private void FixedUpdate()
    {
        if (_moveManager.GetMoveXValue() > 0f)
        {
            _spriteRenderer.flipX = false;
        }
        else if (_moveManager.GetMoveXValue() < 0f)
        {
            _spriteRenderer.flipX = true;
        }

        _animator.SetFloat("velocityX", Mathf.Abs(_moveManager.GetMoveXValue()));
        _animator.SetFloat("velocityY", _body2d.velocity.y);
        _animator.SetBool("grounded", _moveManager.IsGrounded);
    }
}
