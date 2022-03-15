using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBow : MonoBehaviour
{
    public float ThrowForce = 10f;

    public Vector2 ShootDirection;

    public Vector3 AjustArrowSpawnPos;

    public Transform AimPoint;

    [SerializeField]
    private GameObject ArrowPrefab;


    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private GameObject _player;

    private bool canPick = false;
    private bool focus = false;
    private bool canAttack = true;

    private AttackState _attackState;

    private bool notTouchPlayer;

    private Vector3 _scale;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator.SetTrigger("Quit");
        _scale = transform.localScale;
        AimPoint.gameObject.SetActive(false);
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


            if (Input.GetAxis("Horizontal") > 0)
            {
                transform.localScale = new Vector3(-_scale.x, _scale.y, _scale.z);
            }else if (Input.GetAxis("Horizontal") < 0)
            {
                transform.localScale = _scale;
            }


            ShootDirection.x = AimPoint.position.x - (transform.position.x + AjustArrowSpawnPos.x);
            ShootDirection.y = AimPoint.position.y - (transform.position.y + AjustArrowSpawnPos.y);
        }

    }


    public void Attack()
    {
        GameObject clone = GameObject.Instantiate(ArrowPrefab, transform.position + AjustArrowSpawnPos, Quaternion.identity);
        clone.gameObject.GetComponent<Arrow>().Throw(ShootDirection, ThrowForce, false);
        canAttack = true;
    }






    private void GetFocus()
    {
        focus = !focus;
        if (focus)
        {
            _player.GetComponent<InputManager>().enabled = false;
            _player.GetComponent<MoveManager>().Movable = false;
            AimPoint.gameObject.SetActive(true);
        }
        else
        {
            _player.GetComponent<InputManager>().enabled = true;
            _player.GetComponent<MoveManager>().Movable = true;
            AimPoint.gameObject.SetActive(false);
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
