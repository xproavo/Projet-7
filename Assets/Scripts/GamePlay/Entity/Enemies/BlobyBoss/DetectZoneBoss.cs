using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectZoneBoss : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponentInParent<Bloby>().EnemyInZone(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponentInParent<Bloby>().EnemyInZone(false);
        }
    }
}
