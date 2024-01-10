using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public Toggle musicToggle;
    public Slider volumeSlider;
    public TMP_Dropdown ballsDropdown;

    void Start()
    {
        musicToggle.onValueChanged.AddListener(delegate { ToggleMusic(musicToggle); });
        volumeSlider.onValueChanged.AddListener(delegate { ChangeVolume(volumeSlider); });
        ballsDropdown.onValueChanged.AddListener(delegate { ChangeBall(ballsDropdown); });

        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1);
        musicToggle.isOn = PlayerPrefs.GetInt("Music", 1) == 1;
        ballsDropdown.value = PlayerPrefs.GetString("Ball", "Ball1") == "Ball1" ? 0 : 1;
    }

    void ChangeBall(TMP_Dropdown change)
    {
        PlayerPrefs.SetString("Ball", change.options[change.value].text);
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