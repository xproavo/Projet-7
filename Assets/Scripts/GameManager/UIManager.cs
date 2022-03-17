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
    public TextMeshProUGUI marchandprix1;
    public TextMeshProUGUI marchandprix2;
    public TextMeshProUGUI marchandprix3;
    public TextMeshProUGUI forgeronprix1;
    public TextMeshProUGUI forgeronprix2;
    public TextMeshProUGUI forgeronprix3;

    private GameObject _player;
    public GameObject _potion;
    public GameObject _forgeron;
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
        UpdatePotion();
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
    public void UpdatePotion()
    {
        var prixpotion = _potion.GetComponent<NPCMenu>().PrixPotion.ToString();
        var forgeronprix = _forgeron.GetComponent<ForgeronMenu>().ForgeronPrix.ToString();
        marchandprix1.text = prixpotion;
        marchandprix2.text = prixpotion;
        marchandprix3.text = prixpotion;
        forgeronprix1.text = forgeronprix;
        forgeronprix2.text = forgeronprix;
        forgeronprix3.text = forgeronprix;
    }
}
