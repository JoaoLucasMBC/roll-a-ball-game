using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Vector3 pointA;
    [SerializeField] private Vector3 pointB;
    [SerializeField] private float speed = 2f;

    private Vector3 currentTarget;

    void Start()
    {
        // Ele começa indo para o ponto A
        currentTarget = pointA;
    }

    void Update()
    {
        // Move o inimigo em direção ao ponto atual
        // Linha escrita com suporte do ChatGPT
        // Prompt: Como fazer um objeto em Unity se mover entre dois pontos
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);

        // Caso ele chegue perto o suficiente de um ponto, passa a seguir o outro
        if (Vector3.Distance(transform.position, currentTarget) < 0.001f)
        {
            if (currentTarget == pointA)
            {
                currentTarget = pointB;
            }
            else
            {
                currentTarget = pointA;
            }
        }
    }
}
