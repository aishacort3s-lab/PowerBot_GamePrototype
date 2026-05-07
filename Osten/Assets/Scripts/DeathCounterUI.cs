using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathCounterUI : MonoBehaviour
{
    public TextMeshProUGUI deathText;

    void OnEnable()
    {
        UpdateDeathCount();
    }

    // Atualiza o texto da UI
    public void UpdateDeathCount()
    {
        if (DeathManager.Instance != null)
        {
            deathText.text = DeathManager.Instance.GetDeathCount().ToString();
        }
    }

}
