using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolnfAi : MonoBehaviour
{
    public GameObject wolnfTerritory;
    private WolnfTerritory wolnfTerritoryScript;
    public Transform territoryCore;
    public Rigidbody2D rb;
    public int wolfSpeed;
    private int direction;
    private int movement;
    private bool facingRight;
    public float buffer;
    public int hp;

    private PlayerMovement playerMovementScript;
    public GameObject player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        wolnfTerritoryScript = wolnfTerritory.GetComponent<WolnfTerritory>();
        playerMovementScript = player.GetComponent<PlayerMovement>();
    }
    
    void Update()
    {
        if (wolnfTerritoryScript.territoryEntered)
        {
            if (player.transform.position.x > transform.position.x)
            {
                if (direction != 1)
                {
                    flip();
                    direction = 1;
                }
                movement = 1;
            }
            else
            {
                if (direction != -1)
                {
                    flip();
                    direction = -1;
                }
                movement = 1;
            }
        }
        else
        {
            if(territoryCore.position.x - buffer < transform.position.x && transform.position.x < territoryCore.position.x + buffer)
            {
                movement = 0;
            }
            else if(transform.position.x < territoryCore.position.x)
            {
                if (direction != 1)
                {
                    flip();
                    direction = 1;
                }
                movement = 1;
            }
            else if(transform.position.x > territoryCore.position.x)
            {
                if (direction != -1)
                {
                    flip();
                    direction = -1;
                }
                movement = 1;
            }
        }

        rb.velocity = new Vector2(direction * movement * wolfSpeed, rb.velocity.y);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("NOICEW");
        hp -= damage;

        if(hp <= 0)
        {
            Debug.Log("amogus");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy damageable")
        {
            hp -= playerMovementScript.damage;
        }
    }

    void flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
