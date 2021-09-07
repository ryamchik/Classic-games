using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 200.0f;

    private Rigidbody2D rigid_body;
    private void Awake()
    {
        rigid_body = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        ResetPosition();
        AddStartingForce();
    }

    public void ResetPosition()
    {
        rigid_body.position = Vector3.zero;
        rigid_body.velocity = Vector3.zero;
    }

    public void AddStartingForce()
    {
        float x = Random.value < 0.5f? -1.0f: 1.0f;
        float y = Random.value < 0.5f? Random.Range(-1.0f, -0.5f): Random.Range(0.5f, 1.0f);

        Vector2 direct = new Vector2(x, y);
        rigid_body.AddForce(direct * speed);
    }

    public void AddForce(Vector2 force)
    {
        rigid_body.AddForce(force);
    }
}