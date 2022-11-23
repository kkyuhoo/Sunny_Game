using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    private Rigidbody2D playerRigidbody;
    SpriteRenderer spriteRenderer;
    Animator anim;
    public float maxSpeed;
    public float JumpPower;

    // Start is called before the first frame update
    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        // Jump
        if (Input.GetButtonDown("Jump") && !anim.GetBool("isJumping"))
        {
            playerRigidbody.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
        }

        // Stop Speed
        if (Input.GetButtonUp("Horizontal"))
        {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.normalized.x * 0.5f, playerRigidbody.velocity.y);
        }

        // Direction Sprite
        float h = Input.GetAxisRaw("Horizontal");
        if (h != 0)
        {
            spriteRenderer.flipX = h == -1;
        }

        if (Mathf.Abs(playerRigidbody.velocity.x) < 0.3)
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
        }


    }
    // Update is called once per frame
    void FixedUpdate()
    {
        // move Speed
        float move = Input.GetAxisRaw("Horizontal");

        playerRigidbody.AddForce(Vector2.right * move, ForceMode2D.Impulse);

        // ¿À¸¥ÂÊ
        if(playerRigidbody.velocity.x > maxSpeed)
        {
            playerRigidbody.velocity = new Vector2(maxSpeed, playerRigidbody.velocity.y);
        }
        // ¿ÞÂÊ
        else if (playerRigidbody.velocity.x < maxSpeed * (-1))
        {
            playerRigidbody.velocity = new Vector2(maxSpeed * (-1), playerRigidbody.velocity.y);
        }


        // Landing Platform
        if (playerRigidbody.velocity.y < 0)
        {
            Debug.DrawRay(playerRigidbody.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(playerRigidbody.position, Vector3.down, Mathf.Infinity, LayerMask.GetMask("Platform"));

            if(rayHit.collider != null)
            {
                if(rayHit.distance < 0.5f)
                {
                    anim.SetBool("isJumping", false);
                }
            }
        }

    }
}
