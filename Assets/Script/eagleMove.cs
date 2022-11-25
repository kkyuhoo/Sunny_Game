using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eagleMove : MonoBehaviour
{
    private Rigidbody2D enemyRigidbody;
    Animator anim;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D enemyCollider;

    private float maxSpeed = 2f;
    private Vector3 oriPos;
    private float distance = 1f;
    private Vector2 dir = Vector3.up;

    // Start is called before the first frame update
    void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyCollider = GetComponent<CapsuleCollider2D>();

        oriPos = transform.position;
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        var pos = enemyRigidbody.position;
        pos += dir * maxSpeed * Time.deltaTime;
        enemyRigidbody.MovePosition(pos);

        if (oriPos.y + distance < enemyRigidbody.position.y && !anim.GetBool("isHurt"))
        {
            dir = Vector3.down;
        }
        if (oriPos.y - distance > enemyRigidbody.position.y && !anim.GetBool("isHurt"))
        {
            dir = Vector3.up;
        }

    }

    public void OnDamaged ()
    {
        anim.SetBool("isDestroy", true);

        //// Sprite Ani
        //anim.SetBool("isHurt" ,true);

        //// Sprite Alpha
        //spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        //// Sprite Flip Y
        //spriteRenderer.flipY = true;

        //// Collider Disable
        enemyCollider.enabled = false;

        //// Die Effect Jump
        //enemyRigidbody.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        //// Destroy
        //Invoke("DeActive", 5);
    }
    void DeActive()
    {
        gameObject.SetActive(false);
    }


}
