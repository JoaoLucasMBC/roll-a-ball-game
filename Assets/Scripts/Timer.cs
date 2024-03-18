using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToDie = 120.0f;
    [SerializeField] Image timerImage;
    float timerValue;
    float fillFraction;

    // Start is called before the first frame update
    void Start()
    {
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();   
        timerImage.fillAmount = fillFraction;
    }

    private void UpdateTimer() {
        timerValue -= Time.deltaTime;
        if (timerValue <= 0) {
            // loads the next scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        } else {
            fillFraction = timerValue / timeToDie;
        }
    }

    private void ResetTimer() {
        timerValue = timeToDie;
    }

    public void IncrementTimer() {
        timerValue += 30;
        if (timerValue > timeToDie) {
            timerValue = timeToDie;
        }
    }
}
