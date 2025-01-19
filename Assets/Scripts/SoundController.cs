using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider; // Referência ao Slider que controla o volume
    private AudioSource audioSource; // Referência ao componente de áudio

    void Start()
    {
        // Obtém a referência do componente de áudio associado ao GameObject atual
        audioSource = GetComponent<AudioSource>();

        // Define o valor inicial do slider com base no volume atual
        volumeSlider.value = audioSource.volume;

        // Adiciona um listener ao slider para chamar a função OnVolumeChanged quando o valor do slider é alterado
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    void OnVolumeChanged(float volume)
    {
        // Atualiza o volume do áudio com base no valor do slider
        audioSource.volume = volume;
    }
}
