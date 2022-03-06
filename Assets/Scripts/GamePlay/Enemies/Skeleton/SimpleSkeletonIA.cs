using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSkeletonIA : MonoBehaviour
{
    public LayerMask PlayerLayer;
    public float DetectEnemyRange = 5f;

    private Vector3 _dirSee;

    private bool _focusEnemy = false;
    private bool _waitCorotineStart = false;


    private SpriteRenderer _spriteRenderer;
    private MoveManager _moveManager;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _moveManager = GetComponent<MoveManager>();
    }

    private void Update()
    {
        _moveManager.NullMoveXValue();
        if (_focusEnemy)
        {
            _moveManager.ChangeMoveXValue(_dirSee.x);
        }
    }

    private void FixedUpdate()
    {
        RaycastHit2D Detect = Physics2D.Raycast(transform.position, _dirSee, DetectEnemyRange, PlayerLayer.value);
        _focusEnemy = Detect.collider != null;

        if (!_focusEnemy && !_waitCorotineStart)
        {
            StartCoroutine("Waiting");
            _waitCorotineStart = true;
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



            if (_focusEnemy)
            {
                StopCoroutine("Waiting");
                _waitCorotineStart = false;
            }


            yield return new WaitForSeconds(3);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        var _dir = Vector3.right;
        if (_spriteRenderer.flipX)
            _dir = Vector3.left;
        Gizmos.DrawLine(transform.position, transform.position + _dir * DetectEnemyRange );
    }

}
