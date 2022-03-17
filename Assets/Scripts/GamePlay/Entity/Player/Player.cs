using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public LayerMask EnemyLayer;

    public float AttackCouldown;

    private bool _attackable = true;

    private StateManager _stateManager;
    private AnimateManager _animateManager;
    private MoveManager _moveManager;

    void Start()
    {
        _stateManager = GetComponent<StateManager>();
        _animateManager = GetComponent<AnimateManager>();
        _moveManager = GetComponent<MoveManager>();

        _stateManager.OnTakeDamage += UIManager.Instance.UpdatePlayerLifeBar;
        _stateManager.OnTakeDamage += UIManager.Instance.OnPlayerDeath;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.CurrentTimeOfDay == GameManager.TimeOfDay.Night && !_stateManager.Death)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (GameManager.Instance.CurrentTimeOfDay == GameManager.TimeOfDay.Day && !_stateManager.Death)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

    }

    public void Attack()
    {
        if (_attackable)
        {

            var whatAttackRandom = Random.Range(0, 2);
            print(whatAttackRandom);
            if (whatAttackRandom == 0)
                _animateManager.Attack("Attack1");
            else
                _animateManager.Attack("Attack2");


            RaycastHit2D hit = Physics2D.Raycast(transform.position, _moveManager.DirectionSee(), _stateManager.AttackRange, _stateManager.EnemyLayer.value);

            if (hit.collider != null)
            {
                if (!hit.collider.gameObject.GetComponent<StateManager>().Death && !hit.collider.gameObject.GetComponent<StateManager>().Invicibility)
                {
                    var hitGameObjectState = hit.collider.gameObject.GetComponent<StateManager>();
                    hitGameObjectState.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(_moveManager.DirectionSee().x * _stateManager.HitForce, 100));

                    hitGameObjectState.gameObject.GetComponent<StateManager>().Attack(_stateManager.Damage);

                }

            }

            StartCoroutine(AttackCouldownCoroutine());

        }
    }


    private IEnumerator AttackCouldownCoroutine()
    {
        _attackable = false;
        yield return new WaitForSeconds(AttackCouldown);
        _attackable = true;
    }
}
