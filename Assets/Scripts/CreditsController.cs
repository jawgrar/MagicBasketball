using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
    public void OpenMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}