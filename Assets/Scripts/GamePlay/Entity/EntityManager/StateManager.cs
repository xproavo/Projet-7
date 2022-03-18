using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public float LifePoint = 20f;
    public float Coin = 0f;

    public bool hit = false;
    public bool soup = false;

    public float Damage = 2f;
    public float AttackRange = 1f;
    public float HitForce = 1f;

    public float InvicibilityTime = 30f;
    public bool Invicibility = false;

    public bool Death = false;

    public LayerMask EnemyLayer;

    private MoveManager _moveManager;
    private AnimateManager _animateManager;
    private StateManager _statemanager;

    public delegate void HitDelegate(float damage);
    public event HitDelegate OnTakeDamage;


    private void Awake()
    {
        _moveManager = GetComponent<MoveManager>();
        _animateManager = GetComponent<AnimateManager>();
        _statemanager = GetComponent<StateManager>();
        OnTakeDamage += Hit;

    }

    private void Start()
    {
        OnTakeDamage += isDeath;

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
            Coin = 0;
        }
    }

    public void Attack(float damage)
    {
        OnTakeDamage?.Invoke(damage);
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
            return;
        }
    }


    public void isDeath(float damage)
    {
        if (Death)
        {
            StopAllCoroutines();
            StartCoroutine(WaitToTouchGround());
        }
    }

    private IEnumerator WaitToTouchGround()
    {
        bool _continu = true;
        bool wait = false;
        while (_continu)
        {
            if (this.GetComponent<Rigidbody2D>().velocity == Vector2.zero && wait)
            {
                this.GetComponent<Rigidbody2D>().gravityScale = 0;
                this.GetComponent<Collider2D>().enabled = false;
                if (this.transform.childCount > 0)
                {
                    for (int i = 0; i < this.transform.childCount; i++)
                    {
                        var child = transform.GetChild(i);
                        child.GetComponent<Collider2D>().enabled = false;
                    }
                }
                _continu = false;
            }
            yield return new WaitForSeconds(0.5f);
            wait = true;
        }
    }
}
