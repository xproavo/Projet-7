using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSkeletonIA : MonoBehaviour
{

    public int MaxCoin = 2;

    public Vector2 WaitingAfterReturnMinAndMaxSecond = new Vector2(3, 10);

    private Vector3 _dirMove;
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
                _dirMove.Normalize();
                _moveManager.ChangeMoveXValue(_dirMove.x);

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.CompareTag("Player") && !_stateManager.Death)
        {
            StopCoroutine(Waiting());
            _focusEnemy = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null && !_stateManager.Death)
        {
            if (collision.transform.CompareTag("Player"))
            {

                if (collision.GetComponent<StateManager>().Death)
                {
                    _focusEnemy = false;
                }
                _dirMove =  collision.transform.position - transform.position;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.transform.CompareTag("Player") && !_stateManager.Death)
        {
            StartCoroutine(Waiting());
            _dirMove = Vector3.zero;
        }
    }
}
