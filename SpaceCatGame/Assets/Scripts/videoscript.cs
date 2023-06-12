using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class videoscript : MonoBehaviour
{
    public VideoPlayer VideoPlayer; // video
    public string SceneName; // Kohdekohteen (scene) nimi

    void Start()
    {
        VideoPlayer.loopPointReached += LoadScene; // LoadScene toteutetaan kun video loppuun.
    }

    void LoadScene(VideoPlayer vp)
    {
        SceneManager.LoadScene(SceneName); // Ladataan uusi kohde (scene) annetun SceneName-nimen perusteella
    }
}
