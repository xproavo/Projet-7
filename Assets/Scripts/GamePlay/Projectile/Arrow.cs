using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float Damage = 2f;
    public float HitForce = 1f;


    public bool CanTouchPlayer = true;

    private Vector2 _dirToMove;

    public LayerMask UnOverlapPlayerLayer;

    public void Throw(Vector2 vec2, float force , bool canTouchPlayer)
    {
        CanTouchPlayer = canTouchPlayer;
        _dirToMove = vec2;
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(vec2 * force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" && CanTouchPlayer)
        {
            collision.gameObject.GetComponent<StateManager>().Attack(Damage);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(_dirToMove.x * HitForce, 100));
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            if (collision.GetComponent<StateManager>().Death)
                return;

            collision.gameObject.GetComponent<StateManager>().Attack(Damage);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(_dirToMove.x * HitForce, 100));
            Destroy(gameObject);
        }
    }

    private void UnOverlapPlayer()
    {
        this.gameObject.layer = UnOverlapPlayerLayer;
    }
}
