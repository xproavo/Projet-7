using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public float LifePoint = 20f;

    public float Degat = 2f;
    public float AttackRange = 1f;
    public RaycastHit2D isAttackable;
    public float HitForce = 1f;

    public float InvicibilityTime = 30f;
    public bool Invicibility = false;

    public bool Death = false;

    private float _hitDelay = 0f;

    public EntityType entityType;
    public AttackState attackState = AttackState.Check;

    private MoveManager _moveManager;
    private AnimateManager _animateManager;

    private void Awake()
    {
        _moveManager = GetComponent<MoveManager>();
        _animateManager = GetComponent<AnimateManager>();
    }


    private void Update()
    {
        
        if (LifePoint <= 0) // creer un delegate
        {

            Death = true;
        }
        if (Invicibility)
        {
            if (_hitDelay <= 0)
            {
                Invicibility = false;
            }
            _hitDelay -= Time.deltaTime;
        }
    }

    public void Attack(Vector2 dirAttack, float DetectRange, LayerMask layerHit)
    {
        switch (attackState)
        {
            case AttackState.Check:
                isAttackable = Physics2D.Raycast(transform.position, dirAttack, DetectRange, layerHit.value);
                if (isAttackable.collider != null)
                {
                    attackState = AttackState.Prepare;
                }
                break;
            case AttackState.Prepare:
                if (!isAttackable.collider.gameObject.GetComponent<StateManager>().Death && !isAttackable.collider.gameObject.GetComponent<StateManager>().Invicibility)
                {
                    attackState = AttackState.Attack;
                    _animateManager.Attack1();
                    _moveManager.Movable = false;
                }
                else
                    attackState = AttackState.End;
                break;
            case AttackState.Attack:
                attackState = AttackState.End;

                var hitGameObjectState = isAttackable.collider.gameObject.GetComponent<StateManager>();
                hitGameObjectState.LifePoint -= Degat;
                isAttackable.collider.gameObject.GetComponent<Rigidbody2D>().AddForce( new Vector2(dirAttack.x * HitForce, 1));

                hitGameObjectState.Invicibility = true;
                hitGameObjectState._hitDelay = hitGameObjectState.InvicibilityTime;

                break;
            case AttackState.End:
                attackState = AttackState.Check;
                _moveManager.Movable = true;
                break;
        }          
    }

    public enum EntityType
    {
        Player,
        Skeleton
    }

   public enum AttackState
    {
        Check,
        Prepare,
        Attack,
        End
    }
}
