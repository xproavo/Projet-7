using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageScript : MonoBehaviour
{
    public GameObject[] WayPoints;
    public float TimeBeforeTeleport = 5;

    private IEnumerator TeleportMage() { 
        while( true)
        {
            gameObject.transform.position = WayPoints[Random.Range(0, WayPoints.Length - 1)].transform.position;
            yield return new WaitForSeconds(TimeBeforeTeleport);
        }

    }
    private void Start()
    {
        StartCoroutine(TeleportMage());

    }


}
