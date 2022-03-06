using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{

    public LayerMask GroundLayer;


    public float SpeedMove = 5;

    public float JumpForce = 5;
    public bool Jump;


    [Range(0.5f, 1f)]
    public float DetectGroundRange = 0.75f;
    public bool IsGrounded = false;


    private Vector3 _move;
    private Rigidbody2D _body2d;




    void Awake()
    {
        _body2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Movement();
    }

    private void FixedUpdate()
    {
        Vector3 rayCastDir = Vector3.down;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayCastDir, DetectGroundRange, GroundLayer.value);
        IsGrounded = hit.collider != null;
    }

    public void Movement()
    {
        if (Jump && IsGrounded)
        {
            _body2d.velocity = Vector3.up * JumpForce ;
            Jump = false;
        }
        transform.position += _move * SpeedMove * Time.deltaTime;
    }

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
