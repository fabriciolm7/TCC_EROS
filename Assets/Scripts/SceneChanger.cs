using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Tempo fixo em segundos antes de mudar para a próxima cena
    private float delay = 1.3f; // Valor fixo de 1.3 segundos

    // Nome da cena para a qual você quer mudar
    public string lvlName;

    // Start é chamado antes do primeiro frame update
    void Start()
    {
        // Inicia a corrotina para mudar de cena após o delay
        SceneManager.LoadScene(lvlName);
    }

    // Corrotina que espera pelo tempo especificado e depois muda de cena
    IEnumerator ChangeSceneAfterDelay()
    {
        // Espera pelo tempo definido em 'delay'
        yield return new WaitForSeconds(delay);

        // Verifica se o nome da cena está configurado
        if (!string.IsNullOrEmpty(lvlName))
        {
            // Muda para a cena especificada
            SceneManager.LoadScene(lvlName);
        }
        else
        {
            Debug.LogError("O nome da cena (lvlName) não foi definido!");
        }
    }
}
