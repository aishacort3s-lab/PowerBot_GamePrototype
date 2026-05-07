using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Destruir em contato com camadas (opcional)")]
    public string layerParaDestruir = "Wall";

    private void Start()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (!string.IsNullOrEmpty(layerParaDestruir))
        {
            int layerIndex = LayerMask.NameToLayer(layerParaDestruir);
            if (collision.gameObject.layer == layerIndex)
            {
                Destroy(gameObject);
            }
        }
    }
}
