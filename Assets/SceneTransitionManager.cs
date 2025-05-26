using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager instance;
    public bool readyForInput = false;

    [SerializeField] Animator animator;
private void Awake()
{
    instance = this;
    readyForInput = false; // Reset just in case
}


    public void LoadScene(string sceneName)
    {
        StartCoroutine(Transition(sceneName));
    }

    IEnumerator Transition(string sceneName)
    {
        animator.SetTrigger("End");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);

        // Aguarde um frame para garantir que a cena carregou
        yield return null;

        // Opcional: ative uma transição de entrada
        animator.SetTrigger("Start");
    }

    public void ShowGameOverWithFade(GameObject gameOverPanel)
    {
        StartCoroutine(GameOverFadeRoutine(gameOverPanel));
    }

    IEnumerator GameOverFadeRoutine(GameObject gameOverPanel)
    {
        readyForInput = false;

        animator.SetTrigger("End");
        yield return new WaitForSecondsRealtime(1f); // use Realtime!

        gameOverPanel.SetActive(true);

        animator.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(1f); // again, Realtime!

        Time.timeScale = 0f;

        readyForInput = true; // ✅ input now allowed
    }
    
}
