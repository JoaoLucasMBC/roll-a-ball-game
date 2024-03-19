using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hpUpAudioClip;
    [SerializeField] private AudioClip runeAudioClip;
    [SerializeField] private AudioClip damageAudioClip;
    [SerializeField] private AudioClip timeUpAudioClip;
    [SerializeField] private AudioClip jumpAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayHPUp() {
        audioSource.PlayOneShot(hpUpAudioClip);
    }

    public void PlayRune() {
        audioSource.PlayOneShot(runeAudioClip);
    }

    public void PlayDamage() {
        audioSource.PlayOneShot(damageAudioClip);
    }

    public void PlayTimeUp() {
        audioSource.PlayOneShot(timeUpAudioClip, 3f);
    }

    public void PlayJump() {
        audioSource.PlayOneShot(jumpAudioClip);
    }
}