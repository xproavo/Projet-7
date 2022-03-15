using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private float _value = 1f;

    private void Awake()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy") && !collision.gameObject.GetComponent<StateManager>().Death)
        {
            collision.gameObject.GetComponent<StateManager>().TakeCoin(_value);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {
            UIManager.Instance.UpdateCoin();
        }
    }
}
