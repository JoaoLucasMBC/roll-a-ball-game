using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI totalTimeText;
    [SerializeField] private AudioClip loseAudioClip;
    [SerializeField] private AudioClip winAudioClip;
    private AudioSource audioSource;

    public void Start() {

        // Unlocka o cursor
        Cursor.lockState = CursorLockMode.None;

        audioSource = GetComponent<AudioSource>();

        // Pega as variáveis do PlayerPrefs
        int score = PlayerPrefs.GetInt("Score");
        float totalTime = PlayerPrefs.GetFloat("Time");
        int win = PlayerPrefs.GetInt("Win");

        // Ajusta os textos
        scoreText.text = "You Collected " + score.ToString() + " Runes";
        totalTimeText.text = "Total Time: " + totalTime.ToString("F2") + "s";

        // Pausa a música para o SFX de final de jogo
        GameObject music = GameObject.Find("MusicManager");
        if (music != null) {
            music.GetComponent<AudioSource>().Pause();
        }

        if (win == 1) {
            audioSource.PlayOneShot(winAudioClip);
        } else {
            audioSource.PlayOneShot(loseAudioClip);
        }

        if (music != null) {
            music.GetComponent<AudioSource>().UnPause();
        }

    }

    public void GoToMainMenu() {
        SceneManager.LoadScene(0);
    }

    public void RetryGame() {
        SceneManager.LoadScene(1);
    }
}
