using UnityEngine;
using UnityEngine.SceneManagement;

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
   

    public void RetryButton()
    {
        // Recommencer le niveau
        // Recharger la scéne
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // Replace le joueur au spawn (redonner la vie etc)
        //Game Over UI off
        gameOverUI.SetActive(false);
    }
    public void MainMenuButton()
    {
        // Retour menu principale
    }

    public void ExitButton()
    {
        // Quitte le jeu
        Application.Quit();
    }
}


//    FONCTION NON UTILISER!!!!!