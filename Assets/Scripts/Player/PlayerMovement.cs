using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    //jump-related variables
    private Vector2 jump;
    public float jumpForce;
    public int maxJumps;
    private int extraJumps;
    private bool isGrounded;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private float jumpTimeCounter;
    private bool isJumping;
    public float jumpTime;

    //wall sliding related variables
    public float wallSlidingSpeed;
    private bool isOnWall;
    private bool wallSliding;
    public Transform WallCheck;
    public float wallCheckRadius;

    private bool wallJumping;
    public float wallJumpx;
    public float wallJumpy;
    public float wallJumpTime;

    public Vector2 knockBackTaken;
    private Vector2 startPos;
    private Vector2 lastCheckPoint;
    public int MaxHp;
    private int Hp;
    public float immunityTime;
    private float currentImmunityTime;
    private bool isImmune;
    public Image[] hearts;

    public GameObject tail;
    public int damage;
    public Transform enemyCollider;
    public float attackBoxRadius;
    public LayerMask Enemies;
    public float AttackCooldown;
    private float currentAttackCooldown;
    private bool isAttacking;
    public int knockBackGiven;

    public int attackDamage;

    private Rigidbody2D rb;

    private bool facingRight = true;

    public SpriteRenderer spriteRenderer;
    public Sprite attackMochi;
    public Sprite normalMochi;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        extraJumps = maxJumps - 1;
        jumpTimeCounter = jumpTime;
        startPos = transform.position;
        lastCheckPoint = startPos;
        Hp = MaxHp;
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            SceneManager.LoadScene("Menu");
        }
        if (Input.GetMouseButtonDown(0))
        {
            spriteRenderer.sprite = attackMochi;
            tail.gameObject.SetActive(true);
            currentAttackCooldown = AttackCooldown;
            isAttacking = true;
            Attack();
        }
        if (isAttacking)
        {
            currentAttackCooldown -= Time.deltaTime;
            if(currentAttackCooldown <= 0)
            {
                isAttacking = false;
                tail.gameObject.SetActive(false);
                spriteRenderer.sprite = normalMochi;
            }
        }

        if (Hp <= 0){
            //play deat animation
            restartCurrentScene();
        }
        //check if the character is in contact with the ground or a wall
        isOnWall = Physics2D.OverlapCircle(WallCheck.position, wallCheckRadius, whatIsGround);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        //get movement input
        float movement = Input.GetAxis("Horizontal");

        //set character direction
        if (facingRight == false && movement > 0)
        {
            flip();
        }
        else if (facingRight == true && movement < 0)
        {
            flip();
        }

        //make the character move
        if (!wallSliding && !wallJumping && isImmune == false)
        {
            rb.velocity = new Vector2(movement * speed, rb.velocity.y);
        }
        
        //define when the character is wall sliding
        if (isOnWall == true && isGrounded == false)
        {
            
            wallSliding = true;
        }
        else
        {
            wallSliding = false;
        }

        //reset jumps
        if (isGrounded || isOnWall)
        {
            extraJumps = maxJumps - 1;
            if (isGrounded)
            {
                jumpTimeCounter = jumpTime;
            }
        }

        //limit the character's speed when on a wall
        if (wallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }

        //If the player wants to jump...
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //... off the ground
            if (isGrounded)
            {
                //rb.velocity = Vector2.up * jumpForce;
                isJumping = true;
            }

            //... twice or more
            else if (extraJumps > 0)
            {
                rb.velocity = Vector2.up * jumpForce * 1.5f;
                extraJumps--;
                //isJumping = true;
            }

            //... off a wall
            if (wallSliding)
            {
                wallJumping = true;
                Invoke("setWallJumpToFalse", wallJumpTime);
                wallSliding = false;
                isOnWall = false;
            }

            if (wallJumping)
            {
                rb.velocity = new Vector2(wallJumpx * -movement, wallJumpy);
            }
        }


        //make the character jump longer when jumping off the ground
        if (Input.GetKey(KeyCode.Space))
        {
            if (jumpTimeCounter > 0)
            {
                //rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

        if (isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (isImmune)
        {
            currentImmunityTime -= Time.deltaTime;
        }
        if (currentImmunityTime <= 0)
        {
            isImmune = false;
        }

        int i = 1;
        foreach (Image heart in hearts)
        {
            if (i <= Hp)
            {
                heart.gameObject.SetActive(true);
            }
            else
            {
                heart.gameObject.SetActive(false);
            }
            i++;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        
        if (collision.gameObject.tag == "Enemy" && isImmune == false)
        {
            currentImmunityTime = immunityTime;
            isImmune = true;
            Hp -= 1;
            if (collision.transform.position.x < transform.position.x)
            {
                rb.velocity = knockBackTaken;
            }
            else if (collision.transform.position.x > transform.position.x)
            {
                rb.velocity = new Vector2(-knockBackTaken.x, knockBackTaken.y);
            }
        }
        else if (collision.gameObject.tag == "respawn damageable")
        {
            Hp -= 1;
            transform.position = lastCheckPoint;
        }
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if(trigger.gameObject.tag == "checkpoint")
        {
            lastCheckPoint = trigger.transform.position;
        }
    }

    //function that flips the character
    void flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(enemyCollider.position, attackBoxRadius, Enemies);

        foreach (Collider2D enemy in hitEnemies)
        {
            int direction;
            
            if (facingRight)
            {
                direction = 1;
            }
            else
            {

                direction = -1;
            }
            enemy.GetComponent<EnemyHp>().TakeDamage(attackDamage, knockBackGiven, direction);
        }
    }

    void setWallJumpToFalse()
    {
        wallJumping = false;
    }

    void restartCurrentScene()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}