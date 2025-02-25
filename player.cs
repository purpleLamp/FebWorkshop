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

    private bool jumpRequest;
    public float jumpPower;
    private float maxJumpDir = 0.1f;
    private float jumpTime;

    public bool grabPressed;
    public AudioClip jumpClip;

    private void OnDrawGizmos()
    {
        Debug.DrawRay(new Vector2(transform.position.x - 0.4f, transform.position.y - 0.6f), new Vector2(.8f, 0), Color.red);
    }

    void Update()
    {
        rb.velocity = new Vector2(input.x * moveSpeed, rb.velocity.y);

        if (jumpTime > maxJumpDir) jumpRequest = false;

        if (input.x < 0f) sp.flipX = true;
        if (input.x > 0f) sp.flipX = false;

        if (transform.position.y < -20f)
        {
            SceneManager.LoadScene("Level");
        }
    }

    public void move(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    public void jump(InputAction.CallbackContext context)
    {
        if (context.performed && Grounded())
        {
            AudioInterface.instance.PlayClip(jumpClip, transform, 0.3f);
            jumpRequest = true;
            jumpTime = 0;
        }

        if (context.canceled)
        {
            jumpRequest = false;
        }
    }
    private void FixedUpdate()
    {
        if (jumpRequest)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jumpTime += Time.deltaTime;
        }
    }

    private bool Grounded()
    {
        return Physics2D.Raycast(new Vector2(transform.position.x - 0.4f, transform.position.y - 0.6f), Vector2.right, 0.8f);
    }

}
