using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateFlying : MonoBehaviour
{


    private Animator _animator;
    private Rigidbody2D _body2d;
    private SpriteRenderer _spriteRenderer;

    private MoveManager _moveManager;
    private StateManager _stateManager;
    private BeeIA _beeia;

    private bool _die;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _body2d = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _stateManager = GetComponent<StateManager>();
        _beeia = GetComponent<BeeIA>();
        _moveManager = GetComponent<MoveManager>();

        _die = false;
    }

    private void Update()
    {
        //_animator.SetBool("roll", _moveManager.Roll);

        if (!_die)
        {
            if (_moveManager.GetMoveXValue() > 0f)
            {
                _spriteRenderer.flipX = false;
            }
            else if (_moveManager.GetMoveXValue() < 0f)
            {
                _spriteRenderer.flipX = true;
            }

            _animator.SetFloat("velocityX", Mathf.Abs(_body2d.velocity.x));
            _animator.SetFloat("velocityY", _body2d.velocity.y);
            _animator.SetBool("Grounded", _moveManager.IsGrounded);

        }


        if (_stateManager.Death && !_die)
        {
            _die = true;
            _animator.SetBool("Respawn", false);
            _animator.SetBool("Death", _die);
            _beeia.enabled = false;


        }
        else if (_stateManager.Death && _die)
        {
            _animator.SetBool("Death", false);
        }
        else if (!_stateManager.Death && _die)
        {
            _die = false;
            _animator.SetBool("Respawn", true);
        }
    }

    public void Attack(string attackName)
    {
        _animator.SetTrigger(attackName);
    }

}