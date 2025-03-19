using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{

    public TMP_Text scoreText;

    public Button restartButton;
    public Button mainMenuButton;
    public Scene sceneLoaded;
    public string currentScene;
    public int currentSceneID;
    public string mainMenuScene = "2";
    public GameObject gameOverGO;
    public GameObject sliderGO;
    public GameObject staminasliderGO;

    // Start is called before the first frame update
    private void Awake()
    {
        sceneLoaded = SceneManager.GetActiveScene();
    }
    void Start()
    {
        currentScene = sceneLoaded.name;

        //mainMenuButton.onClick.AddListener(SceneManager.LoadScene(0));
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GameOver(int score)
    {
        staminasliderGO.SetActive(false);
        sliderGO.SetActive(false);
        gameOverGO.SetActive(true);
        Time.timeScale = 0f;
        scoreText.text = score.ToString() + " points";
    }
    public void LoadThisScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(currentScene);
    }
    public void LoadMainMenuScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
