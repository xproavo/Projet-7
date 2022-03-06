using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private bool _controlEnabled = true;

    private MoveManager _moveManager;
    private StateManager _stateManager;

    void Awake()
    {
        _moveManager = GetComponent<MoveManager>();
        _stateManager = GetComponent<StateManager>();
    }

    void Update()
    {
        if (_controlEnabled && !_stateManager.Death)
        {
            _moveManager.ChangeMoveXValue(Input.GetAxis("Horizontal"));

            if (Input.GetButtonDown("Jump"))
                _moveManager.Jump = true;
            if (Input.GetButtonUp("Jump"))
                _moveManager.Jump = false;

            //if (Input.GetKeyDown("a"))
            //    _moveManager.Roll = true; 
        }
    }
}
