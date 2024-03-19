using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance = null;


    // MusicManager, que toca a música do jogo, não é destruído entre as cenas
    // e é acessível de qualquer lugar do jogo
    // Desenvolvido com suporte do ChatGPT
    // Prompt/Créditos: Como fazer um gerenciador de música que não é destruído entre cenas em Unity

    public static MusicManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void Mute() {
        AudioListener.pause = !AudioListener.pause;
    }

}

