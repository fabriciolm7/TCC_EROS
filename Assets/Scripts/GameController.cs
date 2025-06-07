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

    // Novas variáveis para armazenar o progresso ao entrar na fase
    private int scoreBeforeLevel;
    private int strawberriesBeforeLevel;

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

        // Salva o estado atual da pontuação e morangos ao iniciar a fase
        scoreBeforeLevel = totalScore;
        strawberriesBeforeLevel = totalStrawberriesCollected;
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
        // Restaura o progresso salvo
        totalScore = scoreBeforeLevel;
        totalStrawberriesCollected = strawberriesBeforeLevel;
        UpdateScore();

        // Exibe o game over com fade ou fallback
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

    public static void ResetProgress()
    {
        totalScore = 0;
        totalStrawberriesCollected = 0;
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
