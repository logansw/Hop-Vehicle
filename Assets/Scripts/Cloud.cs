using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public static int cloudCount;

    public Transform playerTrans;
    public SpriteRenderer spr;

    private Vector3 velocityVector;

    [SerializeField] private float velocity;
    [SerializeField] private float deviation;
    [SerializeField] private Vector2 minDimensions;
    [SerializeField] private Vector2 maxDimensions;

    private void Awake()
    {
        playerTrans = GameObject.Find("Player").GetComponent<Transform>();
        spr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        cloudCount++;
        velocityVector = InitializeVelocity(velocity);
        spr.size = new Vector2(Random.Range(minDimensions.x, maxDimensions.x), Random.Range(minDimensions.y, maxDimensions.y));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += velocityVector;
        ClearOffScreen();
    }

    private Vector3 InitializeVelocity(float velocity)
    {
        velocity += velocity * Random.Range(-deviation, deviation);
        if (Random.Range(-1, 1) < 0)
        {
            velocity = -velocity;
        }
        return new Vector3(velocity, 0);
    }

    private void ClearOffScreen()
    {
        if (transform.position.y < playerTrans.position.y - 10f)
        {
            cloudCount--;
            Destroy(this.gameObject);
        } else if (transform.position.x < -(15 + spr.size.x / 2)
                    || transform.position.x > (15 + spr.size.x / 2)) {
            transform.position = new Vector2(-transform.position.x, transform.position.y);
        }
    }
}
