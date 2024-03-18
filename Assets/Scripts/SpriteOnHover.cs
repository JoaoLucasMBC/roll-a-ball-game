using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteOnHover : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] public Sprite sprite1;
    [SerializeField] public Sprite sprite2;

    void Start()
    {
        button.image.sprite = sprite1;  
    }

    public void changeWhenHover() {
        button.image.sprite = sprite2;
    }

    public void changeWhenLeaves() {
        button.image.sprite = sprite1;
    }
}
