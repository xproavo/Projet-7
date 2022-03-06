using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateManager : MonoBehaviour
{
    

    private Animator _animator;
    private Rigidbody2D _body2d;
    private SpriteRenderer _spriteRenderer;

    private MoveManager _moveManager;
    private StateManager _stateManager;

    public bool Die;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _body2d = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _moveManager = GetComponent<MoveManager>();
        _stateManager = GetComponent<StateManager>();

        Die = false;
    }

    private void Update()
    {
        //_animator.SetBool("roll", _moveManager.Roll);
    }

    private void FixedUpdate()
    {
        if (!Die)
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
            _animator.SetBool("Grounded", _moveManager.IsGrounded);
        }

        
        if (_stateManager.Death && !Die)
        {
            Die = true;
            _animator.SetBool("Respawn", false);
            _animator.SetBool("Death", Die);
        }
        else if (_stateManager.Death && Die)
        {
            _animator.SetBool("Death", false);
        }
        else if (!_stateManager.Death && Die)
        {
            Die = false;
            _animator.SetBool("Respawn", true);
        }
        
    }
}
