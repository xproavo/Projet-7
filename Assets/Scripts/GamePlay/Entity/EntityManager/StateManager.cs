using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public float LifePoint = 20f;
    public float Coin = 0f;

    public bool hit = false;

    public float Damage = 2f;
    public float AttackRange = 1f;
    public RaycastHit2D isAttackable;
    public float HitForce = 1f;

    public float InvicibilityTime = 30f;
    public bool Invicibility = false;

    public bool Death = false;


    public AttackState attackState = AttackState.Check;

    private MoveManager _moveManager;
    private AnimateManager _animateManager;

    public delegate void HitDelegate(float damage);
    public event HitDelegate OnTakeDamage;


    private void Awake()
    {
        _moveManager = GetComponent<MoveManager>();
        _animateManager = GetComponent<AnimateManager>();
    }

    private void Start()
    {
        OnTakeDamage += Hit;
    }


    private void Update()
    {
        if (hit)
        {
            hit = false;
            this.OnTakeDamage?.Invoke(5);
        }
    }


    public void TakeCoin(float value)
    {
        Coin += value;
    }

    public void ThrowCoin()
    {
        if (Coin > 0)
        {
            for (int i = 0; i < Coin; i++)
            {
                //GameManager.Instance.CoinPrefab;
                GameObject clone = GameObject.Instantiate(GameManager.Instance.CoinPrefab, transform.position + Vector3.up * 1,  Quaternion.identity);
                var randX = Random.Range(-250, 250);
                var randY = Random.Range(100, 300);
                clone.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(randX, randY));
            }
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
                }
                else
                    attackState = AttackState.End;
                break;
            case AttackState.Attack:


                if (_animateManager.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5)
                {
                    attackState = AttackState.End;

                    var hitGameObjectState = isAttackable.collider.gameObject.GetComponent<StateManager>();
                    hitGameObjectState.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(dirAttack.x * HitForce , 100)); // marche pas

                    hitGameObjectState.gameObject.GetComponent<StateManager>().OnTakeDamage?.Invoke(Damage);
                }

                break;
            case AttackState.End:
                attackState = AttackState.Check;
                break;
        }          
    }


    private IEnumerator InvicibilityCoroutine()
    {
        Invicibility = true;
        yield return new WaitForSeconds(InvicibilityTime);
        Invicibility = false;
    }


    public void Hit(float damage)

    {
        LifePoint -= damage;
        StartCoroutine(InvicibilityCoroutine());
        if (LifePoint <= 0)
        {
            _moveManager.ChangeMoveXValue(0);
            Death = true;
            ThrowCoin();
        }
    } 



    public enum AttackState
    {
        Check,
        Prepare,
        Attack,
        End
    }
}
