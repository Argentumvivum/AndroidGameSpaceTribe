using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuOptionsController : MonoBehaviour {

    private GameManager manager;

    [SerializeField]
    private Toggle toggle;
    [SerializeField]
    private AudioSource audioSource;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();

        if (PlayerPrefs.GetInt("sound") == 0)
            toggle.isOn = false;
        else
            toggle.isOn = true;

        ToggleAudioSources();
    }

    public void StartGame()
    {
        manager.StartGame();
    }

    public void QuitGame()
    {
        manager.QuitGame();
    }

    public void ToggleSound()
    {
        if (toggle)
        {
            manager.ToggleSound(toggle);
            ToggleAudioSources();
        }
    }

    private void ToggleAudioSources()
    {
        audioSource.mute = !toggle.isOn;
    }
}
