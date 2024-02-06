using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private float minX, maxX, minY, maxY;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        // Assuming your game world starts at (0,0) and your bounds are 20x20
        minX = 0;
        maxX = 20;
        minY = 0;
        maxY = 20;
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        movement = new Vector2(moveX, moveY);
    }

    void FixedUpdate()
    {
        MoveCharacter(movement);
    }

    void MoveCharacter(Vector2 direction)
    {
        Vector2 newPos = rb.position + direction * speed * Time.fixedDeltaTime;
        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
        newPos.y = Mathf.Clamp(newPos.y, minY, maxY);
        rb.MovePosition(newPos);
    }
}
