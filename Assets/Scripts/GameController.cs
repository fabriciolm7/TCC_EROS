using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int totalScore;
    public static GameController instance;
    public TextMeshProUGUI scoreText;
    public GameObject gameOver;
    public static int totalStrawberriesCollected = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {

    }
    public void UpdateScore()
    {
        scoreText.text = totalScore.ToString();
    }
    public void ShowGameOver()
    {
        gameOver.SetActive(true);
    }
    public void RestartGame(string lvlName)
    {
        SceneManager.LoadScene(lvlName);
    }
    public void doExitGame()
    {
        Application.Quit();
    }
     public void AddStrawberry()
    {
        totalStrawberriesCollected++;
    }
}


