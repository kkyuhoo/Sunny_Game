using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameManager gameManager;
    public AudioClip audioJump;

    private Rigidbody2D playerRigidbody;
    SpriteRenderer spriteRenderer;
    Animator anim;
    AudioSource audioSource;
    CapsuleCollider2D playerCollider;

    public float maxSpeed;
    public float JumpPower;

    // Start is called before the first frame update
    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Jump
        if (Input.GetButtonDown("Jump") && !anim.GetBool("isJumping"))
        {
            playerRigidbody.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
            //PlaySound("JUMP");

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

        // 오른쪽
        if(playerRigidbody.velocity.x > maxSpeed)
        {
            playerRigidbody.velocity = new Vector2(maxSpeed, playerRigidbody.velocity.y);
        }
        // 왼쪽
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            // Attack
            if(playerRigidbody.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                OnAttack(collision.transform);
            }
            // Damaged
            else
            {
                OnDamaged(collision.transform.position);
            }
        }
        else if(collision.gameObject.tag == "Eagle")
        {
            if (playerRigidbody.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                OnAttack2(collision.transform);
            }
            // Damaged
            else
            {
                OnDamaged(collision.transform.position);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            // Point
            // Contains(비교문) : 대상 문자열에 비교문이 있으면 true
            bool ischerry = collision.gameObject.name.Contains("cherry");
            bool isgem = collision.gameObject.name.Contains("gem");

            if (ischerry)
            {
                //gameManager.stagePoint += 50;
                Animator animCherry = collision.GetComponent<Animator>();
                animCherry.SetBool("ItemFeedback", true);
            }
            else if (isgem)
            {
                //gameManager.stagePoint += 100;
                Animator animGem = collision.GetComponent<Animator>();
                animGem.SetBool("ItemFeedback", true);
            }

        }
        else if (collision.gameObject.tag == "Finish")
        {
            // Next Stage
            //gameManager.NextStage();

        }

    }
    void OnAttack(Transform enemy)
    {
        // Point
        //gameManager.stagePoint += 100;

        // Reaction Force
        playerRigidbody.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        // Enemy Die
        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
        enemyMove.OnDamaged();
    }

    void OnAttack2(Transform enemy)
    {
        // Point
        //gameManager.stagePoint += 100;

        // Reaction Force
        playerRigidbody.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        // Enemy Die
        eagleMove eagleMove = enemy.GetComponent<eagleMove>();
        eagleMove.OnDamaged();
    }

    void OnDamaged(Vector2 targetPos)
    {
        // Health Down
        //gameManager.HealthDown();

        // Change Layer (Immortal Active)
        gameObject.layer = 11;

        // View Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        // Reaction Force
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        playerRigidbody.AddForce(new Vector2(dirc, 1) * 5, ForceMode2D.Impulse);

        // Animation
        anim.SetTrigger("doDamaged");

        Invoke("OffDamaged", 3);

    }

    void OffDamaged()
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    public void OnDie()
    {
        // Sprite Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        // Sprite Flip Y
        spriteRenderer.flipY = true;

        // Collider Disable
        playerCollider.enabled = false;

        // Die Effect Jump
        playerRigidbody.AddForce(Vector2.up * 2, ForceMode2D.Impulse);
    }

    public void VelocityZero()
    {
        playerRigidbody.velocity = Vector2.zero;
    }
}
