using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectZone : MonoBehaviour
{
    public LayerMask EnemyLayer;
    public bool InZone = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == EnemyLayer)
        {
            InZone = true;
        }
    }
}
