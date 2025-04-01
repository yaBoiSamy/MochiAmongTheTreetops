using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelAI : MonoBehaviour
{
    public Transform player;
    public float patrolDistance;
    public float speed;
    public Vector2 knockback;
    private int direction;
    private int isWalking;
    public float pauseTime;
    private float pauseTimeCurrent;
    public float knockbackTime;
    private float knockbackTimeCurrent;
    private bool isKnocked;
    private bool facingRight;
    private int hp;

    private Vector2 startPos;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        direction = 1;
    }

    void Update()
    {
        if(isWalking == 1 && ((transform.position.x >= startPos.x + patrolDistance && direction == 1) || (transform.position.x <= startPos.x - patrolDistance && direction == -1)))
        {
            isWalking = 0;
            pauseTimeCurrent = pauseTime;
        }
        else if(isWalking == 0 && pauseTimeCurrent <= 0)
        {
            isWalking = 1;
            direction *= -1;
            flip();
        }

        if(isWalking == 0)
        {
            pauseTimeCurrent -= Time.deltaTime;
        }
        if (isKnocked)
        {
            knockbackTimeCurrent -= Time.deltaTime;
            if(knockbackTimeCurrent <= 0)
            {
                isKnocked = false;
            }
        }

        if (!isKnocked)
        {
            rb.velocity = new Vector2(speed * isWalking * direction, rb.velocity.y);
        }
    }

    void flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    } 

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(player.transform.position.x > transform.position.x)
            {
                rb.velocity = new Vector2(-knockback.x, knockback.y);
            }
            else
            {
                rb.velocity = new Vector2(knockback.x, knockback.y);
            }
            
            isKnocked = true;
            knockbackTimeCurrent = knockbackTime;
        }
    }
}
