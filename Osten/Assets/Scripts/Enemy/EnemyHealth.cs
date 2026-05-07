using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnemyHealth : MonoBehaviour
{
    [Header("Vida do inimigo")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("Animações")]
    public Animator anim;
    public bool isDead = false;

    [Header("Som de dano")]
    public AudioSource damageSound; 

    [Header("Som de morte / vitória")]
    public AudioSource deathSound; 

    [Header("Knockback")]
    public float knockbackForce = 6f;
    public float knockbackDuration = 0.12f;
    private Rigidbody2D rb;
    private Coroutine knockbackCoroutine;

    [Header("Atordoamento (Stun)")]
    public float stunDuration = 0.4f;
    public bool isStunned = false;

    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogWarning("EnemyHealth: Adicione um Rigidbody2D ao inimigo para o knockback funcionar.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bala"))
        {
            Destroy(collision.gameObject);
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        // ✅ Toca som de dano
        if (damageSound != null)
            damageSound.Play();

        // Animação de dano
        if (anim != null)
            anim.SetTrigger("dano");

        // Inicia stun
        if (!isStunned)
            StartCoroutine(Stun());

        // Knockback
        if (rb != null)
        {
            float direction = transform.localScale.x > 0 ? -1f : 1f;
            Vector2 knockDir = new Vector2(direction, 0.5f).normalized;

            if (knockbackCoroutine != null) StopCoroutine(knockbackCoroutine);
            knockbackCoroutine = StartCoroutine(ApplyKnockback(knockDir));
        }

        // Verifica morte
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator ApplyKnockback(Vector2 direction)
    {
        float timer = 0f;

        rb.velocity = direction * knockbackForce;

        while (timer < knockbackDuration)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero;
        knockbackCoroutine = null;
    }

    private IEnumerator Stun()
    {
        isStunned = true;
        yield return new WaitForSeconds(stunDuration);
        isStunned = false;
    }

    private void Die()
    {
        isDead = true;

        if (rb != null)
            rb.velocity = Vector2.zero;

        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D col in colliders)
        {
            col.enabled = false;
        }

        if (anim != null)
            anim.SetTrigger("death");

        if (deathSound != null)
            deathSound.Play();

        StartCoroutine(LoadEndScene());
    }

    private IEnumerator LoadEndScene()
    {
        float waitTime = (deathSound != null) ? deathSound.clip.length : 1.2f;
        yield return new WaitForSeconds(waitTime);

        // troca de cena
        SceneManager.LoadScene("Obrigado"); 
    }
}

