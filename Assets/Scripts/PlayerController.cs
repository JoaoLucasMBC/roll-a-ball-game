using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    // Movimento do jogador
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private Vector3 jump;
    private float jumpForce = 2.0f;
    private bool isGrounded;
    [SerializeField] private float speed = 0f;

    // Contagem de pickups e de tempo
    private int count;
    private float totalTime;
    [SerializeField] private GameObject timerObject;
    [SerializeField] private Camera playerCamera;

    // Vida do jogador
    [Header("Health Points")]
    [SerializeField] private int health = 3;
    [SerializeField] private Image[] hearts;

    // Barra de progresso
    [Header("Progress Bar")]
    [SerializeField] private Image barImage;
    private float barValue = 10.0f;

    // SFX é uma classe que toca os efeitos nas ações do jogador
    private SFX sFX;

    // Start is called before the first frame update
    void Start()
    {
        // Coloca os valores iniciais
        rb = GetComponent<Rigidbody>();
        count = 0;
        totalTime = 0;
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        UpdateProgressBar();
        playerCamera = Camera.main;
        sFX = GetComponent<SFX>();
    }

    private void Update() {
        // O total time será mostrado no final do jogo
        totalTime += Time.deltaTime;
        
        // Pega o timer e checa se o jogo acabou por tempo
        Timer timer = timerObject.GetComponent<Timer>();
        if (!timer.playing) {
            SaveVariables();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        // Lógica de pulo
        // Crédito: https://www.youtube.com/watch?v=xvLMD2qWaKk e ChatGPT - Prompt: Como fazer um pulo em Unity
        if (isGrounded) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                rb.AddForce(jump * jumpForce, ForceMode.Impulse);
                sFX.PlayJump();
                isGrounded = false;
            }
        }
    }

    private void FixedUpdate() {

        // Precisamos ajustar para o jogador andar sempre na direção da câmera
        Vector3 cameraForward = playerCamera.transform.forward;
        Vector3 cameraRight = playerCamera.transform.right;

        // Prepara os vetores
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // O movimento é sempre na direção da câmera
        Vector3 direction = cameraForward * movementY + cameraRight * movementX;

        rb.AddForce(direction.normalized * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }


    void OnMove(InputValue movementValue) {
        
        // atualiza os valores de movimento
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;

    }

    void UpdateProgressBar() {
        // Atualiza a barra de progresso
        barImage.fillAmount = count / barValue;
        if (count >= 10) {
            SaveVariables();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void OnTriggerEnter(Collider other) {
        // Colisão com os pickups
        if (other.gameObject.CompareTag("PickUp")) {
            other.gameObject.SetActive(false);
            count++;
            sFX.PlayRune();
            UpdateProgressBar();
        } 
        // Colisão com o relógio (adiciona tempo)
        else if (other.gameObject.CompareTag("ClockUp")) {
            other.gameObject.SetActive(false);
            Timer timer = timerObject.GetComponent<Timer>();
            sFX.PlayTimeUp();
            timer.IncrementTimer();
        }
        // Colisão com o coração (adiciona vida)
        else if (other.gameObject.CompareTag("Heart")) {
            if (health < 3) {
                health++;
                hearts[3 - health].enabled = true; // volta o sprite na tela
            }
            other.gameObject.SetActive(false);
            sFX.PlayHPUp();
        }
    }

    private void OnCollisionEnter(Collision other) {
        // Se colide com o chão, ele pode pular novamente
        if (other.gameObject.CompareTag("Ground")) {
            isGrounded = true;
        } 
        // Se colide com um inimigo, perde vida
        else if (other.gameObject.CompareTag("Enemy")) {
            health--;
            sFX.PlayDamage();
            if (health >= 0) {
                hearts[3 - health - 1].enabled = false; // tira o sprite da tela
                rb.AddForce(-transform.forward * 5, ForceMode.Impulse); // joga o jogador para trás
            }
            if (health == 0) {
                SaveVariables();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    private void SaveVariables() {
        // Salva as variáveis no PlayerPrefs
        PlayerPrefs.SetInt("Score", count);
        PlayerPrefs.SetFloat("Time", totalTime);
        if (count == 10) {
            PlayerPrefs.SetInt("Win", 1);
        } else {
            PlayerPrefs.SetInt("Win", 0);
        }
        PlayerPrefs.Save();
    }
}