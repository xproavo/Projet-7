using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private bool _controlEnabled = true;

    private MoveManager _moveManager;


    void Awake()
    {
        _moveManager = GetComponent<MoveManager>();
    }

    void Update()
    {
        if (_controlEnabled)
        {
            _moveManager.ChangeMoveXValue(Input.GetAxis("Horizontal"));

            if (Input.GetButtonDown("Jump"))
                _moveManager.Jump = true;

            if (Input.GetKeyDown("a"))
                _moveManager.Jump = false;
        }
    }
}
