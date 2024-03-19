using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Update is called once per frame
    // Rotação para as runas
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);       
    }
}
