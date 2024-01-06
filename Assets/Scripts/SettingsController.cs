using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public Toggle musicToggle;
    public Slider volumeSlider;

    void Start()
    {
        musicToggle.onValueChanged.AddListener(delegate { ToggleMusic(musicToggle); });
        volumeSlider.onValueChanged.AddListener(delegate { ChangeVolume(volumeSlider); });

        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1);
        musicToggle.isOn = PlayerPrefs.GetInt("Music", 1) == 1;
    }

    void ToggleMusic(Toggle change)
    {
        PlayerPrefs.SetInt("Music", change.isOn ? 1 : 0);
    }

    void ChangeVolume(Slider slider)
    {
        PlayerPrefs.SetFloat("Volume", slider.value);
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}