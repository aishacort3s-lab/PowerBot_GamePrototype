using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Pilha : MonoBehaviour
{
    public GameObject Audio;

    [Header("Próxima cena")]
    public string lvlName;

    [Header("Objeto visual que aparece quando coletado")]
    public GameObject collected;

    private SpriteRenderer sr;
    private CircleCollider2D circle;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            sr.enabled = false;
            circle.enabled = false;

            if (collected != null)
                collected.SetActive(true);

            LoadNextLevelImmediately();

        }
    }

    private void LoadNextLevelImmediately()
    {
        // Instancia o Audio, se atribuído
        if (Audio != null)
        {
            Instantiate(Audio, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Pilha.cs: Audio não atribuído!");
        }

        // Carrega a cena, se o nome estiver definido
        if (!string.IsNullOrEmpty(lvlName))
        {
            SceneManager.LoadScene(lvlName);
        }
        else
        {
            Debug.LogWarning("Pilha.cs: lvlName não definido!");
        }
    }
}
