using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NextsSceneButton : MonoBehaviour
{
    [Header("lvl_1")]
    public string cenaDestino;

    public void CarregarCena()
    {
        SceneManager.LoadScene(cenaDestino);
    }
}
