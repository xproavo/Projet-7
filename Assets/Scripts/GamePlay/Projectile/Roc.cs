using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roc : MonoBehaviour
{
    public float TimeBeforsDestroy = 5f;
    public float Damage = 4f;
    public Vector2 HitForce = Vector2.up;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<StateManager>().Attack(Damage);
            collision.GetComponent<Rigidbody2D>().AddForce(HitForce);
            this.GetComponent<Collider2D>().enabled = false;
            Damage = 0;
            HitForce = Vector2.zero;
        }
    }

    public void Init()
    {
        Destroy(gameObject, TimeBeforsDestroy);
    }
}
