using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool _controlEnabled = true;

    private MoveManager _moveManager;
    private StateManager _stateManager;
    private Player _player;

    void Awake()
    {
        _moveManager = GetComponent<MoveManager>();
        _stateManager = GetComponent<StateManager>();
        _player = GetComponent<Player>();
    }

    void Update()
    {
        if (_controlEnabled && !_stateManager.Death)
        {
            if(Input.GetButton("Vertical") && _moveManager.OnLadder)
            {
                _moveManager.ClimbOnLadder(Input.GetAxis("Vertical"));
                return;
            }
            _moveManager.ChangeMoveXValue(Input.GetAxis("Horizontal"));

            if (Input.GetButtonDown("Jump"))
                _moveManager.Jump = true; 
            if (Input.GetButtonUp("Jump"))
                _moveManager.Jump = false;

            if (Input.GetKeyDown("a"))
                _player.Attack();




            //if (Input.GetKeyDown("a"))
            //    _moveManager.Roll = true; 
        }
    }
}
