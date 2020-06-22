using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuOptionsController : MonoBehaviour {

    private GameManager manager;
    [SerializeField]
    private Button optionsButton;
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private Toggle toggle;
    [SerializeField]
    private AudioSource[] audioSources;
    [SerializeField]
    private Sprite[] tutorialImages;
    [SerializeField]
    private GameObject tutorialUI;
    [SerializeField]
    private GameObject[] tutorialSlides;
    [SerializeField]
    private GameObject[] tutorialButtons;
    [SerializeField]
    public InputField inputField;

    void Start () {
        manager = FindObjectOfType<GameManager>();

        if (PlayerPrefs.GetInt("sound") == 0)
            toggle.isOn = false;

        else
            toggle.isOn = true;

        ToggleAudioSources();
    }

	public void PauseGame()
    {
        manager.PauseGame();
        HideUI();
    }

    public void UnpauseGame()
    {
        manager.UnPauseGame();
        ShowUI();
    }

    public void QuitToMenu()
    {
        manager.UnPauseGame();
        manager.LoadMainMenu();
    }

    public void ToggleSound()
    {
        if (toggle)
        {
            manager.ToggleSound(toggle);
            ToggleAudioSources();
        }
    }

    public void ShowTutorial()
    {
        tutorialUI.SetActive(true);
        tutorialUI.GetComponent<Image>().sprite = tutorialImages[0];
        manager.seenTutorial = false;
        manager.PauseGame();
        manager.SaveGame();
        tutorialSlides[0].SetActive(true);
        tutorialSlides[1].SetActive(false);
        tutorialSlides[2].SetActive(false);
        tutorialButtons[0].SetActive(false);
        tutorialButtons[1].SetActive(true);
        tutorialButtons[2].SetActive(false);
    }

    public void CloseTutorial()
    {
        tutorialUI.SetActive(false);
        manager.seenTutorial = true;
        manager.SaveGame();
        manager.UnPauseGame();
    }

    public void NextTutorial()
    {
        if (tutorialSlides[1].activeInHierarchy)
        {
            tutorialSlides[0].SetActive(false);
            tutorialSlides[1].SetActive(false);
            tutorialSlides[2].SetActive(true);
            tutorialButtons[0].SetActive(true);
            tutorialButtons[1].SetActive(false);
            tutorialButtons[2].SetActive(true);
        }
        if (tutorialSlides[0].activeInHierarchy)
        {
            tutorialUI.GetComponent<Image>().sprite = tutorialImages[1];
            tutorialSlides[0].SetActive(false);
            tutorialSlides[1].SetActive(true);
            tutorialSlides[2].SetActive(false);
            tutorialButtons[0].SetActive(true);
            tutorialButtons[1].SetActive(true);
            tutorialButtons[2].SetActive(false);
        }
    }

    public void PreviousTutorial()
    {
        if(tutorialSlides[1].activeInHierarchy)
        {
            tutorialUI.GetComponent<Image>().sprite = tutorialImages[0];
            tutorialSlides[0].SetActive(true);
            tutorialSlides[1].SetActive(false);
            tutorialSlides[2].SetActive(false);
            tutorialButtons[0].SetActive(false);
            tutorialButtons[1].SetActive(true);
            tutorialButtons[2].SetActive(false);
        }
        if(tutorialSlides[2].activeInHierarchy)
        {
            tutorialSlides[0].SetActive(false);
            tutorialSlides[1].SetActive(true);
            tutorialSlides[2].SetActive(false);
            tutorialButtons[0].SetActive(true);
            tutorialButtons[1].SetActive(true);
            tutorialButtons[2].SetActive(false);
        }
    }

    private void ToggleAudioSources()
    {
        foreach (AudioSource source in audioSources)
        {
            source.mute = !toggle.isOn;
        }
    }

    private void HideUI()
    {
        optionsButton.gameObject.SetActive(false);
        panel.SetActive(true);
    }

    private void ShowUI()
    {
        optionsButton.gameObject.SetActive(true);
        panel.SetActive(false);
    }
}
