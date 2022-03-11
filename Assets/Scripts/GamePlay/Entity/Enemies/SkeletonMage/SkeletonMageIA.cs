using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMageIA : MonoBehaviour
{

    private bool _playerOnTheZone = true;
    public bool attack;

    private Attack _attack;
    private StateManager _stateManager;

    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        _attack = GetComponent<Attack>();
        _stateManager = GetComponent<StateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (_playerOnTheZone)
        {
            if (attack)
            {
                // vector, distance
                print("attack");
                var dir = Player.transform.position - transform.position;
                var distance = Vector3.Distance(transform.position, Player.transform.position);
                _attack.DoAttackWithFireBall(Vector3.zero, distance, _stateManager.EnemyLayer);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        var dir = Player.transform.position - transform.position;
        

        var distance = Vector3.Distance(transform.position, Player.transform.position);



        //var distance = Vector3.Distance(Player.transform.position, transform.position);

        Gizmos.DrawLine(transform.position,   dir * distance);
    }
}
