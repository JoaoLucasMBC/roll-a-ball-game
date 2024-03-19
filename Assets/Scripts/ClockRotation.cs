using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockRotation : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // O relógio roda apenas horizontalmente
        transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);       
    }
}
