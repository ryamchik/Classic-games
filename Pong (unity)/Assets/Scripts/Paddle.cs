using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed = 10.0f;

    protected Rigidbody2D rigid_body;

    private void Awake()
    {
        rigid_body = GetComponent<Rigidbody2D>();

    }

    public void ResetPosition()
    {
        rigid_body.position = new Vector2(rigid_body.position.x, 0.0f);
        rigid_body.velocity = Vector2 .zero;
    }
}
