using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public static GameOverManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("il y a plus d'une instance");
            return;
        }
    }
    public void OnPlayerDeath()
    {
        gameOverUI.SetActive(true);

    }
}
