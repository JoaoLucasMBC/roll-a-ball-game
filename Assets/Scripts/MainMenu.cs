using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private Button muteButton;
    private bool isMuted = false;

    [Header("Mute Sprites")]
    [SerializeField] Sprite muteSprite;
    [SerializeField] Sprite muteSpriteHover;
    [SerializeField] Sprite unmuteSprite;
    [SerializeField] Sprite unmuteSpriteHover;

    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MuteGame() {
        isMuted = !isMuted;
        AudioListener.pause = !AudioListener.pause;

        // change the sprite1 and sprite2 inside the SpriteOnHover script in the muteButton
        if (isMuted) {
            muteButton.GetComponent<SpriteOnHover>().sprite1 = muteSprite;
            muteButton.GetComponent<SpriteOnHover>().sprite2 = muteSpriteHover;
        } else {
            muteButton.GetComponent<SpriteOnHover>().sprite1 = unmuteSprite;
            muteButton.GetComponent<SpriteOnHover>().sprite2 = unmuteSpriteHover;
        }
    }

    public void QuitGame() {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
