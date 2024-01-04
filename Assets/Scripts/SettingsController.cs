using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public Toggle musicToggle;
    public AudioClip musicClip;

    public AudioSource musicSource;

    void Start()
    {
        musicSource.clip = musicClip;
        musicToggle.onValueChanged.AddListener(delegate { ToggleMusic(musicToggle); });
    }

    void ToggleMusic(Toggle change)
    {
        Debug.Log(change.name + " was toggled to: " + change.isOn);

        if (musicToggle.isOn)
        {
            musicSource.Play();
        }
        else
        {
            musicSource.Stop();
        }
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}