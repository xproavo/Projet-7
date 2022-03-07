using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<MoveManager>().SpawnPoint = transform.position;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
