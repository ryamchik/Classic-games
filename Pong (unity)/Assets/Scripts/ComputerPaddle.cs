using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerPaddle : Paddle
{
    public Rigidbody2D ball;

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (this.ball.velocity.x > 0.0f)
        {
            if (this.ball.position.y > this.transform.position.y)
            {
                rigid_body.AddForce(Vector2.up * this.speed);
            }
            else if (this.ball.position.y < this.transform.position.y)
            {
                rigid_body.AddForce(Vector2.down * this.speed);
            }
        }
        else
        {
            if (rigid_body.position.y > 0.0f)
            {
                rigid_body.AddForce(Vector2.down * this.speed);
            }
            else if (rigid_body.position.y < 0.0f)
            {
                rigid_body.AddForce(Vector2.up * this.speed);
            }
        }
    }
}
