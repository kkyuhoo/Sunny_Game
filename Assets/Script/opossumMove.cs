using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opossumMove : MonoBehaviour
{
    private Rigidbody2D enemyRigidbody;
    Animator anim;
    SpriteRenderer spriteRenderer;

    private int nextMove = 1;
    private float maxSpeed = 5f;

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

        // Move
        enemyRigidbody.velocity = new Vector2(nextMove * maxSpeed, enemyRigidbody.velocity.y);

        // Platform Check
        Vector2 frontVec = new Vector2(enemyRigidbody.position.x + nextMove * 0.2f, enemyRigidbody.position.y);

        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)
        {
            Turn();
        }
    }
    void Think()
    {
        // Set Next Active
        nextMove = 1;

        // Flip Sprite
        if (nextMove != 0)
        {
            spriteRenderer.flipX = nextMove == 1;
        }

        // Recursive
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);

    }
    void Turn()
    {
        nextMove = nextMove * (-1);
        spriteRenderer.flipX = nextMove == 1;

        CancelInvoke();
        Invoke("Think", 5);
    }


}
