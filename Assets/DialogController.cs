using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogController : MonoBehaviour
{
    [Header("Referências UI")]
    public GameObject dialogPanel;
    public TextMeshProUGUI dialogText;
    public Button closeButton;

    public static bool IsDialogActive { get; private set; }
    private bool isFinalDialog = false;
    private bool initialDialogShown = false;

    private const string BOSS_SCENE_NAME = "lvl_9";

    private void Awake()
    {
        dialogPanel.SetActive(false);
        IsDialogActive = false;
    }

    private void Start()
    {
        closeButton.onClick.AddListener(CloseDialog);

        if (SceneManager.GetActiveScene().name == BOSS_SCENE_NAME && !PlayerPrefs.HasKey("DialogoUltimaFaseMostrado") && !initialDialogShown)
        {
            MostrarDialogo(GameController.totalStrawberriesCollected, 0f);
            PlayerPrefs.SetInt("DialogoUltimaFaseMostrado", 1);
            PlayerPrefs.Save();
            initialDialogShown = true;
        }
    }

    public void MostrarDialogo(int morangosColetados, float vidaPerdida)
    {
        if (IsDialogActive)
        {
            Debug.LogWarning("Tentativa de mostrar diálogo enquanto outro está ativo. Ignorando.");
            return;
        }

        string mensagem = $"Parabéns! Você resgatou {morangosColetados} morangos!\n" +
                            $"O inimigo perdeu {vidaPerdida} pontos de vida!\nPara derrotá-lo, pule em cima dele e fuja de seus ataques.";

        OpenDialog(mensagem, false);
    }

    public void MostrarDialogoFinal()
    {
        if (IsDialogActive)
        {
            Debug.LogWarning("Tentativa de mostrar diálogo final enquanto outro está ativo. Ignorando.");
            return;
        }

        string mensagem = $"Parabéns! Você venceu a corrida para resgatar seus morangos!";
        OpenDialog(mensagem, true);
    }

    private void OpenDialog(string mensagem, bool final)
    {
        dialogText.text = mensagem;
        dialogPanel.SetActive(true);
        IsDialogActive = true;
        isFinalDialog = final;
    }

    private void CloseDialog()
    {
        dialogPanel.SetActive(false);
        IsDialogActive = false;

        if (isFinalDialog)
        {
            SceneManager.LoadScene("FinalCredits");
        }
    }
}
