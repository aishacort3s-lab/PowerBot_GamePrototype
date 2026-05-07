using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public ItemPopup itemPopup;
    private SpriteRenderer sr;
    private CircleCollider2D circle;

    [Header("Efeito visual ao coletar")]
    public GameObject collected;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();

        Debug.Log($"[Gun Start] sr={(sr != null)}, circle={(circle != null)}, itemPopup={(itemPopup != null)}");
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider) return;

        if (collider.CompareTag("Player"))
        {
            Debug.Log("[Gun] Player entrou no trigger");

            Player player = collider.GetComponent<Player>();
            if (player != null)
            {
                player.EquiparArma();
                Debug.Log("[Gun] Equipou arma no player");
            }
            else
            {
                Debug.LogWarning("[Gun] Player não tem componente Player");
            }

            sr.enabled = false;
            if (circle != null) circle.enabled = false;

            if (collected != null)
                collected.SetActive(true);

            if (itemPopup != null)
            {
                Debug.Log("[Gun] Chamando itemPopup.ShowPopup()");
                itemPopup.ShowPopup();
            }
            else
            {
                Debug.LogWarning("[Gun] itemPopup NÃO está atribuído no Inspector!");
            }

            // Destruir depois de 0.3s pra garantir que a chamada tenha tempo
            Destroy(gameObject, 0.3f);
        }
    }

}
