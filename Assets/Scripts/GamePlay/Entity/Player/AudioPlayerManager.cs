using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerManager : MonoBehaviour
{

    private AudioSource _audioSource;
    private MoveManager _moveManager;


    private void Awake()
    {
        _audioSource = GetComponentInChildren<AudioSource>();
        _moveManager = GetComponent<MoveManager>();
    }

    private void Update()
    {
        if (_moveManager.IsGrounded && _moveManager.Movable)
        {
            _audioSource.volume = Mathf.Abs(_moveManager.GetMoveXValue());
        }
        else
        {
            _audioSource.volume = 0;
        }
    }


}
