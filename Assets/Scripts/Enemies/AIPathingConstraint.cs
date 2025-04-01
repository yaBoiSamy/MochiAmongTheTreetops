using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIPathingConstraint : MonoBehaviour
{
    public Transform target;
    public Vector2 range;
    public float speed;

    private Rigidbody2D rb;

    void start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void update()
    {
        //if the target is in range of the enemy...

        Debug.Log(target.position.x);
        Debug.Log(transform.position.x);
        if (target.position.x < transform.position.x + range.x )
           //target.position.x > transform.position.x - range.x &&
           //target.position.y < transform.position.y + range.y &&
           //target.position.y > transform.position.y - range.y)
        {
            if (target.position.x < transform.position.x)
            {
                rb.velocity = new Vector2(-1, 0) * speed;
            }else if(target.position.x > transform.position.x)
            {
                rb.velocity = new Vector2(1, 0) * speed;
            }
        }
    }
   
}
