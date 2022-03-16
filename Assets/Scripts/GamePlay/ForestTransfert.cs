using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestTransfert : MonoBehaviour
{
        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.gameObject.CompareTag("Player"))
            {

                GameObject DungeonBegin = this.transform.GetChild(0).gameObject;

                collision.gameObject.GetComponent<Transform>().position = DungeonBegin.transform.position;
            }
        }
    }

