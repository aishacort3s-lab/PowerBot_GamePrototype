using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadNextCorredor : MonoBehaviour
{
    public void LoadCorredor()
    {
        SceneManager.LoadScene("lvl_corredor");
        Debug.Log("Cena carregada: lvl_corredor (sem resetar mortes).");
    }
}
