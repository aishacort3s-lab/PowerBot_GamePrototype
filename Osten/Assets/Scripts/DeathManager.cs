using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    public static DeathManager Instance { get; private set; }
    public int deathCount = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject); 
        }

    }

    // Adiciona morte ao contador
    public void AddDeath()
    {
        deathCount++;
        Debug.Log("Mortes: " + deathCount);
    }

    // Retorna número de mortes
    public int GetDeathCount()
    {
        return deathCount;
    }

    // Reseta contador (opcional)
    public void ResetDeaths()
    {
        deathCount = 0;
        Debug.Log("Contador foi zerado");
    }

    // Método opcional se quiser herdar mortes de outro personagem
    public void InheritDeathsFrom(int previousDeathCount)
    {
        deathCount = previousDeathCount;
        Debug.Log("Mortes herdadas: " + deathCount);
    }
}
