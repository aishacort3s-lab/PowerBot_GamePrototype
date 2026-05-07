using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuingPlayerEnemy : MonoBehaviour
{

    private Rigidbody2D rb;
    public float speed = 10f;
    private Transform transformPlayer;
    public float detectionRadius = 5f;
    private bool facingRight = false;
    private bool isChasing = false;

    private EnemyHealth enemyHealth; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transformPlayer = GameObject.FindWithTag("Player").transform;
        enemyHealth = GetComponent<EnemyHealth>(); 
    }

    void Update()
    {
        if (enemyHealth != null && enemyHealth.isDead)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        DetectionPlayer();

        if (isChasing)
        {
            FollowPlayer();
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void DetectionPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, transformPlayer.position);

        isChasing = distanceToPlayer <= detectionRadius;
    }

    private void FollowPlayer()
    {
        Vector2 direction = (transformPlayer.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
        PositionCorrection();
    }

    private void PositionCorrection()
    {
        if (rb.velocity.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (rb.velocity.x < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        facingRight = !facingRight;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
