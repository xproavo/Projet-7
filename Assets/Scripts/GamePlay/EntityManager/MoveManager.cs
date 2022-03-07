using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{

    public LayerMask GroundLayer;
    public LayerMask LadderLayer;


    public float SpeedMove = 5;

    public float JumpForce = 5;
    public bool Jump;
    /*
    public float RollForce = 5;
    public bool Roll;
    public float SecondeWaitingBeforeReRoll = 1;
    private bool _reRoll = true;
    */

    [Range(0.5f, 1f)]
    public float DetectGroundRange = 0.75f;
    public bool IsGrounded = false;

    public bool OnLadder = false;
    public bool Climb= false;

    public Vector3 SpawnPoint = Vector3.zero;

    private Vector3 _move;
    private Rigidbody2D _body2d;
    private SpriteRenderer _spriteRenderer;



    void Awake()
    {
        _body2d = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Movement();
    }

    private void FixedUpdate()
    {
        Vector3 rayCastDir = Vector3.down;
        RaycastHit2D Grounded = Physics2D.Raycast(transform.position, rayCastDir, DetectGroundRange, GroundLayer.value);
        IsGrounded = Grounded.collider != null;
        
        RaycastHit2D Ladder = Physics2D.Raycast(transform.position, rayCastDir, DetectGroundRange, LadderLayer.value);
        OnLadder = Ladder.collider != null;
    }

    public void Movement()
    {
        if (Jump && IsGrounded)
        {
            _body2d.velocity = Vector3.up * JumpForce ;
            Jump = false;
        }
        /*
        if (Roll && IsGrounded && _reRoll)
        {
            if (!_spriteRenderer.flipX)
            {
                _body2d.velocity = Vector3.right * RollForce;
            }
            else
            {
                _body2d.velocity = Vector3.left * RollForce;
            }
            _reRoll = false;
            StartCoroutine("WaitBeforeReRoll");
            print("pass"); 
        }
        else if (Roll)
        {
            Roll = false;
        }
        */
    


        transform.position += _move * SpeedMove * Time.deltaTime;
    }


    /*
    private IEnumerator WaitBeforeReRoll()  
    {
        print("pass2");

        yield return new WaitForSeconds(SecondeWaitingBeforeReRoll);
        _reRoll = true;
        print("pass3");
    }
    */

    public void ClimbOnLadder(float value)
    {
        transform.position += new Vector3(0, value * SpeedMove * Time.deltaTime);
        print("Climb");
    }

    public void NullMoveXValue()
        { _move.x = 0; }
    public void ChangeMoveXValue(float moveX)
        { _move.x = moveX; }
    public float GetMoveXValue()
        { return _move.x; }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.down * DetectGroundRange));
    }
}
