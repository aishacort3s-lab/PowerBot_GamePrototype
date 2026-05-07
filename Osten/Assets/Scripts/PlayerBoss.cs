using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBoss : MonoBehaviour
{
    [Header("Som de Pulo")]
    public AudioSource jumpSound;

    [Header("Som de Tiro")]
    public AudioSource shootSound;

    [Header("Tiro")]
    public GameObject balaProjetil;
    public Transform arma;
    private bool tiro;
    public float forcaDoTiro = 10f;
    private bool flipX = false;
    private float nextFireTime = 0f;
    public float fireRate = 0.5f;

    private bool isDead = false;
    private bool hasGun = true; 

    [Header("Movimento")]
    public float Speed = 5f;
    public float JumpForce = 10f;

    [Header("Estado de Pulo")]
    public bool isJumping;
    public int maxJumps = 1;
    private int jumpCount = 0;

    [Header("Animações")]
    private Animator anim;
    private Rigidbody2D rig;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Jump();

        tiro = Input.GetButtonDown("Fire1");
        Atirar();
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        rig.velocity = new Vector2(horizontal * Speed, rig.velocity.y);

        if (flipX && horizontal > 0f)
        {
            Flip();
            anim.SetBool("run", true);
        }
        else if (!flipX && horizontal < 0f)
        {
            Flip();
            anim.SetBool("run", true);
        }
        else
        {
            anim.SetBool("run", false);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            rig.velocity = new Vector2(rig.velocity.x, 0f);
            rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
            anim.SetBool("jump", true);

            if (jumpSound != null)
                jumpSound.Play();

            jumpCount++;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isJumping = false;
            jumpCount = 0;
            anim.SetBool("jump", false);
        }

        if (!isDead && (collision.gameObject.CompareTag("Espinho") || collision.gameObject.CompareTag("Serra")))
        {
            isDead = true;
            Debug.Log("Tocou em um obstáculo!");

            DeathManager.Instance?.AddDeath();

            var ui = FindObjectOfType<DeathCounterUI>();
            if (ui != null)
                ui.UpdateDeathCount();

            SceneManager.LoadScene("lvl_renascer");
        }
    }

    private void Atirar()
    {
        if (Time.time < nextFireTime) return;

        if (tiro)
        {
            GameObject temp = Instantiate(balaProjetil, arma.position, Quaternion.identity);

            Rigidbody2D rb = temp.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.velocity = new Vector2(forcaDoTiro, 0f);

            if (shootSound != null)
                shootSound.Play();

            Destroy(temp, 3f);

            nextFireTime = Time.time + fireRate;
        }
    }

    private void Flip()
    {
        flipX = !flipX;
        float x = transform.localScale.x;
        x *= -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);

        forcaDoTiro *= -1;
    }
}
