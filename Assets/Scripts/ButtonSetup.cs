using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSetup : MonoBehaviour
{
    public Button muteButton;

    void Start()
    {
        muteButton.onClick.RemoveAllListeners();
        muteButton.onClick.AddListener(MusicManager.Instance.Mute);
    }
}

