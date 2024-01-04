using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
