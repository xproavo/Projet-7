using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderTransfer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {

            GameObject VillageBegin = this.transform.GetChild(0).gameObject;

            collision.gameObject.GetComponent<Transform>().position = VillageBegin.transform.position;
        }
    }
}
