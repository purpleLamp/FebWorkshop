using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    public float moveSpeed;
    private Vector2 input;
    public Rigidbody2D rb;
    public SpriteRenderer sp;

    private bool jumpPressed;
    public float jumpPower;

    private void OnDrawGizmos()
    {
        Vector2 startPos = new Vector2(transform.position.x - 0.4f, transform.position.y - 0.6f);
        Vector2 endPos = new Vector2(transform.position.x + 0.4f, transform.position.y - 0.6f);

        Debug.DrawLine(startPos, endPos, Color.red);
    }

    void Update()
    {
        rb.velocity = new Vector2(input.x * moveSpeed, rb.velocity.y);

        if (jumpPressed) rb.velocity = new Vector2(rb.velocity.x, jumpPw);
        if (!Grounded()) jumpPressed = false;

        if (input.x < 0f) sp.flipX = true;
        if (input.x > 0f) sp.flipX = false;

    }

    public void move(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    public void jump(InputAction.CallbackContext context)
    {
        if (context.performed && Grounded())
        {
            jumpPressed = true;
        }

        if (context.canceled)
        {
            jumpPressed = false;
        }
    }

    private bool Grounded()
    {
        Vector2 startPos = new Vector2(transform.position.x - 0.4f, transform.position.y - 0.6f);

        return Physics2D.Raycast(startPos, Vector2.right, 0.8f);
    }

}
