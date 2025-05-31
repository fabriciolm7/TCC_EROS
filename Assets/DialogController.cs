using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;  // Importa o sistema de cenas

public class DialogController : MonoBehaviour
{
    [Header("Referências UI")]
    public GameObject dialogPanel;
    public TextMeshProUGUI dialogText;
    public Button closeButton;
    public static bool IsDialogActive;
    public static bool jaMostrouDialogo = false;

    private bool isFinalDialog = false;  // Verifica se é o diálogo final

    private void Start()
    {
        IsDialogActive = false;

        closeButton.onClick.AddListener(() =>
        {
            dialogPanel.SetActive(false);
            IsDialogActive = false;

            if (isFinalDialog)
            {
                // Troca para outra cena, por exemplo, a cena de "Fim"
                SceneManager.LoadScene("FinalCredits");
            }
        });
    }

    public void MostrarDialogo(int morangosColetados, float vidaPerdida)
    {
        string mensagem = $"Parabéns! Você resgatou {morangosColetados} morangos!\n" +
                          $"O inimigo perdeu {vidaPerdida} pontos de vida!\nPara derrotá-lo, pule em cima dele e fuja de seus ataques.";
        dialogText.text = mensagem;
        dialogPanel.SetActive(true);
        IsDialogActive = true;
        jaMostrouDialogo = true;
        isFinalDialog = false;  // Esse não é o diálogo final
    }

    public void MostrarDialogoFinal()
    {
        string mensagem = $"Parabéns! Você venceu a corrida para resgatar seus morangos!";
        dialogText.text = mensagem;
        dialogPanel.SetActive(true);
        IsDialogActive = true;
        isFinalDialog = true;  // Define como diálogo final
    }
}
