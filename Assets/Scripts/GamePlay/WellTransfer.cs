using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellTransfer : MonoBehaviour
{
    public GameObject bg1;
    public GameObject bg2;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player")) {
            
            GameObject CaveBegin = this.transform.GetChild(0).gameObject;

            collision.gameObject.GetComponent<Transform>().position = CaveBegin.transform.position;

            // changement du background
            bg1.SetActive(false);
            bg2.SetActive(true);
       }
    }

}
