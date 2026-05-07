using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class MainMenuController : MonoBehaviour
{

    [Header("Canvas do Menu (arraste aqui ou marque a Tag 'MenuCanvas')")]
    public GameObject menuCanvas;

    [Header("Tecla para abrir/fechar o menu")]
    public KeyCode menuKey = KeyCode.Escape;

    private bool isMenuOpen = false;

    void Awake()
    {
     
        EnsureEventSystemExists();

       
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
      
        if (menuCanvas == null)
            FindMenuCanvas();

     
        CloseMenu();
    }

    void Update()
    {
        if (Input.GetKeyDown(menuKey))
        {
            ToggleMenu();
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       
        if (menuCanvas == null)
        {
            FindMenuCanvas();
        }

        
        EnsureEventSystemExists();
    }

    private void FindMenuCanvas()
    {
        GameObject found = GameObject.FindWithTag("MenuCanvas");
        if (found != null)
        {
            menuCanvas = found;
            menuCanvas.SetActive(false);
        }
        else
        {
            Debug.LogWarning("[MainMenuController] Nenhum objeto com Tag 'MenuCanvas' encontrado na cena " + SceneManager.GetActiveScene().name);
        }
    }

    public void ToggleMenu()
    {
        if (menuCanvas == null)
        {
            FindMenuCanvas();
            if (menuCanvas == null) return;
        }

        isMenuOpen = !isMenuOpen;
        menuCanvas.SetActive(isMenuOpen);
        Time.timeScale = isMenuOpen ? 0f : 1f;
    }

    public void OpenMenu()
    {
        if (menuCanvas == null)
        {
            FindMenuCanvas();
            if (menuCanvas == null) return;
        }

        isMenuOpen = true;
        menuCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseMenu()
    {
        if (menuCanvas == null)
        {
            FindMenuCanvas();
            if (menuCanvas == null) return;
        }

        isMenuOpen = false;
        menuCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Debug.Log("Fechando o jogo...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void EnsureEventSystemExists()
    {
        if (FindObjectOfType<EventSystem>() == null)
        {
            GameObject es = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
            Debug.Log("[SceneMenuController] Nenhum EventSystem encontrado, criando um novo.");
        }
    }
}

