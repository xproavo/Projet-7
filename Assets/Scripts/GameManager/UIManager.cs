using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField]
    private Slider _lifePointSlider;

    private GameObject _player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        _player = GameObject.FindGameObjectWithTag("Player");

        InitPlayerLifeBar();
    }

    public void InitPlayerLifeBar()
    {
        _lifePointSlider.maxValue = _player.GetComponent<StateManager>().LifePoint;
        UpdatePlayerLifeBar(0);
    }

    public void UpdatePlayerLifeBar(float damage)
    {
        _lifePointSlider.value = _player.gameObject.GetComponent<StateManager>().LifePoint;
    }
}
