using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // Lógica do timer foi retirada de um curso que eu fiz de Unity nas férias
    // Créditos: https://udemy.com/course/unitycourse/

    [SerializeField] float timeToDie = 90.0f;
    [SerializeField] Image timerImage;
    float timerValue;
    float fillFraction;
    public bool playing;

    // Start is called before the first frame update
    void Start()
    {
        ResetTimer();
        playing = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();   
        timerImage.fillAmount = fillFraction;
    }

    private void UpdateTimer() {
        timerValue -= Time.deltaTime;
        if (playing) {
            if (timerValue <= 0) {
                playing = false;
            } else {
                fillFraction = timerValue / timeToDie;
            }
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
