using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float rotationSpeed = 5f;
    private Vector3 offset;
    private float currentAngleY;

    void Start()
    {
        offset = transform.position - player.transform.position;
        currentAngleY = transform.eulerAngles.y;
    }

    void LateUpdate()
    {
        // Pega e soma a rotação do mouse horizontalmente
        currentAngleY += Input.GetAxis("Mouse X") * rotationSpeed;

        // Usa Quaternio para rotação
        // Linha escrita com suporte do ChatGPT
        // Prompt: Como fazer uma rotação da câmera em Unity
        Quaternion rotation = Quaternion.Euler(0, currentAngleY, 0);

        // Ajuda a câmera com o offset do jogador
        transform.position = player.transform.position + rotation * offset;

        // Câmera olha para o jogador
        // Linha escrita com suporte do ChatGPT
        // Prompt: Como fazer uma câmera seguir o jogador em Unity
        transform.LookAt(player.transform.position + Vector3.up);
    }
}
