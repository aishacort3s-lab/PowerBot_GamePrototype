using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{

    [Header("Configurações")]
    public bool resetDeathsOnClick = false; 

    public void LoadNext()
    {
        if (resetDeathsOnClick && DeathManager.Instance != null)
        {
            DeathManager.Instance.ResetDeaths();
            Debug.Log("Contador de mortes zerado!");
        }

       
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
