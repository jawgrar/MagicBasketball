using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanelController : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
