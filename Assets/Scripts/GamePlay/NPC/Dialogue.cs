using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{

    [SerializeField] public GameObject Colar;
    public GameObject nextText;
    public GameObject textBefore;
    public static bool trigger = false;
    public GameObject _statemanager;

    public void Continue()
    {
        nextText.SetActive(true);
        textBefore.SetActive(false);
    }
    public void Continue2()
    {
        nextText.SetActive(false);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Colar.SetActive(true);

            trigger = true;
            textBefore.SetActive(true);

            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        trigger = false;
        textBefore.SetActive(false);
        nextText.SetActive(false);
    }
    public void setBool()
    {
        var test = _statemanager.GetComponent<StateManager>().soup;
        test = true;
    }
}
