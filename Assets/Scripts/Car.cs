using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* General Car class, from which other cars derive. All cars should:
 * 
 * Move at a constant, random speed
 * Detect edge
 * Flip
 */
public class Car : MonoBehaviour
{
    protected const float TURN_FORCE = 1000f;

    [SerializeField] protected float normSpeed;
    [SerializeField] protected SpriteRenderer spr;
    protected Rigidbody2D rb;
    protected Vector2 turnVector;
    protected float turnDistance = 7;
    protected float turnDampener = 0.25f;

    protected virtual void Awake()
    {
        GetComponents();
    }

    private void Start()
    {
        turnVector = new Vector2(TURN_FORCE, 0);
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        Drive();
        if (transform.position.x > turnDistance)
        {
            TurnLeft();
        }
        else if (transform.position.x < -turnDistance)
        {
            TurnRight();
        }
    }

    protected void Drive()
    {
        if (Mathf.Abs(rb.velocity.x) < normSpeed)
        {
            rb.AddForce(turnVector * Time.deltaTime);
        } 
    }

    protected void TurnLeft()
    {
        rb.velocity = new Vector2(-normSpeed * turnDampener, rb.velocity.y);
        spr.flipX = true;
        turnVector = new Vector2(-TURN_FORCE, 0);
    }

    protected void TurnRight()
    {
        rb.velocity = new Vector2(normSpeed * turnDampener, rb.velocity.y);
        spr.flipX = false;
        turnVector = new Vector2(TURN_FORCE, 0);
    }

    protected void Freeze()
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
    }

    protected void GetComponents()
    {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
    }
}
