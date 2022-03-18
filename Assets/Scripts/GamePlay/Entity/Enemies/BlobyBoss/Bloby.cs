using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloby : MonoBehaviour
{
    public Vector2 RandomTimeToAttack;

    public GameObject[] Projectil;

    [Range(0f, 100f)]
    public float RageJauge;

    public bool IsAttack;

    private Animator _animator;
    private StateManager _stateManager;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _stateManager = GetComponent<StateManager>();
    }


    private void Update()
    {
        if (IsAttack)
        {
            Jump();
        }
    }

    private void Jump()
    {
    }
}
