using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMenu : MonoBehaviour
{
    public static bool inNPC;
    public GameObject NPCmenu;
    public static bool trigger = false;

    private GameObject _player;
    public GameObject _interaction;
    // Start is called before the first frame update
    void Start()
    {
        NPCmenu.SetActive(false);
    }

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
          if ((Input.GetKeyDown(KeyCode.E)) && trigger)
          {
            if (inNPC)
            {
                ResumeGame();
                _player.GetComponent<InputManager>().enabled = true;
            }
            else
            {
                NPCpause();
                _player.GetComponent<InputManager>().enabled = false;
                _player.GetComponent<MoveManager>().ChangeMoveXValue(0);
            }
          }
    }

    public void ResumeGame()
    {
        NPCmenu.SetActive(false);
        inNPC = false;
    }

    public void NPCpause()
    {
        NPCmenu.SetActive(true);
        inNPC = true;
    }

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
