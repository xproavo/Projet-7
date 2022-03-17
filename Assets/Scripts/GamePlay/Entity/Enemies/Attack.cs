using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public AttackState attackState = AttackState.Check;

    private RaycastHit2D _isAttackable;
    private RaycastHit2D _haveObstacle;

    public GameObject FireBallPrefab;

    private AnimateManager _animateManager;
    private StateManager _stateManager;
    private MoveManager _moveManager;

    private void Awake()
    {
        _animateManager = GetComponent<AnimateManager>();
        _stateManager = GetComponent<StateManager>();
        _moveManager = GetComponent<MoveManager>();

    }

    private void Start()
    {
        _stateManager.OnTakeDamage += NotMove;

    }

    public void DoAttackWithMeleeWeapon(Vector2 dirAttack, float DetectRange, LayerMask layerHit)
    {
        switch (attackState)
        {
            case AttackState.Check:
                _isAttackable = Physics2D.Raycast(transform.position, dirAttack, DetectRange, layerHit.value);
                if (_isAttackable.collider != null )
                {
                    attackState = AttackState.Prepare;
                }
                break;
            case AttackState.Prepare:
                if (!_isAttackable.collider.gameObject.GetComponent<StateManager>().Death && !_isAttackable.collider.gameObject.GetComponent<StateManager>().Invicibility)
                {
                    attackState = AttackState.Attack;
                    _animateManager.Attack("Attack1");
                }
                else
                    attackState = AttackState.End;
                break;
            case AttackState.Attack:
                if (_animateManager.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5)
                {
                    attackState = AttackState.End;

                    var hitGameObjectState = _isAttackable.collider.gameObject.GetComponent<StateManager>();
                    hitGameObjectState.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(dirAttack.x * _stateManager.HitForce, 100));

                    hitGameObjectState.gameObject.GetComponent<StateManager>().Attack(_stateManager.Damage);
                }
                break;
            case AttackState.End:
                attackState = AttackState.Check;
                break;
        }
    }


    public void DoAttackWithFireBall(Vector2 dirAttack, float DetectRange, LayerMask layerHit)
    {
        if (dirAttack.x > 0)
            this.GetComponent<SpriteRenderer>().flipX = false;
        else
            this.GetComponent<SpriteRenderer>().flipX = true;


        switch (attackState)
        {
            case AttackState.Check:
                _isAttackable = Physics2D.Raycast(transform.position, dirAttack, DetectRange, layerHit.value);
                _haveObstacle = Physics2D.Raycast(transform.position, dirAttack, DetectRange, _moveManager.GroundLayer.value);
                if (_isAttackable.collider != null && _haveObstacle.collider == null)
                {
                    attackState = AttackState.Prepare;
                }
                break;
            case AttackState.Prepare:

                if (!_isAttackable.collider.gameObject.GetComponent<StateManager>().Death && !_isAttackable.collider.gameObject.GetComponent<StateManager>().Invicibility)
                {
                    attackState = AttackState.Attack;
                    _animateManager.Attack("Attack1");
                }
                else
                    attackState = AttackState.End;
                break;
            case AttackState.Attack:


                if (_animateManager.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5)
                {
                    attackState = AttackState.End;
                    var _dirSee = new Vector3(_moveManager.DirectionSee().x, _moveManager.DirectionSee().y);
                    GameObject clone = GameObject.Instantiate(FireBallPrefab, transform.position + _dirSee, Quaternion.identity);
                    clone.gameObject.GetComponent<FireBall>().ChangeTheDirToMove(dirAttack.normalized);
                }

                break;
            case AttackState.End:

                attackState = AttackState.Check;
                break;
        }

    }


    public void NotMove(float damage)
    {
        StartCoroutine(NotMoveCoroutine());
    }

    private IEnumerator NotMoveCoroutine()
    {
        _moveManager.Movable = false;
        this.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(_stateManager.InvicibilityTime - 1);
        this.GetComponent<Collider2D>().enabled = true;
        _moveManager.Movable = true;    
    }

    public enum AttackState
    {
        Check,
        Prepare,
        Attack,
        End
    }
}
