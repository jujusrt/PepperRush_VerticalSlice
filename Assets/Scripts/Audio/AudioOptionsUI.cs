using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioOptionsUI : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    const string MUSIC_PARAM = "MusicV";
    const string SFX_PARAM = "SFXV";

    void Start()
    {
        bool isMainMenu = SceneManager.GetActiveScene().name == "OptionsScene";

        float value;

        if (mixer.GetFloat(MUSIC_PARAM, out value))
        {
            musicSlider.value = value;
        }

        if (!isMainMenu)
        {
            if (mixer.GetFloat(SFX_PARAM, out value))
            {
                sfxSlider.value = value;
            }

            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }

        musicSlider.onValueChanged.AddListener(SetMusicVolume);
    }

    public void SetMusicVolume(float value)
    {
        mixer.SetFloat(MUSIC_PARAM, value);
    }

    public void SetSFXVolume(float value)
    {
        mixer.SetFloat(SFX_PARAM, value);
    }
}