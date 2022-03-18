using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouletDialogue : MonoBehaviour
{
    public static bool trigger = false;
    public GameObject _interaction;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            trigger = true;
            _interaction.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        trigger = false;
        _interaction.SetActive(false);
    }
}
