using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GoToScene(string sceneName)
    {
        // Zera os morangos antes de carregar a nova cena
        GameController.ResetProgress();

        // Também pode zerar o score se quiser:
        // GameController.totalScore = 0;

        SceneManager.LoadScene(sceneName);
    }

    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Application has quit");
    }
}
