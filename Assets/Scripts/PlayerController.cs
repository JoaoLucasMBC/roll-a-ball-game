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

    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private Vector3 jump;
    private float jumpForce = 2.0f;
    private bool isGrounded;
    [SerializeField] private float speed = 0f;
    private int count;
    [SerializeField] private TextMeshProUGUI countText;

    [SerializeField] private GameObject timerObject;
    [SerializeField] private Camera playerCamera;

    [Header("Health Points")]
    [SerializeField] private int health = 3;
    [SerializeField] private Image[] hearts;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        SetCountText();
        playerCamera = Camera.main;
    }

    private void Update() {
        if (isGrounded) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                rb.AddForce(jump * jumpForce, ForceMode.Impulse);
                isGrounded = false;
            }
        }
    }

    private void FixedUpdate() {

    // Get the directions relative to the camera's rotation
    Vector3 cameraForward = playerCamera.transform.forward;
    Vector3 cameraRight = playerCamera.transform.right;

    // Make sure the movement is strictly horizontal
    cameraForward.y = 0;
    cameraRight.y = 0;
    cameraForward.Normalize();
    cameraRight.Normalize();

    // Calculate the direction based on the input and the camera's orientation
    Vector3 direction = cameraForward * movementY + cameraRight * movementX;

    // Add force to the Rigidbody in the direction calculated
    rb.AddForce(direction.normalized * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
}


    void OnMove(InputValue movementValue) {

        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;

    }

    void SetCountText() {
        countText.text = "Count: " + count.ToString();
        if (count >= 10) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("PickUp")) {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        } else if (other.gameObject.CompareTag("ClockUp")) {
            other.gameObject.SetActive(false);
            // get the Timer script from the timerObject
            Timer timer = timerObject.GetComponent<Timer>();
            timer.IncrementTimer();
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Ground")) {
            isGrounded = true;
        } else if (other.gameObject.CompareTag("Enemy")) {
            health--;
            if (health >= 0) {
                hearts[3 - health - 1].enabled = false;
            }
            if (health == 0) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
