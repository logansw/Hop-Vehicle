using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeCar : EnemyCar
{
    private void Start()
    {
        turnVector = new Vector2(TURN_FORCE, 0);
        float multiplier = Random.Range(-MAX_DEVIATION, MAX_DEVIATION);
        normSpeed = normSpeed * (1 + multiplier);

        if (Random.Range(-1f, 1f) < 0)
        {
            turnVector = -turnVector;
            spr.flipX = true;
        }
    }

    protected override void FixedUpdate()
    {
        Drive();
        if (transform.position.x > 7.5)
            transform.position += new Vector3(-15f, 0, 0);
        else if (transform.position.x < -7.5)
            transform.position += new Vector3(15f, 0, 0);

        if (transform.position.y - playerTrans.position.y < -10)
            Destroy(this.gameObject);
    }
}
