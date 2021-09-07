using UnityEngine;

public class PlayerPaddle : Paddle
{
    private Vector2 direct;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            direct = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            direct = Vector2.down;
        }
        else
        {
            direct = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if (direct.sqrMagnitude != 0)
        {
            rigid_body.AddForce(direct * this.speed);
        }
    }
}
