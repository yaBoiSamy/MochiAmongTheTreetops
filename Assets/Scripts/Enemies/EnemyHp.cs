using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyHp : MonoBehaviour
{
    public int hp;
    public float dedTime;
    private bool isDed;
    public string enemyType;
    public GameObject enemy;
    private Rigidbody2D rb;
    private CircleCollider2D collider;
    private Pathfinding.AIPath movementScript;

    void start()
    {
        rb = enemy.GetComponent<Rigidbody2D>();
        if (enemyType == "Mostiko")
        {
            movementScript = enemy.GetComponent<Pathfinding.AIPath>();
            collider = enemy.GetComponent<CircleCollider2D>();
        }
    }
   
    void Update()
    {
        if (isDed)
        {
            dedTime -= Time.deltaTime;
            if(dedTime <= 0)
            {
                enemy.gameObject.SetActive(false);
                transform.position = new Vector2(0, 0);
            }
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "respawn damageable")
        {
            TakeDamage(hp, 0, 0);
        }
    }

    public void TakeDamage(int damage, int knockback, int direction)
    {
        hp -= damage;

        if (hp <= 0)
        {
            ded();
        }

        rb.velocity = new Vector2(direction * knockback, knockback);
    }

    public void ded()
    {
        transform.rotation = Quaternion.Euler(0, 0, 90);
        isDed = true;
    }
}
