using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectZoneSkeletonMage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            transform.GetChild(0).gameObject.GetComponent<SkeletonMageIA>().InZone(true);
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            transform.GetChild(0).gameObject.GetComponent<SkeletonMageIA>().InZone(false);
    }
}
