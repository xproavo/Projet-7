using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBow : MonoBehaviour
{
    public Vector2 ShootDirection;

    public Vector3 AjustArrowSpawnPos;

    [SerializeField]
    private GameObject ArrowPrefab;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private GameObject _player;

    private bool canPick = false;
    private bool focus = false;
    private bool canAttack = true;

    private AttackState _attackState;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator.SetTrigger("Quit");
    }

    private void Update()
    {
        if (canPick)
        {
            if (Input.GetKeyDown("e"))
            {
                GetFocus();
            }
        }
        if (focus)
        {
            if (Input.GetKey("a") && canAttack)
            {
                _animator.SetTrigger("Attack");
                canAttack = false;

            }
        }
        if (ShootDirection.x > 0)
            this.GetComponent<SpriteRenderer>().flipX = true;
        else
            this.GetComponent<SpriteRenderer>().flipX = false;

    }


    public void Attack()
    {
        GameObject clone = GameObject.Instantiate(ArrowPrefab, transform.position + AjustArrowSpawnPos, Quaternion.identity);
        clone.gameObject.GetComponent<Arrow>().Throw(ShootDirection.normalized);
        canAttack = true;
    }






    private void GetFocus()
    {
        focus = !focus;
        if (focus)
        {
            _player.GetComponent<InputManager>().enabled = false;
            _player.GetComponent<MoveManager>().Movable = false;
        }
        else
        {
            _player.GetComponent<InputManager>().enabled = true;
            _player.GetComponent<MoveManager>().Movable = true;
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _animator.SetTrigger("Enter");
            canPick = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _animator.SetTrigger("Quit");
            canPick = false;
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
