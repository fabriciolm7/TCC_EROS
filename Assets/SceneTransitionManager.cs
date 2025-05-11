using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager instance;
    [SerializeField] Animator animator;

    private void Awake()
    {
        instance = this;

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
}