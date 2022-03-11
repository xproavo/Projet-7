using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public static GameOverManager instance;
   

    public void RetryButton()
    {
        // Recommencer le niveau
        // Recharger la scéne
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // Replace le joueur au spawn (redonner la vie etc)
        //Game Over UI off
        gameOverUI.SetActive(false);
    }
    public void MainMenuButton(string SceneToLoad)
    {
        SceneManager.LoadScene(SceneToLoad);
    }

    public void ExitButton()
    {
        // Quitte le jeu
        Application.Quit();
    }
}


//    FONCTION NON UTILISER!!!!!