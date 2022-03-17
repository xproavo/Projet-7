using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactionE : MonoBehaviour
{
    public static bool trigger = false;
    public GameObject _interaction;
    public GameObject _shoot;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            trigger = true;
            _interaction.SetActive(true);
            _shoot.SetActive(true);
            GetComponent<Interactible>().IsInteratible();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        trigger = false;
        _interaction.SetActive(false);
        _shoot.SetActive(false);
        GetComponent<Interactible>().IsNotInteratible();
    }
}
