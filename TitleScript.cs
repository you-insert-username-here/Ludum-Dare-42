using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class TitleScript : MonoBehaviour 
{
    public Slider musicSlider, soundSlider;
    public Button play, quit;
    public Toggle music, sound;

    public AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        Button playButton = play.GetComponent<Button>();
        Button quitButton = quit.GetComponent<Button>();

        Slider musicSlide = musicSlider.GetComponent<Slider>();
        Slider soundSlide = soundSlider.GetComponent<Slider>();

        Toggle musicToggle = music.GetComponent<Toggle>();
        Toggle soundoggle = sound.GetComponent<Toggle>();

        PlayerPrefs.SetFloat("Music Volume", 0.0f);
        PlayerPrefs.SetFloat("Sound Volume", 0.0f);

        PlayerPrefs.SetInt("Music Enabled", 0);
        PlayerPrefs.SetInt("Sound Enabled", 0);
    }

    private void Update()
    {
        PlayerPrefs.SetFloat("Music Volume", musicSlider.value);
        PlayerPrefs.SetFloat("Sound Volume", soundSlider.value);

        if (music.isOn)
        {
            audioSource.volume = musicSlider.value;
            PlayerPrefs.SetInt("Music Enabled", 1);
        }
        else
        {
            audioSource.volume = 0;
            PlayerPrefs.SetInt("Music Enabled", 0);
        }

        if (sound.isOn)
            PlayerPrefs.SetInt("Sound Enabled", 1);
        else PlayerPrefs.SetInt("Sound Enabled", 0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

}