using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCar : Car
{
    public Transform playerTrans;

    protected const float MAX_DEVIATION = 0.1f;

    protected override void Awake()
    {
        GetComponents();
        playerTrans = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        turnVector = new Vector2(TURN_FORCE, 0);
        float multiplier = Random.Range(-MAX_DEVIATION, MAX_DEVIATION);
        normSpeed = normSpeed * (1 + multiplier);
    }

    private void Update()
    {
        if (transform.position.y - playerTrans.position.y < -10)
        {
            Destroy(this.gameObject);
        }
    }
}
