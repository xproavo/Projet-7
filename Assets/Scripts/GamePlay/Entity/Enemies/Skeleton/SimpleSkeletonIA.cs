using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSkeletonIA : MonoBehaviour
{
    public LayerMask PlayerLayer;
    public float DetectEnemyRange = 5f;

    public float AttackRange = 1f;

    private Vector3 _dirSee;
    private Vector3 _dirMove;

    public bool _focusEnemy = false;


    private SpriteRenderer _spriteRenderer;
    private MoveManager _moveManager;
    private StateManager _stateManager;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _moveManager = GetComponent<MoveManager>();
        _stateManager = GetComponent<StateManager>();

        StartCoroutine("Waiting");
    }

    private void Update()
    {
        _moveManager.NullMoveXValue();
        if (_focusEnemy)
        {
            _dirMove.Normalize();
            _moveManager.ChangeMoveXValue(_dirMove.x);
            _stateManager.Attack(_dirSee, AttackRange, PlayerLayer);
        }

        if (GameManager.Instance.CurrentTimeOfDay == GameManager.TimeOfDay.Night)
        {
            _stateManager.Death = false;
        }
        else
        {
            _stateManager.Death = true;
        }
    }

    private IEnumerator Waiting()
    {
        while (true)
        {
            _spriteRenderer.flipX = !_spriteRenderer.flipX;

            if (_spriteRenderer.flipX)
                _dirSee = Vector3.left;
            else
                _dirSee = Vector3.right;
            yield return new WaitForSeconds(3);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.CompareTag("Player"))
        {
            StopCoroutine("Waiting");
            _focusEnemy = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null)
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
        if (collision.gameObject.transform.CompareTag("Player"))
        {
            StartCoroutine("Waiting");
            _dirMove = Vector3.zero;
        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        var _dir = Vector3.right;
        if(_spriteRenderer != null)
        {
            if (_spriteRenderer.flipX)
                _dir = Vector3.left;
        }
        Gizmos.DrawLine(transform.position, transform.position + _dir * DetectEnemyRange );
    }  
}
