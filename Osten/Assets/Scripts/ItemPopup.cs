using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemPopup : MonoBehaviour
{

    [Header("Painel principal do Popup")]
    public GameObject popupPanel;

    [Header("Configurações")]
    public bool pauseGame = false;

    [Header("Som do Popup")]
    public AudioClip popupSound; // arraste o som no inspetor
    private AudioSource audioSource;

    void Start()
    {
        Debug.Log($"[ItemPopup Start] popupPanel={(popupPanel != null)}, activeInHierarchy={(popupPanel != null ? popupPanel.activeInHierarchy.ToString() : "N/A")}");

        // Cria um AudioSource se não tiver
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false; // garante que não vai repetir
        audioSource.volume = 1f; // ajuste o volume se quiser
    }

    public void ShowPopup()
    {
        Debug.Log("[ItemPopup] ShowPopup chamado");

        if (popupPanel != null)
        {
            popupPanel.SetActive(true);
            Debug.Log("[ItemPopup] popupPanel.SetActive(true)");
        }
        else
        {
            Debug.LogWarning("[ItemPopup] popupPanel NÃO está atribuído no Inspector!");
        }

        // 🔊 Toca a musiquinha uma vez
        if (popupSound != null)
        {
            Debug.Log("[ItemPopup] Tocando som do popup (uma vez)");
            audioSource.Stop(); // garante que não tem som antigo tocando
            audioSource.PlayOneShot(popupSound);
        }

        if (pauseGame)
        {
            Time.timeScale = 0f;
            Debug.Log("[ItemPopup] Time.timeScale = 0");
        }
    }

    public void ClosePopup()
    {
        Debug.Log("[ItemPopup] ClosePopup chamado");

        if (popupPanel != null)
            popupPanel.SetActive(false);

        // 🛑 Para o som se ainda estiver tocando
        if (audioSource.isPlaying)
        {
            Debug.Log("[ItemPopup] Parando som do popup");
            audioSource.Stop();
        }

        Time.timeScale = 1f;
        Debug.Log("[ItemPopup] Time.timeScale = 1");
    }
}
