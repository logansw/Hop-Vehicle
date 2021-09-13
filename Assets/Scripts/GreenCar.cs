using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenCar : EnemyCar
{
    [SerializeField] private float jumpVelocity;
    private bool isGrounded;
    private float lookDistance = 3f;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y - playerTrans.position.y < -10)
        {
            Destroy(this.gameObject);
        }
        float distance = Mathf.Abs(transform.position.x - playerTrans.position.x);
        if (isGrounded && distance < lookDistance)
        {
            Jump();
        }
    }

    private void Jump()
    {
        isGrounded = false;
        rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }
}
