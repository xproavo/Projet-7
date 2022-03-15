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
    public TextMeshProUGUI score;

    private GameObject _player;
    public GameObject GameOverUI;

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
        UpdateCoin();
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
    
    public void OnPlayerDeath(float damage)
    {
        if (_player.gameObject.GetComponent<StateManager>().Death)
            GameOverUI.gameObject.SetActive(true);
    }

    public void UpdateCoin()
    {
        var actuelCoin = _player.GetComponent<StateManager>().Coin.ToString();
        score.text = actuelCoin;
    }
}
