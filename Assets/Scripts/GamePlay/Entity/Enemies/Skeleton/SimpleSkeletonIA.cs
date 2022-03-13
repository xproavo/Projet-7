using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSkeletonIA : MonoBehaviour
{

    public int MaxCoin = 2;

    public Vector2 WaitingAfterReturnMinAndMaxSecond = new Vector2(3, 10);

    public  Vector3 DirMove;
    private bool _focusEnemy = false;

    private SpriteRenderer _spriteRenderer;
    private MoveManager _moveManager;
    private StateManager _stateManager;
    private Attack _attack;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _moveManager = GetComponent<MoveManager>();
        _stateManager = GetComponent<StateManager>();
        _attack = GetComponent<Attack>();



        _stateManager.Coin = Random.Range(0, MaxCoin + 1);

        InitCoroutine();
    }

    private void Start()
    {
        _stateManager.OnTakeDamage += StopAllCoroutine;

    }

    private void Update()
    {
        if (!_stateManager.Death)
        {
            _moveManager.NullMoveXValue();
            if (_focusEnemy)
            {
                DirMove.Normalize();
                _moveManager.ChangeMoveXValue(DirMove.x);

                if (!_stateManager.Invicibility)
                    _attack.DoAttackWithMeleeWeapon(_moveManager.DirectionSee(), _stateManager.AttackRange, _stateManager.EnemyLayer);
            }
        }
        if (GameManager.Instance.CurrentTimeOfDay == GameManager.TimeOfDay.Night && _stateManager.Death && _stateManager.LifePoint > 0)
        {
            _stateManager.Death = false;
            InitCoroutine();
        }
        else if (GameManager.Instance.CurrentTimeOfDay == GameManager.TimeOfDay.Day && !_stateManager.Death)
        {
            _stateManager.Death = true;
            StopAllCoroutine(0);
        }
    }


    private void InitCoroutine()
    {
        StartCoroutine(Waiting());
    }

    public void StopAllCoroutine(float damage)
    {
        if (_stateManager.Death)
            StopAllCoroutines();
    }
 
    private IEnumerator Waiting()
    {
        while (true)
        {
            _spriteRenderer.flipX = !_spriteRenderer.flipX;

            yield return new WaitForSeconds(Random.Range(WaitingAfterReturnMinAndMaxSecond.x, WaitingAfterReturnMinAndMaxSecond.y));
        }
    }

    public void EndFocusEnemy()
    {
        StartCoroutine(Waiting());
        _focusEnemy = false;
    }
    public void OnFocusEnemy()
    {
        StopCoroutine(Waiting());
        _focusEnemy = true;
    }

}
