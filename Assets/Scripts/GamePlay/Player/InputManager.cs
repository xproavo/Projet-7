using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{



    private bool _controlEnabled = true;
    private bool _jump, _stopJump;

    private Vector2 _move;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private JumpState _jumpState = JumpState.Grounded;


    // Start is called before the first frame update
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
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
       
    }

    private void Movement()
    {

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
