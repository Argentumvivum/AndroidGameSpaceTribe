using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingController : MonoBehaviour {

    private AudioSource audioSource;
    private GameManager manager;
    public SpriteRenderer spriteRenderer;
    public AudioClip audioClip;
    public float volume;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        manager = FindObjectOfType<GameManager>();
    }

    public void PlaySplashSound()
    {
        audioSource.PlayOneShot(audioClip, volume);
    }

    public void HidePlayer()
    {
        spriteRenderer.enabled = false;
    }

    public void GoToMenu()
    {
        manager.LoadMainMenu();
    }
}
