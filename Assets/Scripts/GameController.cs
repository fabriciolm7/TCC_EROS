using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static int totalScore;
    public static GameController instance;
    public TextMeshProUGUI scoreText;
    public GameObject gameOver;
    public static int totalStrawberriesCollected = 0;
    public static float bossCurrentHealth = -1f;

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

    void Update()
    {
        if (
            gameOver != null &&
            gameOver.activeSelf &&
            SceneTransitionManager.instance != null &&
            SceneTransitionManager.instance.readyForInput &&
            Input.GetKeyDown(KeyCode.Space)
        )
        {
            Debug.Log("Restart triggered by spacebar.");
            RestartGame(SceneManager.GetActiveScene().name);
        }
    }


    public void ShowGameOver()
    {
        SceneTransitionManager stm = FindObjectOfType<SceneTransitionManager>();
        if (stm != null)
        {
            stm.ShowGameOverWithFade(gameOver);
        }
        else
        {
            Debug.LogError("SceneTransitionManager not found.");
            gameOver.SetActive(true); // fallback
            Time.timeScale = 0f;
        }
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

