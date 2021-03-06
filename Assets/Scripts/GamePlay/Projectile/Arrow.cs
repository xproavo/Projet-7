using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float Damage = 2f;
    public float HitForceX = 1f;
    public float HitForceY = 100f;


    public bool CanTouchPlayer = true;

    private Vector2 _dirToMove;

    public LayerMask UnOverlapPlayerLayer;

    public void Throw(Vector2 vec2, float force , bool canTouchPlayer)
    {
        CanTouchPlayer = canTouchPlayer;
        _dirToMove = vec2;
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(vec2 * force);
        this.gameObject.transform.right = _dirToMove.normalized;
    }

    private void Update()
    {
        this.gameObject.transform.right = GetComponent<Rigidbody2D>().velocity.normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" && CanTouchPlayer)
        {
            collision.gameObject.GetComponent<StateManager>().Attack(Damage);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(_dirToMove.x * HitForceX, HitForceY));
            Destroy(gameObject);
        }else if(collision.transform.tag == "Enemy")
        {
            return;
        }
        HitForceY = 0;
        HitForceX = 0;
        Damage = 0;
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            if (collision.GetComponent<StateManager>().Death)
                return;

            collision.gameObject.GetComponent<StateManager>().Attack(Damage);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(_dirToMove.x * HitForceX, HitForceY));
            Destroy(gameObject);
        }
    }

    private void UnOverlapPlayer()
    {
        this.gameObject.layer = UnOverlapPlayerLayer;
    }
}
