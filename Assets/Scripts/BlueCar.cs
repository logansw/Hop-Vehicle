using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueCar : Car
{
    private const float JUMP_BUFFER = 0.1f;

    public GameManager gameManager;

    [SerializeField] private float jumpVelocity = 10f;
    [SerializeField] private float weakGravity;
    [SerializeField] private float strongGravity;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource crashSound;
    [SerializeField] private Animator anim;

    private bool isGrounded = false;
    private Vector3 downCastGap = new Vector3(0, 0.4f, 0);
    private CapsuleCollider2D capCollider;
    private float timeSinceSpace;

    protected override void Awake()
    {
        GetComponents();
        capCollider = GetComponent<CapsuleCollider2D>();
        gameManager = FindObjectOfType<GameManager>();
        timeSinceSpace = float.PositiveInfinity;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameOver)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
            timeSinceSpace = 0;
        if (!Input.GetKey(KeyCode.Space) || rb.velocity.y < 0)
            rb.gravityScale = strongGravity;

        if (timeSinceSpace < JUMP_BUFFER && isGrounded)
        {
            isGrounded = false;
            jumpSound.pitch = Random.Range(0.9f, 1.1f);
            jumpSound.Play(0);
            anim.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
            rb.gravityScale = weakGravity;
        }

        timeSinceSpace += Time.deltaTime;
    }

    protected override void FixedUpdate()
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
        RaycastHit2D hitAbove = Physics2D.Raycast(transform.position, Vector2.up, 10f);
        RaycastHit2D hitBelow = Physics2D.Raycast(transform.position - downCastGap, Vector2.down, 10f);
        if (hitAbove.collider.tag.Equals("Floor"))
        {
            Physics2D.IgnoreCollision(hitAbove.collider, capCollider);
        }
        if (hitBelow.collider != null && hitBelow.collider.tag.Equals("Floor"))
        {
            Physics2D.IgnoreCollision(hitBelow.collider, capCollider, false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Enemy") && !gameManager.gameOver)
        {
            gameManager.gameOver = true;
            crashSound.Play(0);
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;
            CameraShake.Instance.ShakeCamera(5f, 1f);
            gameManager.EndGame();
        } else if (collision.collider.tag.Equals("Floor"))
        {
            isGrounded = true;
        }
    }
}
