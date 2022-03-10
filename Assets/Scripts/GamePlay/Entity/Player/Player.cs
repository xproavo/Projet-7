using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private StateManager _stateManager;


    void Start()
    {
        _stateManager = GetComponent<StateManager>();

        _stateManager.OnTakeDamage += UIManager.Instance.UpdatePlayerLifeBar;
        _stateManager.OnTakeDamage += UIManager.Instance.OnPlayerDeath;
    }



}
