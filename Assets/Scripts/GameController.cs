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

    public void UpdateScore()
    {
        scoreText.text = totalScore.ToString();
    }

    public void ShowGameOver()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0f; // Pausa o jogo
    }

    public void RestartGame(string lvlName)
    {
        Time.timeScale = 1f;

        SceneTransitionManager stm = FindObjectOfType<SceneTransitionManager>();
        if (stm != null)
        {
            stm.LoadScene(lvlName);
        }
        else
        {
            Debug.LogError("SceneTransitionManager não encontrado na cena.");
        }
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

