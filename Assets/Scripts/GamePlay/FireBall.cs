using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float Damage = 2f;
    public float AttackRange = 1f;
    public float HitForce = 1f;

    public float Speed = 1f;

    private Vector3 _dirToMove;

    private void Update()
    {
        transform.position += _dirToMove * Speed * Time.deltaTime;
    }

    public void changeTheDirToMove(Vector3 vec3)
    {
        _dirToMove = vec3;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" || collision.transform.tag == "Enemy")
        {
            collision.gameObject.GetComponent<StateManager>().Attack(Damage);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2( _dirToMove.x * HitForce, 100));
            print("pass");
        }
        //Destroy(gameObject);
    }
}
