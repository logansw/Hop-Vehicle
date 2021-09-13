using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackCar : EnemyCar
{
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y - playerTrans.position.y < -10)
        {
            Destroy(this.gameObject);
        }

        if (playerTrans.position.y > transform.position.y - 1f)
        {
            if (transform.position.x < playerTrans.position.x)
                MidTurnRight();
            else if (transform.position.x > playerTrans.position.x)
                MidTurnLeft();
        }     
    }

    private void MidTurnRight()
    {
        spr.flipX = false;
        turnVector = new Vector2(TURN_FORCE, 0);
    }

    private void MidTurnLeft()
    {
        spr.flipX = true;
        turnVector = new Vector2(-TURN_FORCE, 0);
    }
}
