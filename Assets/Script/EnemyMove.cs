using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Rigidbody2D enemyRigidbody;
    Animator anim;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D enemyCollider;



    // Start is called before the first frame update
    void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyCollider = GetComponent<CapsuleCollider2D>();

    }

    // Update is called once per frame
    public virtual void OnDamaged()
    {
        // Sprite Death Anim
        anim.SetBool("isDestroy", true);

        //// Collider Disable
        enemyCollider.enabled = false;

        // Sprite Alpha
        //spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        //// Sprite Flip Y
        //spriteRenderer.flipY = true;

        //// Collider Disable
        //enemyCollider.enabled = false;

        //// Die Effect Jump
        enemyRigidbody.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        //// Destroy
        Invoke("DeActive", 5);
    }
    void DeActive()
    {
        gameObject.SetActive(false);
    }


}
