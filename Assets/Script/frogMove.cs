using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // AI, 내비게이션 시스템 관련 코드 가져오기

public class frogMove : MonoBehaviour
{
    private Rigidbody2D enemyRigidbody;
    Animator anim;
    SpriteRenderer spriteRenderer;
    public int nextMove;
    private float jumpMove;
    public float jumpForce;


    // Start is called before the first frame update
    void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Invoke("Think", 2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Platform Check

        if (enemyRigidbody.velocity.y < 0)
        {
            Debug.DrawRay(enemyRigidbody.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(enemyRigidbody.position, Vector3.down, Mathf.Infinity, LayerMask.GetMask("Platform"));

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {
                    anim.SetBool("isJumping", false);
                }
            }
            if (rayHit.collider == null)
            {
                Turn();
            }
        }
    }

    void Think()
    {
        // Set Next Active
        nextMove = Random.Range(-1, 2);
        jumpMove = Random.Range(200, 250);
        jumpForce = Random.Range(250, 350);
        // Sprite Animation

        // Flip Sprite
        if (nextMove != 0)
        {
            spriteRenderer.flipX = nextMove == 1;
        }

        // Recursive
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
        enemyRigidbody.velocity = Vector2.zero;


        if (!anim.GetBool("isJumping") && nextMove != 0)
        {
            enemyRigidbody.AddForce(new Vector2(nextMove * jumpMove, jumpForce));
            anim.SetBool("isJumping", true);
        }
    }

    void Turn()
    {
        nextMove = nextMove * (-1);
        spriteRenderer.flipX = nextMove == 1;

        CancelInvoke();
        Invoke("Think", 2);
    }

}
